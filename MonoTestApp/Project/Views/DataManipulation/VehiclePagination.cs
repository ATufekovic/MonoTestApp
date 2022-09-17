using MonoTestApp.Project.Service.Models.ServiceModels;
using MonoTestApp.Project.Service.Models.ViewModels;
using X.PagedList;

namespace MonoTestApp.Project.Views.DataManipulation
{
    public class VehiclePagination
    {
        public static Task<IPagedList<VehicleMake>> PageVehicleMakeAsync(IQueryable<VehicleMake> queryVehicleMake, int page, int pageSize)
        {
            var pagedList = queryVehicleMake.ToPagedListAsync(page, pageSize);
            return pagedList;
        }

        public static Task<IPagedList<VehicleMakeView>> PageVehicleMakeViewAsync(IQueryable<VehicleMakeView> queryVehicleMakeView, int page, int pageSize)
        {
            var pagedList = queryVehicleMakeView.ToPagedListAsync(page, pageSize);
            return pagedList;
        }

        public static Task<IPagedList<VehicleModel>> PageVehicleModelAsync(IQueryable<VehicleModel> queryVehicleModel, int page, int pageSize)
        {
            var pagedList = queryVehicleModel.ToPagedListAsync(page, pageSize);
            return pagedList;
        }

        public static Task<IPagedList<VehicleModelView>> PageVehicleModelViewAsync(IQueryable<VehicleModelView> queryVehicleModelView, int page, int pageSize)
        {
            var pagedList = queryVehicleModelView.ToPagedListAsync(page, pageSize);
            return pagedList;
        }
    }
}
