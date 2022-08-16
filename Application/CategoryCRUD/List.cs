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
    public class List
    {
        public class Query : IRequest<Result<List<CategoryResultDto>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<CategoryResultDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<List<CategoryResultDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var categories = await _context.Categories
                        .AsNoTracking()
                        .ProjectTo<CategoryResultDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);

                    return Result<List<CategoryResultDto>>.Success(categories);
                }
                catch (Exception e)
                {
                    return Result<List<CategoryResultDto>>.Failure($"Failure : {e.Message}");
                }

            }
        }
    }
}
