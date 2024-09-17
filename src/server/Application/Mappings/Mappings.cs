using Application.DataDTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class Mappings : Profile
{
    public Mappings()
    {
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<Role, RoleDTO>().ReverseMap();
    }
}