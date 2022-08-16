using System;
using System.Threading;
using System.Threading.Tasks;
using Application.CategoryCRUD.Dtos;
using Application.Common;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.CategoryCRUD
{
    public class Create
    {
        public class Command : IRequest<Result<CategoryResultDto>>
        {
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
                    var category = _mapper.Map<Category>(request.Category);
                    await _context.Categories.AddAsync(category, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    var categoryOut = _mapper.Map<CategoryResultDto>(category);
                    return Result<CategoryResultDto>.Success(categoryOut);
                }
                catch (Exception e)
                {
                    return Result<CategoryResultDto>.Failure($"Failure : {e.InnerException.Message}");
                }

            }
        }
    }
}
