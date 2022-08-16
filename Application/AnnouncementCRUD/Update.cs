using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Application.AnnouncementCRUD.Dtos;
using Application.Common;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.AnnouncementCRUD
{
    public class Update
    {
        public class Command : IRequest<Result<AnnouncementResultDto>>
        {
            public int AnnouncementId { get; set; }
            public AnnouncementRequestDto Announcement { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<AnnouncementResultDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IAuthorizationService _authorizationService;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IMapper mapper, IAuthorizationService authorizationService, IUserAccessor userAccessor)
            {
                _context = context;
                _mapper = mapper;
                _authorizationService = authorizationService;
                _userAccessor = userAccessor;
            }


            public async Task<Result<AnnouncementResultDto>> Handle(Command request, CancellationToken cancellationToken)
            {

                try
                {
                    var announcement =
                        await _context.Announcements.FirstOrDefaultAsync(x => x.Id == request.AnnouncementId,
                            cancellationToken);
                   
                    if (announcement == null)
                    {
                        return Result<AnnouncementResultDto>.Failure("NotFound");
                    }

                    var result = await _authorizationService.AuthorizeAsync(_userAccessor.GetUserPrinciple
                        , announcement, "AnnouncementCompany");
                    if (result.Succeeded)
                    {
                        announcement.JobName = request.Announcement.JobName;
                        announcement.City = request.Announcement.City;
                        announcement.CooperationType = request.Announcement.CooperationType;
                        announcement.Gender = request.Announcement.Gender;
                        announcement.JobDescription = request.Announcement.JobDescription;
                        announcement.MilitarySituation = request.Announcement.MilitarySituation;
                        announcement.MinDegree = request.Announcement.MinDegree;
                        announcement.MinExperience = request.Announcement.MinExperience;
                        announcement.Salary = request.Announcement.Salary;

                        if (request.Announcement.CategoryId != null)
                        {
                            if (!await _context.Categories.AnyAsync(x => x.Id == request.Announcement.CategoryId,
                                    cancellationToken))
                            {
                                return Result<AnnouncementResultDto>.Failure("Category NotFound");
                            }
                        }

                        announcement.CategoryId = request.Announcement.CategoryId;
                        await _context.SaveChangesAsync(cancellationToken);

                        var announcementOut = _mapper.Map<AnnouncementResultDto>(announcement);
                        return Result<AnnouncementResultDto>.Success(announcementOut);
                    }
                    return Result<AnnouncementResultDto>.Failure("Access Denied :/");
                }
                catch (Exception e)
                {
                    return Result<AnnouncementResultDto>.Failure($"Failure : {e.InnerException.Message}");
                }
            }
        }
    }
}
