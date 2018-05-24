namespace Lab.SignalR.Server.Hubs
{
    public class MachineKey 
    {

        public MachineKey(string group, string name)
        {
            this.Group = group;
            this.Name = name;
        }

        public string Group { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var other = obj as MachineKey;

            if (other == null)
                return false;

            return Group.Equals(other.Group) && Name.Equals(other.Name);
        }

        public override int GetHashCode() {
            return this.Group.GetHashCode() ^ this.Name.GetHashCode();
        }
    }

}