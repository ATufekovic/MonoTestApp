using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MonoTestApp.Project.Service;

namespace MonoTestApp.Data
{
    public class MonoTestAppContext : DbContext
    {
        public MonoTestAppContext (DbContextOptions<MonoTestAppContext> options)
            : base(options)
        {
        }

        public DbSet<MonoTestApp.Project.Service.VehicleMake> VehicleMake { get; set; } = default!;

        public DbSet<MonoTestApp.Project.Service.VehicleModel>? VehicleModel { get; set; }
    }
}
