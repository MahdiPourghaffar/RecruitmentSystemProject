using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.CategoryCRUD.Dtos;
using Application.Common;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CategoryCRUD
{
    public class Update
    {
        public class Command : IRequest<Result<CategoryResultDto>>
        {
            public int CategoryId { get; set; }
            public CategoryRequestDto Category { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<CategoryResultDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<CategoryResultDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var category = await _context.Categories
                        .FirstOrDefaultAsync(x => x.Id == request.CategoryId, cancellationToken);
                    if (category == null)
                    {
                        return Result<CategoryResultDto>.Failure("NotFound");
                    }
                    category.CategoryName = request.Category.CategoryName;
                    await _context.SaveChangesAsync(cancellationToken);
                    var categoryOut = _mapper.Map<CategoryResultDto>(category);
                    return Result<CategoryResultDto>.Success(categoryOut);
                }
                catch (Exception e)
                {
                    return Result<CategoryResultDto>.Failure($"Failure : {e.Message}");
                }
            }
        }
    }
}
