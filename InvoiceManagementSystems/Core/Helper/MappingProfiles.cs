using AutoMapper;
using InvoiceManagementSystems.Core.DTOs;
using InvoiceManagementSystems.Models;

namespace InvoiceManagementSystems.Core.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Admin, AdminDto>();
            CreateMap<AdminDto, Admin>();
        }
    }
}
