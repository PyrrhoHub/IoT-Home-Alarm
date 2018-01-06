using System;
using System.Collections.Generic;
using System.Text;

namespace IoT_StateMachine_Home_Alarm
{
    internal class AlarmController
    {
        AlarmApi api;
        AlarmState state;

        internal AlarmController(AlarmApi api)
        {
            this.api = api;
            state = new AlarmState();

            Update();
        }

        internal void DoorOpened()
        {
            state.Buzzer = !state.Buzzer;
            Update();
        }

        internal void Update()
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
