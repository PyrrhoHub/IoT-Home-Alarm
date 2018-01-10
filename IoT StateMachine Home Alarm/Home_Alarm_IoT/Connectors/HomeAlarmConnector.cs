using IoT_StateMachine_Home_Alarm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home_Alarm_IoT
{
    public class HomeAlarmConnector
    {
        private static AlarmApi instance;

        private HomeAlarmConnector() { }

        public static AlarmApi Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AlarmApi();
                }
                return instance;
            }
        }        
    }
}
