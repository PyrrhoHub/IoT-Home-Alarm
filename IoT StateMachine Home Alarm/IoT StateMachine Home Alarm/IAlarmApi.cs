using System;
using System.Collections.Generic;
using System.Text;

namespace IoT_StateMachine_Home_Alarm
{
    public interface IAlarmApi
    {
        event EventHandler ButtonPressed;
        event EventHandler MovementDetected;
        event EventHandler DoorOpened;
    }
}
