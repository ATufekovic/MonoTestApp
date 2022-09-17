using AutoMapper;
using MonoTestApp.Project.Service.Models.ServiceModels;
using MonoTestApp.Project.Service.Models.ViewModels;
using System.Runtime.Intrinsics.X86;

namespace MonoTestApp.Project.Service.Models.Profiles
{
    public class VehicleMakeProfile : Profile
    {
        public VehicleMakeProfile()
        {
            CreateMap<VehicleMake, VehicleMakeView>();
        }
    }
}
