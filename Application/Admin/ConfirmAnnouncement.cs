using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Admin
{
    public class ConfirmAnnouncement
    {
        public class Command : IRequest<Result>
        {
            public int AnnouncementId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var announcement = await _context.Announcements
                        .FirstOrDefaultAsync(x => x.Id == request.AnnouncementId, cancellationToken);
                    if (announcement == null)
                    {
                        return Result.Failure("NotFound");
                    }
                    announcement.Confirmed = true;
                    var result = await _context.SaveChangesAsync(cancellationToken);
                    if (result > 0)
                    {
                        return Result.Success();
                    }
                    return Result.Failure("SomeThing Wrong :/");
                }
                catch(Exception e)
                {
                    return Result.Failure($"Failure {e.Message}");
                }
            }
        }
    }
}
