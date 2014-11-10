using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FittaProject.Infrastructure;
using FittaProject.States;
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
                IState state = new SetupLocation();
                state.Execute();
            }

            GUILayout.EndVertical();
        }
    }
}
