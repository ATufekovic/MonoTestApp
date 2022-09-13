using System.ComponentModel.DataAnnotations;

namespace MonoTestApp.Project.Service
{
    public class VehicleModel : IModel
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-Z0-9]+[A-Za-z0-9\s]*$")]
        [Display(Name = "Vehicle model")]
        public string name { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"[A-Z0-9]+$")]
        [Display(Name = "Vehicle abbreviation")]
        public string abbr { get; set; }

        [Required]
        [Display(Name = "Vehicle maker")]
        public int vehicleMakeId { get; set; }

        //can't set up model binding if not set as "NULL" -> "?", since it throws an error on creation
        public virtual VehicleMake? vehicleMake { get; set; }

        public void PrintStatus()
        {
            Console.WriteLine("Model: " + name + ", Abbr: " + abbr + ", Maker ID: " + vehicleMakeId);
        }
    }
}
