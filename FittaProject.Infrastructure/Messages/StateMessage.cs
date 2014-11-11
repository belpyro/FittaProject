using TinyMessenger;

namespace FittaProject.Infrastructure.Messages
{
    public abstract class StateMessage : ITinyMessage
    {
        protected StateMessage(object sender)
        {
            Sender = sender;
        }

        public object Sender { get; private set; }
    }
}
