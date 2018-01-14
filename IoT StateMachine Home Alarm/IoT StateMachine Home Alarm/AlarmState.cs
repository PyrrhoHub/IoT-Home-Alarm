using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace IoT_StateMachine_Home_Alarm
{
    public sealed class AlarmState
    {
        AlarmModes alarmMode;        
        bool lighting;
        DeviceAlarmModes deviceAlarmMode;
        bool ownersHome;

        public AlarmModes AlarmMode
        {
            get
            {
                return alarmMode;
            }
            set
            {
                alarmMode = value;
            }
        }

        public bool OwnersHome
        {
            get
            {
                return ownersHome;
            }
            set
            {
                ownersHome = value;
            }
        }

        public bool Lighting
        {
            get
            {
                return lighting;
            }
            set
            {
                lighting = value;
            }
        }

        public DeviceAlarmModes DeviceAlarmMode
        {
            get
            {
                return deviceAlarmMode;
            }
            set
            {
                deviceAlarmMode = value;
            }
        }

        public AlarmState()
        {
            lighting = false;
            deviceAlarmMode = DeviceAlarmModes.Off;
            alarmMode = AlarmModes.Off;
        }

        public AlarmState GetState()
        {
            return (AlarmState)MemberwiseClone();
        }
    }
}
