using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.CategoryCRUD.Dtos;
using Application.Common;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CategoryCRUD
{
    public class Detail
    {
        public class Query : IRequest<Result<CategoryResultDto>>
        {
            public int CategoryId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<CategoryResultDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<CategoryResultDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var category = await _context.Categories
                        .AsNoTracking()
                        .ProjectTo<CategoryResultDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync(x => x.Id == request.CategoryId, cancellationToken);

                        return Result<CategoryResultDto>.Success(category);
                }
                catch (Exception e)
                {
                    return Result<CategoryResultDto>.Failure($"Failure : {e.Message}");
                }
            }
        }

    }
}
