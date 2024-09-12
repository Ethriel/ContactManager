using AutoMapper;
using ContactManager.Services.Model.DTO;
using ContactManagerDB.Model;

namespace ContactManager.Services.Model.Mapper
{
    public class ContactMapperProfile : Profile
    {
        public ContactMapperProfile()
        {
            CreateMap<Contact, ContactDto>()
                .ForMember(cd => cd.DateOfBirth, o => o.MapFrom(c => c.DateOfBirth.ToShortDateString()));


            CreateMap<ContactDto, Contact>()
                .ForMember(c => c.DateOfBirth, o => o.MapFrom(cd => DateTime.Parse(cd.DateOfBirth)));
        }
    }
}
