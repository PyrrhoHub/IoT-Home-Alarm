using System;
using System.Collections.Generic;
using System.Text;

namespace IoT_StateMachine_Home_Alarm
{
    public enum DeviceAlarmModes
    {
        On, 
        Off,
        Activating,
    }

    public enum AlarmModes
    {
        Silent,
        Sirene,
        Off,
    }

    public enum ActionScopes
    {
        Mobile,
        Home,
    }
}
