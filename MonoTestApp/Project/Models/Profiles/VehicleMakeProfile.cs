using AutoMapper;
using MonoTestApp.Project.Models.ServiceModels;
using MonoTestApp.Project.Models.ViewModels;
using System.Runtime.Intrinsics.X86;

namespace MonoTestApp.Project.Models.Profiles
{
    public class VehicleMakeProfile : Profile
    {
        public VehicleMakeProfile()
        {
            CreateMap<VehicleMake, VehicleMakeView>();
        }
    }
}
