using System.ComponentModel.DataAnnotations;

namespace MonoTestApp.Project.Models.ServiceModels
{
    public class VehicleModel : IModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-Z0-9]+[A-Za-z0-9\s]*$")]
        [Display(Name = "Vehicle model")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"[A-Z0-9]+$")]
        [Display(Name = "Vehicle abbreviation")]
        public string Abbr { get; set; }

        [Required]
        [Display(Name = "Vehicle maker")]
        public int VehicleMakeId { get; set; }

        //can't set up model binding if not set as "NULL" -> "?", since it throws an error on creation
        public virtual VehicleMake? VehicleMake { get; set; }

        public void PrintStatus()
        {
            Console.WriteLine("Model: " + Name + ", Abbr: " + Abbr + ", Maker ID: " + VehicleMakeId);
        }
    }
}
