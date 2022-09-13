namespace MonoTestApp.Project.Service
{
    //Only purpose is to be a container for displaying maker name alongside name and abbr
    public class VehicleModelInfo
    {
        public string name { get; set; }
        public string abbr { get; set; }
        public string makerName { get; set; }
        public int makerId { get; set; }
    }
}
