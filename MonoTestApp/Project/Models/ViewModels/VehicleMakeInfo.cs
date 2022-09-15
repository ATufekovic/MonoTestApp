using MonoTestApp.Project.Models;

namespace MonoTestApp.Project.Models.ViewModels
{
    public class VehicleMakeInfo : IMake
    {
        public string Name { get; set; }
        public string Abbr { get; set; }

        public void PrintStatus()
        {
            Console.WriteLine("ViewModel for Maker -> Name: " + Name + ", Abbr: " + Abbr + ".");
        }
    }
}
