using AutoMapper;
using MonoTestApp.Project.Models.ServiceModels;
using MonoTestApp.Project.Models.ViewModels;

namespace MonoTestApp.Project.Models.Profiles
{
    public class VehicleModelProfile : Profile
    {
        public VehicleModelProfile()
        {
            CreateMap<VehicleModel, VehicleModelView>();
            CreateProjection<VehicleModel, VehicleModelView>().ForMember(vmv => vmv.MakerName, conf => conf.MapFrom(vm => vm.VehicleMake.Name));
        }
    }
}
