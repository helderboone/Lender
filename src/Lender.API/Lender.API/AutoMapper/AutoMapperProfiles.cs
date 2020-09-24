using AutoMapper;
using Lender.API.Application.Commands;
using Lender.API.Application.DTO;
using Lender.API.Models;

namespace Lender.API.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CreateFriendCommand, Friend>().ReverseMap();
            CreateMap<Friend, FriendDto>().ReverseMap();
        }
    }
}
