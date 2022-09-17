using MonoTestApp.Project.Service.Models.ServiceModels;

namespace MonoTestApp.Project.Views.DataManipulation
{
    public class VehicleFilter
    {
        public static IQueryable<VehicleMake> SearchVehicleMake(IQueryable<VehicleMake> vehicleMakeQuery, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleMakeQuery = vehicleMakeQuery.Where(
                    vm => vm.Name.Contains(searchString) ||
                    vm.Abbr.Contains(searchString)
                    );
            }
            return vehicleMakeQuery;
        }

        public static IQueryable<VehicleModel> SearchVehicleModel(IQueryable<VehicleModel> vehicleModelQuery, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleModelQuery = vehicleModelQuery.Where(
                    vm => vm.Name.Contains(searchString) ||
                    vm.Abbr.Contains(searchString) ||
                    vm.VehicleMake.Name.Contains(searchString) ||
                    vm.VehicleMake.Abbr.Contains(searchString)
                    );
            }
            return vehicleModelQuery;
        }
    }
}
