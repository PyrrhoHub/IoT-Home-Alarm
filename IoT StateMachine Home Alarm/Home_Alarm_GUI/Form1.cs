using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IoT_StateMachine_Home_Alarm;

namespace Home_Alarm_GUI
{
    public partial class Form1 : Form, IAlarmApi
    {
        AlarmApi alarmApi;

        public Form1()
        {
            InitializeComponent();

            alarmApi = new AlarmApi(this);

            alarmApi.StateChanged += AlarmApi_StateChanged;

            alarmApi.ForceUpdate();
        }

        public event EventHandler ButtonPressed;
        public event EventHandler MovementDetected;
        public event EventHandler DoorOpened;

        private void AlarmApi_StateChanged(object sender, StateEventArgs e)
        {
            lblAlarm.Text = e.State.Alarm.ToString();
            lblBuzzer.Text = e.State.Buzzer.ToString();
            lblLighting.Text = e.State.Lighting.ToString();
            lblAlarmMode.Text = e.State.AlarmMode.ToString();
        }

        private void btnOpenDoor_Click(object sender, EventArgs e)
        {
            DoorOpened(this, new EventArgs());
        }

        private void btnMovement_Click(object sender, EventArgs e)
        {
            MovementDetected(this, new EventArgs());
        }

        private void btnAlarmSwitch_Click(object sender, EventArgs e)
        {
            ButtonPressed(this, new EventArgs());
        }
    }
}
