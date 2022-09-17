using System.ComponentModel.DataAnnotations;

namespace MonoTestApp.Project.Service.Models.ServiceModels
{
    public class VehicleMake : IMake
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-Z0-9]+[A-Za-z0-9\s]*$")]
        [Display(Name = "Vehicle maker")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"[A-Z0-9]+$")]
        [Display(Name = "Maker Abbreviation")]
        public string Abbr { get; set; }

        //maker can exists without models in its line, thus "NULL" -> "?"
        public virtual List<VehicleModel>? VehicleModels { get; set; }

        public void PrintStatus()
        {
            Console.WriteLine("Maker: " + Name + ", Abbr: " + Abbr);
        }
    }
}
