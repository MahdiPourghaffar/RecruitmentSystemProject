using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CategoryCRUD
{
    public class Delete
    {
        public class Command : IRequest<Result>
        {
            public int CategoryId { get; set; }
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
                    var category =
                        await _context.Categories.FirstOrDefaultAsync(x => x.Id == request.CategoryId,
                            cancellationToken);
                    if (category == null)
                    {
                        return Result.Failure("notFound");
                    }

                    _context.Categories.Remove(category);
                    await _context.SaveChangesAsync(cancellationToken);
                    return Result.Success();

                }
                catch (Exception e)
                {
                    return Result.Failure($"Failure : {e.Message}");
                }
            }
        }
    }
}
