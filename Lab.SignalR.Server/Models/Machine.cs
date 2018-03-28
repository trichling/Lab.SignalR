namespace Lab.SignalR.Server.Models
{

    public class Machine
    {

        public Machine(string group, string name)
        {
            Group = group;
            Name = name;
            SpeedMeterPerSecond = 0;
        }

        public string Group { get; }
        public string Name { get; }

        public double SpeedMeterPerSecond { get; set; }
    }

}