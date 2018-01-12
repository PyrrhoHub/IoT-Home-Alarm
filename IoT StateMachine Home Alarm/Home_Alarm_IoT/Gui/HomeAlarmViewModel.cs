using IoT_StateMachine_Home_Alarm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.Networking.Connectivity;
using Windows.UI.Core;

namespace Home_Alarm_IoT
{
    public class HomeAlarmViewModel : INotifyPropertyChanged
    {
        private string _alarmStatus;

        public string LocalIp
        {
            get
            {
                return GetLocalIp();
            }
        }

        public string AlarmStatus
        {
            get
            {
                return _alarmStatus;
            }
            private set
            {
                _alarmStatus = value;
                OnPropertyChangedAsync();
            }
        }

        private static string StringifyAlarmState(AlarmState state)
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine("Alarm Mode: \t" + state.AlarmMode.ToString());
            str.AppendLine("Alarm ON: \t" + state.Alarm.ToString());
            str.AppendLine("Buzzer ON: \t" + state.Buzzer.ToString());
            str.AppendLine("Lighting ON: \t" + state.Lighting.ToString());

            return str.ToString();
        }

        public ICommand SendCommand { get; private set; }

        public HomeAlarmViewModel()
        {
            HomeAlarmConnector.Instance.StateChanged += Instance_StateChanged;

            SendCommand = new DelegateCommand<string>(ToggleAlarmMode, _ => true);
        }

        private async void Instance_StateChanged(object sender, StateEventArgs e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                AlarmStatus = StringifyAlarmState(e.State);
            });
        }

        private void ToggleAlarmMode(string body)
        {
            AlarmModes mode;

            switch (HomeAlarmConnector.Instance.GetAlarmState().AlarmMode)
            {
                case AlarmModes.On:
                    mode = AlarmModes.Off;
                    break;
                case AlarmModes.Off:
                    mode = AlarmModes.On;
                    break;
                default:
                    mode = AlarmModes.Off;
                    break;
            }

            HomeAlarmConnector.Instance.SetAlarmMode(mode);
        }

        private void WriteToOutput(HttpWebResponse response, string readAll)
        {
            var outputStringBuilder = new StringBuilder();
            outputStringBuilder.AppendLine(response.ResponseUri.ToString());
            outputStringBuilder.AppendFormat("{0} {1}", ((int)response.StatusCode), response.StatusDescription).AppendLine();

            foreach (var header in response.Headers)
            {
                outputStringBuilder.AppendFormat("{0}: {1}", header, response.Headers[header.ToString()]).AppendLine();
            }
            outputStringBuilder.AppendLine();
            outputStringBuilder.Append(readAll);

            //Output = outputStringBuilder.ToString();
        }

        private string GetLocalIp()
        {
            var icp = NetworkInformation.GetInternetConnectionProfile();

            if (icp?.NetworkAdapter == null) return null;
            var hostname =
                NetworkInformation.GetHostNames()
                    .SingleOrDefault(
                        hn =>
                            hn.IPInformation?.NetworkAdapter != null && hn.IPInformation.NetworkAdapter.NetworkAdapterId
                            == icp.NetworkAdapter.NetworkAdapterId);

            // the ip address
            return hostname?.CanonicalName;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChangedAsync([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
