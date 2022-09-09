using Microsoft.EntityFrameworkCore;
using MonoTestApp.Data;

namespace MonoTestApp.Project.Service.DevelopmentTools
{
    public class DatabaseSeeder
    {
        public static void InitialiseSeedingMakes(IServiceProvider serviceProvider)
        {
            using (var context = new MonoTestAppContext(serviceProvider.GetRequiredService<DbContextOptions<MonoTestAppContext>>()))
            {
                if (context == null || context.VehicleMake == null)
                {
                    throw new ArgumentNullException("Null -> MonoTestAppContext");
                }

                if (context.VehicleMake.Any())
                {
                    return;
                }

                context.VehicleMake.AddRange(
                    new VehicleMake
                    {
                        name = "Toyota",
                        abbr = "TYT"
                    },
                    new VehicleMake
                    {
                        name = "VolksWagen",
                        abbr = "VW"
                    },
                    new VehicleMake
                    {
                        name = "Bayeriche Motoren Werke",
                        abbr = "BMW"
                    },
                    new VehicleMake
                    {
                        name = "Lexus",
                        abbr = "LXS"
                    },
                    new VehicleMake
                    {
                        name = "Audi",
                        abbr = "AUD"
                    },
                    new VehicleMake
                    {
                        name = "Nissan",
                        abbr = "NSN"
                    },
                    new VehicleMake
                    {
                        name = "Peugeot",
                        abbr = "PGT"
                    },
                    new VehicleMake
                    {
                        name = "Mazda",
                        abbr = "MZD"
                    },
                    new VehicleMake
                    {
                        name = "Honda",
                        abbr = "HND"
                    }
                );

                context.SaveChanges();
            }
        }

        public static void InitialiseSeedingModels(IServiceProvider serviceProvider)
        {
            using (var context = new MonoTestAppContext(serviceProvider.GetRequiredService<DbContextOptions<MonoTestAppContext>>()))
            {
                if (context == null || context.VehicleModel == null)
                {
                    throw new ArgumentNullException("Null -> MonoTestAppContext");
                }

                if (context.VehicleModel.Any())
                {
                    return;
                }

                //simple inserter, can't be bothered to add that many models to so many makers by hand
                var makers = context.VehicleMake.ToList();
                foreach (var maker in makers)
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        context.VehicleModel.Add(
                            new VehicleModel
                            {
                                name = i.ToString() + "_test model for maker: " + maker.name,
                                abbr = i.ToString() + "_TMFM: " + maker.abbr,
                                vehicleMakeId = maker.id
                            });
                    }
                    context.SaveChanges();
                }

            }
        }

    }
}
