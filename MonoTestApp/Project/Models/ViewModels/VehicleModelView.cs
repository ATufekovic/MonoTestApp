using MonoTestApp.Project.Models;

namespace MonoTestApp.Project.Models.ViewModels
{
    //Only purpose is to be a container for displaying maker name alongside name and abbr
    public class VehicleModelView : IModel
    {
        public string Name { get; set; }
        public string Abbr { get; set; }
        public string MakerName { get; set; }

        public void PrintStatus()
        {
            Console.WriteLine("ViewModel for Model -> Name: " + Name + ", Abbr: " + Abbr + ".");
        }
    }
}
