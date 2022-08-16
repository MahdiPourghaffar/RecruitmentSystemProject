using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Company.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Company
{
    public class EditProfile
    {
        public class Command : IRequest<Result<ResultProfileDto>>
        {
            public RequestProfileDto ProfileDto { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<ResultProfileDto>>
        {
            private readonly UserManager<User> _userManager;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;

            public Handler(UserManager<User> userManager, IMapper mapper, IUserAccessor userAccessor)
            {
                _userManager = userManager;
                _mapper = mapper;
                _userAccessor = userAccessor;
            }
            public async Task<Result<ResultProfileDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var company = await _userManager.FindByIdAsync(_userAccessor.UserId);
                    if (company == null)
                    {
                        return Result<ResultProfileDto>.Failure("NotFound");
                    }

                    company.Name = request.ProfileDto.Name;
                    company.UserName = request.ProfileDto.UserName;
                    company.Email = request.ProfileDto.Email;
                    company.Location = request.ProfileDto.Location;
                    company.PhoneNumber = request.ProfileDto.PhoneNumber;
                    var result = await _userManager.UpdateAsync(company);
                    if (result.Succeeded)
                    {
                        var compnayOut = _mapper.Map<ResultProfileDto>(company);
                        return Result<ResultProfileDto>.Success(compnayOut);
                    }

                    var errorMessage = "";
                    foreach (var error in result.Errors)
                    {
                        errorMessage += error.Description + Environment.NewLine;
                    }

                    return Result<ResultProfileDto>.Failure($"Failure : {errorMessage}", HttpStatusCode.BadRequest);
                }
                catch (Exception e)
                {
                    return Result<ResultProfileDto>.Failure($"Failure {e.Message}");
                }
            }
        }
    }

}
