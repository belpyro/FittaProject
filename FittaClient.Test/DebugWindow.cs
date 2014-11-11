using FittaProject.Infrastructure;
using FittaProject.Infrastructure.Messages;
using PluginFramework;
using UnityEngine;

namespace FittaClient.Test
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    [WindowInitials(Caption = "Debug state", ClampToScreen = true, Visible = true)]
    public class DebugWindow: MonoBehaviourWindow
    {
        public override void DrawWindow(int id)
        {
            GUILayout.BeginVertical(GUILayout.ExpandHeight(true));

            if (GUILayout.Button("SetupLocation state"))
            {
                Messenger.Instance.Hub.Publish(new OrbitInitializeMessage(this));
            }

            GUILayout.EndVertical();
        }
    }
}
