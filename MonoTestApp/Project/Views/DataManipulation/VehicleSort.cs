using MonoTestApp.Project.Service.Models.ServiceModels;

namespace MonoTestApp.Project.Views.DataManipulation
{
    public class VehicleSort
    {
        public static IQueryable<VehicleMake> SortVehicleMake(IQueryable<VehicleMake> vehicleMakeQuery, string sortBy)
        {
            switch (sortBy)
            {
                case "name_desc":
                    vehicleMakeQuery = vehicleMakeQuery.OrderByDescending(vm => vm.Name);
                    break;
                case "name":
                    vehicleMakeQuery = vehicleMakeQuery.OrderBy(vm => vm.Name);
                    break;
                case "abbr_desc":
                    vehicleMakeQuery = vehicleMakeQuery.OrderByDescending(vm => vm.Abbr);
                    break;
                case "abbr":
                    vehicleMakeQuery = vehicleMakeQuery.OrderBy(vm => vm.Abbr);
                    break;
                default:
                    vehicleMakeQuery = vehicleMakeQuery.OrderBy(vm => vm.Name);
                    break;
            }

            return vehicleMakeQuery;
        }

        public static IQueryable<VehicleModel> SortVehicleModel(IQueryable<VehicleModel> vehicleModelQuery, string sortBy)
        {
            switch (sortBy)
            {
                case "name_desc":
                    vehicleModelQuery = vehicleModelQuery.OrderByDescending(vm => vm.Name);
                    break;
                case "name":
                    vehicleModelQuery = vehicleModelQuery.OrderBy(vm => vm.Name);
                    break;
                case "abbr_desc":
                    vehicleModelQuery = vehicleModelQuery.OrderByDescending(vm => vm.Abbr);
                    break;
                case "abbr":
                    vehicleModelQuery = vehicleModelQuery.OrderBy(vm => vm.Abbr);
                    break;
                default:
                    vehicleModelQuery = vehicleModelQuery.OrderBy(vm => vm.Name);
                    break;
            }

            return vehicleModelQuery;
        }
    }
}
