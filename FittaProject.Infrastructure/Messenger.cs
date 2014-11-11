using TinyMessenger;

namespace FittaProject.Infrastructure
{
    public class Messenger
    {
        private static Messenger _instance;

        public Messenger()
        {
            Hub = new TinyMessengerHub();
        }

        public static Messenger Instance {get { return _instance ?? (_instance = new Messenger()); }}

        public TinyMessengerHub Hub { get; private set; }
    }
}
