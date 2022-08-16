using Application.Accounts.Dtos;
using Application.AnnouncementCRUD.Dtos;
using Application.CategoryCRUD.Dtos;
using Application.Company.Dtos;
using AutoMapper;
using Domain;

namespace Application.Common
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Announcement, AnnouncementResultDto>().ReverseMap();
            CreateMap<Announcement, AnnouncementRequestDto>().ReverseMap();

            CreateMap<Category, CategoryRequestDto>().ReverseMap();
            CreateMap<Category, CategoryResultDto>().ReverseMap();

            CreateMap<User, SignUpDto>()
                .ForMember(x => x.RePassword, y => y.Ignore())
                .ReverseMap();
            CreateMap<User, UserDto>()
                .ForMember(x => x.Token, y => y.Ignore())
                .ReverseMap();

            CreateMap<User, RequestProfileDto>().ReverseMap();
            CreateMap<User, ResultProfileDto>().ReverseMap();
        }
    }
}
