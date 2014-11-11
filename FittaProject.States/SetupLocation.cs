using System.IO;
using System.Linq;
using FittaProject.Infrastructure;
using FittaProject.Infrastructure.Messages;
using TinyMessenger;
using UnityEngine;

namespace FittaProject.States
{
    [KSPAddon(KSPAddon.Startup.EveryScene, false)]
    public class SetupLocation : MonoBehaviour, IState
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
            _token.Dispose();
        }

        /// <summary>
        /// create vessel on custom orbit
        /// </summary>
        public void Execute(OrbitInitializeMessage orbitInitializeMessage)
        {
            //todo move vessel config to right location
            var gameDirectoryPath = GameDatabase.Instance.root.AllDirectories.First(x => x.type == UrlDir.DirectoryType.GameData).path;

            var path = Path.Combine("Configs", "test.craft");

            path = Path.Combine("FittaClient", path);

            var fullConfigPath = Path.Combine(gameDirectoryPath, path);

            const string vesselName = "Argo";

            //todo move params to config

            var protoVesselConfig = ConfigNode.Load(fullConfigPath);

            if (protoVesselConfig == null)
            {
                //todo logger
                Debug.LogWarning("Cannot load config");
                return;
            }

            var nodes = protoVesselConfig.GetNodes("PART");

            var body = FlightGlobals.Bodies.FirstOrDefault(x => x.bodyName.Contains(orbitInitializeMessage.BodyName));

            if (body == null)
            {
                Debug.LogWarning("Cannot load body");
                return;
            }

            var orbit = Orbit.CreateRandomOrbitAround(body,
                body.Radius + orbitInitializeMessage.MinAltitude, body.Radius + orbitInitializeMessage.MaxAltitude);

            var vesselNode = ProtoVessel.CreateVesselNode(vesselName, VesselType.Ship, orbit, 0, nodes);

            HighLogic.CurrentGame.AddVessel(vesselNode);
        }
    }
}
