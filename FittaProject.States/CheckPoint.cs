using System;
using FittaProject.Infrastructure;
using FittaProject.Infrastructure.Messages;
using TinyMessenger;
using UnityEngine;

namespace FittaProject.States
{
    public class CheckPoint : PartModule, IState
    {
        private TinyMessageSubscriptionToken _token;
        private CheckPointMessage _message;

        public override void OnStart(StartState state)
        {
            base.OnStart(state);
            Register();
        }

        void OnDestroy()
        {
            // ReSharper disable once DelegateSubtraction
            vessel.orbitDriver.OnReferenceBodyChange -= OnReferenceBodyChange;
            Unregister();
        }

        public void Register()
        {
            _token = Messenger.Instance.Hub.Subscribe<CheckPointMessage>(Executed);
        }

        public void Unregister()
        {
            _token.Dispose();
        }

        private void Executed(CheckPointMessage obj)
        {
            ScreenMessages.PostScreenMessage("Register message", 3f,
                ScreenMessageStyle.UPPER_RIGHT);
            vessel.orbitDriver.OnReferenceBodyChange += OnReferenceBodyChange;
            _message = obj;
        }

        private void OnReferenceBodyChange(CelestialBody body)
        {
            //todo remove
            Debug.LogWarning("Body reference changed" + body.name);
            ScreenMessages.PostScreenMessage(string.Format("Body reference changed to {0}", body), 7f,
                ScreenMessageStyle.UPPER_CENTER);

            if (!body.name.Equals(_message.BodyName, StringComparison.InvariantCultureIgnoreCase)) return;

            //if (!(vessel.orbit.semiMajorAxis <= _message.MaxAltitude)) return;

            Messenger.Instance.Hub.Publish(new OrbitInitializeMessage(this, maxAltitude:_message.MaxAltitude, bodyName:_message.BodyName));
            enabled = false;
        }
    }
}
