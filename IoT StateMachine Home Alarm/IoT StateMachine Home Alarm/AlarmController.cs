using System;
using System.Collections.Generic;
using System.Text;

namespace IoT_StateMachine_Home_Alarm
{
    public class AlarmController
    {
        AlarmApi api;
        AlarmState state;

        public AlarmController(AlarmApi api)
        {
            this.api = api;
            state = new AlarmState();

            Update();
        }

        public void DoorOpened()
        {
            state.Buzzer = !state.Buzzer;
            Update();
        }

        public void Update()
        {
            api.RaiseStateChanged(state.GetState());
        }

        internal void MovementDetected()
        {
            state.Alarm = !state.Alarm;
            Update();
        }

        internal void SwitchAlarmMode()
        {
            state.AlarmMode = AlarmModes.On;
            Update();
        }
    }
}
