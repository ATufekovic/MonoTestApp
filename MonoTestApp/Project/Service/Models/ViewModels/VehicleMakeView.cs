using MonoTestApp.Project.Service.Models;

namespace MonoTestApp.Project.Service.Models.ViewModels
{
    public class VehicleMakeView : IMake
    {
        public string Name { get; set; }
        public string Abbr { get; set; }

        public void PrintStatus()
        {
            Console.WriteLine("ViewModel for Maker -> Name: " + Name + ", Abbr: " + Abbr + ".");
        }
    }
}
