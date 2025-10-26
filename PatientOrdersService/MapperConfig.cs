using AutoMapper;
using PatientOrdersService.Models;
using PatientOrdersService.Common.Dtos;

namespace PatientOrdersService.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, PatientDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<CreateOrderReq, Order>();
            CreateMap<UpdateOrderReq, Order>();
        }
    }
}
