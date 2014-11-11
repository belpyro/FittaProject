using System.IO;
using System.Linq;
using FittaProject.Infrastructure;
using FittaProject.Infrastructure.Messages;
using TinyMessenger;
using UnityEngine;

namespace FittaProject.States
{
    [KSPAddon(KSPAddon.Startup.Flight, true)]
    public class SetupLocation :MonoBehaviour, IState
    {
        void Awake()
        {
            Register();
        }

        void OnDestroy()
        {
            Unregister();
        }

        private TinyMessageSubscriptionToken _token;

        public void Register()
        {
           _token = Messenger.Instance.Hub.Subscribe<OrbitInitializeMessage>(Execute);
        }

        public void Unregister()
        {
            Messenger.Instance.Hub.Unsubscribe<OrbitInitializeMessage>(_token);
        }

        /// <summary>
        /// create vessel on custom orbit
        /// </summary>
        public void Execute(OrbitInitializeMessage orbitInitializeMessage)
        {
            //todo move vessel config to right location
            var path = Path.Combine("Configs", "test.craft");

            const string vesselName = "Argo";

            //todo move params to config

            var protoVesselConfig = ConfigNode.Load(path);

            if (protoVesselConfig == null) return;

            var nodes = protoVesselConfig.GetNodes("PART");

            var body = FlightGlobals.Bodies.FirstOrDefault(x => x.bodyName.Contains(orbitInitializeMessage.BodyName));

            var orbit = Orbit.CreateRandomOrbitAround(body,
                body.Radius + orbitInitializeMessage.MinAltitude, body.Radius + orbitInitializeMessage.MaxAltitude);

            var vesselNode = ProtoVessel.CreateVesselNode(vesselName, VesselType.Ship, orbit, 0, nodes);

            HighLogic.CurrentGame.AddVessel(vesselNode);
        }
    }
}
