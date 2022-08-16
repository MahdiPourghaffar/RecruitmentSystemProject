using System;
using System.Threading;
using System.Threading.Tasks;
using Application.AnnouncementCRUD.Dtos;
using Application.Common;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.AnnouncementCRUD
{
    public class Detail
    {
        public class Query : IRequest<Result<AnnouncementResultDto>>
        {
            public int AnnouncementId { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<AnnouncementResultDto>>
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

            public async Task<Result<AnnouncementResultDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var announcement = await _context.Announcements
                        .AsNoTracking()
                        .ProjectTo<AnnouncementResultDto>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync(x => x.Id == request.AnnouncementId, cancellationToken);

                    if (announcement == null)
                    {
                        return Result<AnnouncementResultDto>.Failure("NotFound");
                    }

                    return Result<AnnouncementResultDto>.Success(announcement);

                }
                catch (Exception e)
                {
                    return Result<AnnouncementResultDto>.Failure($"Failure : {e.Message}");
                }
            }
        }
    }
}
