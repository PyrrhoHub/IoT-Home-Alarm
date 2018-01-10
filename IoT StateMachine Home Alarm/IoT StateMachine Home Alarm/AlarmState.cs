using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace IoT_StateMachine_Home_Alarm
{
    public sealed class AlarmState
    {
        bool alarm;
        bool buzzer;
        bool lighting;
        AlarmModes alarmMode;

        public bool Alarm
        {
            get
            {
                return alarm;
            }
            set
            {
                alarm = value;
            }
        }

        public bool Buzzer
        {
            get
            {
                return buzzer;
            }
            set
            {
                buzzer = value;
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

        public AlarmState()
        {
            alarm = false;
            buzzer = false;
            lighting = false;
            alarmMode = AlarmModes.Off;
        }

        public AlarmState GetState()
        {
            return (AlarmState)MemberwiseClone();
        }
    }
}
