namespace FittaProject.Infrastructure.Messages
{
    public class CheckPointMessage : StateMessage
    {
        public CheckPointMessage(object sender, string bodyName, double maxAltitude = 200000) : base(sender)
        {
            BodyName = bodyName;
            MaxAltitude = maxAltitude;
        }

        public string BodyName { get; private set; }

        public double MaxAltitude { get; private set; }
    }
}