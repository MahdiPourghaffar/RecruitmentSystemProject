using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MediatR;
using System.Threading;
using Application.AnnouncementCRUD.Dtos;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Application.Common;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Application.AnnouncementCRUD
{
    public class Create
    {
        public class Command : IRequest<Result<AnnouncementResultDto>>
        {
            public AnnouncementRequestDto Announcement { get; set; }
        }
        public class Handler : IRequestHandler<Command, Result<AnnouncementResultDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly UserManager<User> _userManager;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IMapper mapper, UserManager<User> userManager, IUserAccessor userAccessor)
            {
                _context = context;
                _mapper = mapper;
                _userManager = userManager;
                _userAccessor = userAccessor;
            }

            public async Task<Result<AnnouncementResultDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var announcement = _mapper.Map<Announcement>(request.Announcement);
                    var user = await _userManager.FindByIdAsync(_userAccessor.UserId);

                    if (request.Announcement.CategoryId != null)
                    {
                        if (! await _context.Categories.AnyAsync(x => x.Id == announcement.CategoryId, cancellationToken))
                        {
                            return Result<AnnouncementResultDto>.Failure("Category NotFound");
                        }
                    }

                    announcement.UserId = user.Id;
                    announcement.UserName = user.UserName;
                    await _context.Announcements.AddAsync(announcement, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    var announcementOut = _mapper.Map<AnnouncementResultDto>(announcement);
                    return Result<AnnouncementResultDto>.Success(announcementOut);

                }
                catch (Exception e)
                {
                    return Result<AnnouncementResultDto>.Failure($"Failure : {e.InnerException.Message} ");
                }

            }
        }
    }
}
