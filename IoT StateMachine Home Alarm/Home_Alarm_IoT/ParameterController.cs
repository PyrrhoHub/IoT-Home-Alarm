using Restup.Webserver.Attributes;
using Restup.Webserver.Models.Contracts;
using Restup.Webserver.Models.Schemas;

namespace Home_Alarm_IoT
{
    [RestController(InstanceCreationType.Singleton)]
    public class ParameterController
    {
        [UriFormat("/simpleparameter/{id}/property/{propName}")]
        public IGetResponse GetWithSimpleParameters(int id, string propName)
        {
            return new GetResponse(
              GetResponse.ResponseStatus.OK,
              new DataReceived() { ID = id, PropName = propName });
        }
    }
}