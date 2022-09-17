using AutoMapper;
using MonoTestApp.Project.Service.Models.ServiceModels;
using MonoTestApp.Project.Service.Models.ViewModels;

namespace MonoTestApp.Project.Service.Models.Profiles
{
    public class VehicleModelProfile : Profile
    {
        public VehicleModelProfile()
        {
            CreateProjection<VehicleModel, VehicleModelView>().ForMember(vmv => vmv.MakerName, conf => conf.MapFrom(vm => vm.VehicleMake.Name));
        }
    }
}
