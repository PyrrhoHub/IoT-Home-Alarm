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
        public IPutResponse UpdateAlarmMode([FromContent]DataReceived data)
        {
            HomeAlarmConnector.Instance.SetAlarmMode(data.AlarmMode);

            return new PutResponse(
                PutResponse.ResponseStatus.OK);
        }

        [UriFormat("/homealarm")]
        public IGetResponse GetAlarmState()
        {
            return new GetResponse(
                GetResponse.ResponseStatus.OK,
                HomeAlarmConnector.Instance.GetAlarmState());
        }
    }
}
