using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.AnnouncementCRUD.Dtos;
using Application.Common;
using Application.Params;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.AnnouncementCRUD
{
    public class List
    {
        public class Query : IRequest<Result<List<AnnouncementResultDto>>>
        {
            public AnnouncementParams Params { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<List<AnnouncementResultDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }


            public async Task<Result<List<AnnouncementResultDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var announcements = _context.Announcements
                        .AsNoTracking()
                        .Where(x => x.Confirmed == true)
                        .ProjectTo<AnnouncementResultDto>(_mapper.ConfigurationProvider)
                        .AsQueryable();

                    if (!String.IsNullOrEmpty(request.Params.City))
                    {
                        var city = request.Params.City.TrimStart().TrimEnd();
                        var cities = new Dictionary<Announcement.CitiesEnum, string>()
                        {
                            { Announcement.CitiesEnum.Tabriz, "تبریز"},
                            { Announcement.CitiesEnum.Tehran , "تهران"},
                            { Announcement.CitiesEnum.Esfehan , "اصفهان"},
                            { Announcement.CitiesEnum.Mashhad , "مشهد"},
                            { Announcement.CitiesEnum.Shiraz , "شیراز"}
                        };

                        foreach (var c in cities)
                        {
                            if (c.Value.Contains(city))
                            {
                                announcements = announcements.Where(d => d.City == c.Key);
                                break;
                            }
                        }
                    }

                    if (!String.IsNullOrEmpty(request.Params.CooperationType))
                    {
                        var cooperationType = request.Params.CooperationType.TrimStart().TrimEnd();
                        var cooperationTypes = new Dictionary<Announcement.CooperationTypeEnum, string>()
                        {
                            { Announcement.CooperationTypeEnum.FullTime, "تمام وقت"},
                            { Announcement.CooperationTypeEnum.PartTime , "‍‍پاره وقت"},
                            { Announcement.CooperationTypeEnum.Remote , "دورکاری"},
                        };
                        foreach (var co in cooperationTypes)
                        {
                            if (co.Value.Contains(cooperationType))
                            {
                                announcements = announcements.Where(d => d.CooperationType == co.Key);
                                break;
                            }
                        }
                    }

                    if (!String.IsNullOrEmpty(request.Params.Salary.ToString()))
                    {
                        announcements = announcements.Where(d => d.Salary >= request.Params.Salary);
                    }

                    if (!String.IsNullOrEmpty(request.Params.Name))
                    {
                        announcements = announcements.Where(d => d.JobName.Contains(request.Params.Name));
                    }

                    return Result<List<AnnouncementResultDto>>.Success(await announcements.ToListAsync(cancellationToken));
                }
                catch (Exception e)
                {
                    return Result<List<AnnouncementResultDto>>.Failure($"Failure : {e.InnerException.Message}");
                }
            }
        }
    }
}
