using IoT_StateMachine_Home_Alarm;
using Restup.Webserver.Attributes;
using Restup.Webserver.Models.Contracts;
using Restup.Webserver.Models.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home_Alarm_IoT
{
    [RestController(InstanceCreationType.PerCall)]
    public sealed class HomeAlarmController
    {
        [UriFormat("/homealarm")]
        public IPostResponse PostAlarmMode()
        {
            return new PostResponse(
                PostResponse.ResponseStatus.Created,
                "newlocation",
                new AlarmState());
        }

        [UriFormat("/homealarm")]
        public IGetResponse GetAlarmState()
        {
            return new GetResponse(
                GetResponse.ResponseStatus.OK,
                new AlarmState());
        }
    }
}
