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
        IAlarmApi gui;

        public delegate void StateChangedHandler(object sender, StateEventArgs e);
        public event StateChangedHandler StateChanged;

        public AlarmApi(IAlarmApi gui)
        {
            controller = new AlarmController(this);
            this.gui = gui;

            gui.ButtonPressed += Gui_ButtonPressed;
            gui.DoorOpened += Gui_DoorOpened;
            gui.MovementDetected += Gui_MovementDetected;
        }

        private void Gui_MovementDetected(object sender, EventArgs e)
        {
            controller.MovementDetected();
        }

        private void Gui_DoorOpened(object sender, EventArgs e)
        {
            controller.DoorOpened();
        }

        private void Gui_ButtonPressed(object sender, EventArgs e)
        {
            controller.SwitchAlarmMode();
        }
        
        internal void RaiseStateChanged(AlarmState state)
        {
            StateChanged?.Invoke(this, new StateEventArgs(state));
        }

        public void ForceUpdate()
        {
            controller.Update();
        }
    }
}
