using System;
using System.Collections.Generic;
using System.Text;

namespace IoT_StateMachine_Home_Alarm
{
    public class StateEventArgs : EventArgs
    {
        public AlarmState State { get; private set; }
        public StateEventArgs(AlarmState state) { State = state; }
    }

    public class AlarmApi
    {
        AlarmController controller;

        public delegate void StateChangedHandler(object sender, StateEventArgs e);
        public event StateChangedHandler StateChanged;

        public AlarmApi()
        {
            controller = new AlarmController(this);
        }

        public void MovementDetected()
        {
            controller.MovementDetected();
        }

        public void DoorOpened()
        {
            controller.DoorOpened();
        }

        public void SetAlarmMode(bool mode, ActionScopes scope)
        {
            controller.SetAlarmMode(mode, scope);
        }
        
        internal void RaiseStateChanged(AlarmState state)
        {
            StateChanged?.Invoke(this, new StateEventArgs(state));
        }

        public void ForceUpdate()
        {
            controller.Update();
        }

        public AlarmState GetAlarmState()
        {
            return controller.GetAlarmState();
        }
    }
}
