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
using Windows.Networking.Connectivity;
using Windows.UI.Core;

namespace Home_Alarm_IoT
{
    public class HomeAlarmViewModel : INotifyPropertyChanged
    {
        private string _alarmStatus;

        private readonly Uri _baseUri;

        private bool isNotSendingRequest;
        private string _output;

        private string _relativeUri;
        private string _method;
        private string _body;
        private Uri _webViewUri;

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

        public bool IsNotSendingRequest
        {
            get { return isNotSendingRequest; }
            private set
            {
                isNotSendingRequest = value;
                OnPropertyChangedAsync();
                IsBodyAcceptingInput = true;
            }
        }

        public bool IsBodyAcceptingInput
        {
            get { return IsNotSendingRequest && Method != "GET"; }
            private set { OnPropertyChangedAsync(); } // just to notify that the property has changed
        }

        public string Output
        {
            get { return _output; }
            private set { _output = value; OnPropertyChangedAsync(); }
        }

        public string RelativeUri
        {
            get { return _relativeUri; }
            set { _relativeUri = value; OnPropertyChangedAsync(); }
        }

        public Uri WebViewUri
        {
            get { return _webViewUri; }
            set { _webViewUri = value; OnPropertyChangedAsync(); }
        }

        public string Method
        {
            get { return _method; }
            set
            {
                _method = value;
                OnPropertyChangedAsync();
                IsBodyAcceptingInput = true;
            }
        }

        public string Body
        {
            get { return _body; }
            set { _body = value; OnPropertyChangedAsync(); }
        }

        public ICommand SendCommand { get; private set; }

        public HomeAlarmViewModel()
        {
            HomeAlarmConnector.Instance.StateChanged += Instance_StateChanged;

            SendCommand = new DelegateCommand<string>(ToggleAlarmMode, _ => IsNotSendingRequest);

            _baseUri = new Uri("http://127.0.0.1:8800/");
            Method = "GET";
            RelativeUri = "/api/homealarm";
            WebViewUri = GetAbsoluteUri();
            IsNotSendingRequest = true;
        }

        private void Instance_StateChanged(object sender, StateEventArgs e)
        {
            AlarmStatus = StringifyAlarmState(HomeAlarmConnector.Instance.GetAlarmState());
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

        private async void SendRequest(string body)
        {
            IsNotSendingRequest = false;

            try
            {
                await SendRequest();
            }
            catch (Exception ex)
            {
                Output = ex.ToString();
            }
            finally
            {
                IsNotSendingRequest = true;
            }
        }

        private async Task SendRequest()
        {
            var requestUri = GetAbsoluteUri();
            WebViewUri = requestUri;

            var webRequest = WebRequest.CreateHttp(requestUri);
            webRequest.Accept = "application/json";
            webRequest.Method = Method;

            if (webRequest.Method != "GET" && !string.IsNullOrWhiteSpace(Body))
            {
                webRequest.ContentType = "application/json";

                var requestStream = await webRequest.GetRequestStreamAsync();
                using (var streamWriter = new StreamWriter(requestStream))
                {
                    await streamWriter.WriteAsync(Body);
                }
            }

            var response = await webRequest.GetResponseAsync();
            var responseStream = response.GetResponseStream();

            using (var streamReader = new StreamReader(responseStream))
            {
                var readAll = await streamReader.ReadToEndAsync();
                WriteToOutput((HttpWebResponse)response, readAll);
            }
            response.Dispose();
        }

        private Uri GetAbsoluteUri()
        {
            return new Uri(_baseUri + RelativeUri.TrimStart('/'));
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

            Output = outputStringBuilder.ToString();
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
