using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Accounts.Dtos;
using Application.Common;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Accounts
{
    public class CurrentUser
    {
        public class Query : IRequest<Result<UserDto>>
        {

        }

        public class Handler : IRequestHandler<Query, Result<UserDto>>
        {
            private readonly UserManager<User> _userManager;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;

            public Handler(UserManager<User> userManager, IUserAccessor userAccessor, IMapper mapper)
            {
                _userManager = userManager;
                _userAccessor = userAccessor;
                _mapper = mapper;
            }


            public async Task<Result<UserDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.Users
                    .AsNoTracking()
                    .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.Id.ToString() == _userAccessor.UserId);
                return Result<UserDto>.Success(user);
            }
        }
    }
}
