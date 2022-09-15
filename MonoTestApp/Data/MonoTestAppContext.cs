using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MonoTestApp.Project.Models.ServiceModels;

namespace MonoTestApp.Data
{
    public class MonoTestAppContext : DbContext
    {
        public MonoTestAppContext (DbContextOptions<MonoTestAppContext> options)
            : base(options)
        {
        }

        public DbSet<VehicleMake> VehicleMake { get; set; } = default!;

        public DbSet<VehicleModel>? VehicleModel { get; set; }
    }
}
