using Microsoft.EntityFrameworkCore;
using MonoTestApp.Data;
using MonoTestApp.Project.Service.Models.ServiceModels;

namespace MonoTestApp.Project.DevelopmentTools
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
                        Name = "Toyota",
                        Abbr = "TYT"
                    },
                    new VehicleMake
                    {
                        Name = "VolksWagen",
                        Abbr = "VW"
                    },
                    new VehicleMake
                    {
                        Name = "Bayeriche Motoren Werke",
                        Abbr = "BMW"
                    },
                    new VehicleMake
                    {
                        Name = "Lexus",
                        Abbr = "LXS"
                    },
                    new VehicleMake
                    {
                        Name = "Audi",
                        Abbr = "AUD"
                    },
                    new VehicleMake
                    {
                        Name = "Nissan",
                        Abbr = "NSN"
                    },
                    new VehicleMake
                    {
                        Name = "Peugeot",
                        Abbr = "PGT"
                    },
                    new VehicleMake
                    {
                        Name = "Mazda",
                        Abbr = "MZD"
                    },
                    new VehicleMake
                    {
                        Name = "Honda",
                        Abbr = "HND"
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
                                Name = i.ToString() + "_test model for maker: " + maker.Name,
                                Abbr = i.ToString() + "_TMFM: " + maker.Abbr,
                                VehicleMakeId = maker.Id
                            });
                    }
                    context.SaveChanges();
                }

            }
        }

    }
}
