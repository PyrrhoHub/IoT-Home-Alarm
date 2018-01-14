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
        /*
        <Page.DataContext>
            <local:HomeAlarmViewModel/>
        </Page.DataContext>
        */

        private string _alarmStatus;

        private string alarmMode;
        private string ownersHome;
        private string lighting;
        private string deviceAlarmMode;

        public string LocalIp
        {
            get
            {
                return GetLocalIp();
            }
        }

        public string AlarmMode
        {
            get
            {
                return alarmMode;
            }
            private set
            {
                alarmMode = value;
                OnPropertyChangedAsync();
            }
        }

        public string OwnersHome
        {
            get
            {
                return ownersHome;
            }
            private set
            {
                ownersHome = value;
                OnPropertyChangedAsync();
            }
        }

        public string Lighting
        {
            get
            {
                return lighting;
            }
            private set
            {
                lighting = value;
                OnPropertyChangedAsync();
            }
        }

        public string DeviceAlarmMode
        {
            get
            {
                return deviceAlarmMode;
            }
            private set
            {
                deviceAlarmMode = value;
                OnPropertyChangedAsync();
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

            str.AppendLine("DeviceAlarm Mode: \t" + state.DeviceAlarmMode.ToString());
            str.AppendLine("AlarmMode Mode: \t" + state.AlarmMode.ToString());
            str.AppendLine("Lighting ON: \t" + state.Lighting.ToString());

            return str.ToString();
        }

        public ICommand SendCommand { get; private set; }

        public ICommand CmdToggleLighting { get; private set; }

        public ICommand CmdToggleAlarmMode { get; private set; }

        public ICommand CmdMovementDetected { get; private set; }

        public ICommand CmdDoorOpened { get; private set; }

        public HomeAlarmViewModel()
        {
            HomeAlarmConnector.Instance.StateChanged += Instance_StateChanged;

            SendCommand = new DelegateCommand<string>(ToggleAlarmMode, _ => true);

            CmdToggleLighting = new DelegateCommand<string>(ToggleLighting, _ => true);
            CmdToggleAlarmMode = new DelegateCommand<string>(ToggleAlarmMode, _ => true);
            CmdMovementDetected = new DelegateCommand<string>(MovementDetected, _ => true);
            CmdDoorOpened = new DelegateCommand<string>(DoorOpened, _ => true);
        }

        private void MovementDetected(string obj)
        {
            HomeAlarmConnector.Instance.MovementDetected();
        }

        private void DoorOpened(string obj)
        {
            HomeAlarmConnector.Instance.DoorOpened();
        }

        private async void Instance_StateChanged(object sender, StateEventArgs e)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                AlarmMode = "AlarmMode: " + e.State.AlarmMode.ToString();
                OwnersHome = "OwnersHome: " + e.State.OwnersHome.ToString();
                Lighting = "Lighting: " + e.State.Lighting.ToString();
                DeviceAlarmMode = "DeviceAlarmMode: " + e.State.DeviceAlarmMode.ToString();
            });
        }

        private void ToggleLighting(string body)
        {
            bool mode = true;

            if (HomeAlarmConnector.Instance.GetAlarmState().Lighting)
            {
                mode = false;
            }

            HomeAlarmConnector.Instance.SetLighting(mode);
        }

        private void ToggleAlarmMode(string body)
        {
            bool mode;

            switch (HomeAlarmConnector.Instance.GetAlarmState().DeviceAlarmMode)
            {
                case DeviceAlarmModes.Off:
                    mode = true;
                    break;
                default:
                    mode = false;
                    break;
            }

            HomeAlarmConnector.Instance.SetAlarmMode(mode, ActionScopes.Home);
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
