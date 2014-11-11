namespace FittaProject.Infrastructure.Messages
{
    public class OrbitInitializeMessage : StateMessage
    {
        public OrbitInitializeMessage(object sender, double minAltitude = 100000, double maxAltitude = 120000, string bodyName = "Duna")
            : base(sender)
        {
            MinAltitude = minAltitude;
            MaxAltitude = maxAltitude;
            BodyName = bodyName;
        }

        public double MinAltitude { get; private set; }

        public double MaxAltitude { get; private set; }

        public string BodyName { get; private set; }
    }
}