using System.ComponentModel.DataAnnotations;

namespace MonoTestApp.Project.Service
{
    public class VehicleMake : IMake
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-Z0-9]+[A-Za-z0-9\s]*$")]
        [Display(Name = "Vehicle maker")]
        public string name { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"[A-Z0-9]+$")]
        [Display(Name = "Maker Abbreviation")]
        public string abbr { get; set; }

        //maker can exists without models in its line, thus "NULL" -> "?"
        public List<VehicleModel>? VehicleModels { get; set; }

        public void PrintStatus()
        {
            Console.WriteLine("Maker: " + name + ", Abbr: " + abbr);
        }
    }
}
