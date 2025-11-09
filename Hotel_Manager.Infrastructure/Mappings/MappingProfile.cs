using AutoMapper;
using Hotel_Manager.Core.Entities;
using Hotel_Manager.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Manager.Infrastructure.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookingEntity, BookingDTO>()
                .ForMember(dest => dest.CheckInDate, opt => opt.MapFrom(src => src.CheckInDate.ToString("dd-MM-yyyy")))
                .ForMember(dest => dest.CheckOutDate, opt => opt.MapFrom(src => src.CheckOutDate.ToString("dd-MM-yyyy")));

            CreateMap<BookingDTO, BookingEntity>()
                .ForMember(dest => dest.CheckInDate, opt => opt.MapFrom(src => DateTime.ParseExact(src.CheckInDate, "dd-MM-yyyy", null)))
                .ForMember(dest => dest.CheckOutDate, opt => opt.MapFrom(src => DateTime.ParseExact(src.CheckOutDate, "dd-MM-yyyy", null)));

            CreateMap<GuestEntity, GuestDTO>().ReverseMap();
            CreateMap<RoomEntity, RoomDTO>().ReverseMap();

        }
    }
}
