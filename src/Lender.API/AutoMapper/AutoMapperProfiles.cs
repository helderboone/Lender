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
            //Friends
            CreateMap<CreateFriendCommand, Friend>().ReverseMap();
            CreateMap<CreateFriendCommand, Address>().ReverseMap();

            CreateMap<UpdateFriendCommand, Friend>().ReverseMap();
            CreateMap<UpdateFriendCommand, Address>().ReverseMap();

            CreateMap<Friend, FriendDto>()
                .ForMember(x => x.Number, y => y.MapFrom(z => z.Address.Number))
                .ForMember(x => x.Street, y => y.MapFrom(z => z.Address.Street))
                .ForMember(x => x.Neighborhood, y => y.MapFrom(z => z.Address.Neighborhood))
                .ForMember(x => x.City, y => y.MapFrom(z => z.Address.City));

            //Games
            CreateMap<CreateGameCommand, Game>().ReverseMap();
            CreateMap<UpdateFriendCommand, Game>().ReverseMap();

            CreateMap<Game, GameDto>();
        }
    }
}
