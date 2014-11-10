using System.IO;
using System.Linq;
using FittaProject.Infrastructure;
using UnityEngine;

namespace FittaProject.States
{
    public class SetupLocation : IState
    {
        /// <summary>
        /// create vessel on custom orbit
        /// </summary>
        public void Execute()
        {
            //todo move vessel config to right location
            string path = Path.Combine("Configs", "test.craft");

            string bodyName = "Mun";

            string vesselName = "Kuk";

            //todo move params to config
            double minAltitude = 100000, maxAltitude = 100000;

            var protoVesselConfig = ConfigNode.Load(path);

            if (protoVesselConfig == null) return;

            var nodes = protoVesselConfig.GetNodes("PART");

            var body = FlightGlobals.Bodies.FirstOrDefault(x => x.bodyName.Contains(bodyName));

            var orbit = Orbit.CreateRandomOrbitAround(body,
                body.Radius + minAltitude, body.Radius + maxAltitude);

            var vesselNode = ProtoVessel.CreateVesselNode(vesselName, VesselType.Ship, orbit, 0, nodes);

            HighLogic.CurrentGame.AddVessel(vesselNode);
        }
    }
}
