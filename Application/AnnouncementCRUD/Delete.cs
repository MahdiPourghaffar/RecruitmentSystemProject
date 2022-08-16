using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.AnnouncementCRUD
{
    public class Delete
    {
        public class Command : IRequest<Result>
        {
            public int AnnouncementId { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IAuthorizationService _authorizationService;

            public Handler(DataContext context, IUserAccessor userAccessor, IAuthorizationService authorizationService)
            {
                _context = context;
                _userAccessor = userAccessor;
                _authorizationService = authorizationService;
            }

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }


            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {

                    var announcement = await _context.Announcements.FirstOrDefaultAsync(x => x.Id == request.AnnouncementId, cancellationToken);

                    var result = await _authorizationService.AuthorizeAsync(_userAccessor.GetUserPrinciple
                        , announcement, "AnnouncementCompany");

                    if (result.Succeeded)
                    {
                        if (announcement == null)
                        {
                            return Result.Failure("NotFound");
                        }

                        _context.Announcements.Remove(announcement);
                        await _context.SaveChangesAsync(cancellationToken);
                        return Result.Success();
                    }

                    return Result.Failure("Access Denied :/");
                }
                catch (Exception e)
                {
                    return Result.Failure($"Failure : {e.Message}");
                }
            }
        }
    }
}
