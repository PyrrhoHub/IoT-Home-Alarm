using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;

namespace IoT_StateMachine_Home_Alarm
{
    public class AlarmController
    {
        AlarmApi api;
        AlarmState state;

        int alarmTimeOutInSeconds = 5;
        bool preAlarmLightingState;
        TimeSpan timeActivatedAlarm;
        TimeSpan timeAlarmIsOn;
        TimeSpan timeLastDoorOpend;
        TimeSpan timeLastMovementDetected;

        BackgroundWorker bgwAlarmController;

        public AlarmController(AlarmApi api)
        {
            this.api = api;
            state = new AlarmState();

            preAlarmLightingState = false;

            bgwAlarmController = new BackgroundWorker();
            bgwAlarmController.DoWork += BgwAlarmController_DoWork;
            bgwAlarmController.WorkerSupportsCancellation = true;
            bgwAlarmController.RunWorkerAsync();

            Update();
        }

        ~AlarmController()
        {
            bgwAlarmController.CancelAsync();
        }

        internal void SetLighting(bool mode)
        {
            if (state.AlarmMode != AlarmModes.Sirene)
            {
                state.Lighting = mode;
                preAlarmLightingState = mode;
            }
            Update();
        }

        private void BgwAlarmController_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                switch (state.DeviceAlarmMode)
                {
                    case DeviceAlarmModes.On:
                        // owners home
                        if (state.OwnersHome)
                        {
                            // if movement than silent
                            if (TimeSpan.Compare(timeAlarmIsOn, timeLastMovementDetected) == -1)
                            {
                                state.AlarmMode = AlarmModes.Silent;
                            }

                            // if door open than sirene
                            if (TimeSpan.Compare(timeAlarmIsOn, timeLastDoorOpend) == -1)
                            {
                                preAlarmLightingState = state.Lighting;

                                state.AlarmMode = AlarmModes.Sirene;
                                state.Lighting = true;
                            }

                            // if silent for timeoutseconds than sirene
                            if (TimeSpan.Compare(timeLastMovementDetected.Add(new TimeSpan(0, 0, alarmTimeOutInSeconds)), DateTime.Now.TimeOfDay) == -1 && state.AlarmMode == AlarmModes.Silent)
                            {
                                preAlarmLightingState = state.Lighting;

                                state.AlarmMode = AlarmModes.Sirene;
                                state.Lighting = true;
                            }
                        }
                        // owners not home
                        else
                        {
                            // if movement than silent
                            if (TimeSpan.Compare(timeAlarmIsOn, timeLastDoorOpend) == -1)
                            {
                                state.AlarmMode = AlarmModes.Silent;
                            }

                            // if movement than sirene
                            if (TimeSpan.Compare(timeAlarmIsOn, timeLastMovementDetected) == -1)
                            {
                                preAlarmLightingState = state.Lighting;

                                state.AlarmMode = AlarmModes.Sirene;
                                state.Lighting = true;
                            }

                            // if silent for timeoutseconds than sirene
                            if (TimeSpan.Compare(timeLastDoorOpend.Add(new TimeSpan(0, 0, alarmTimeOutInSeconds)), DateTime.Now.TimeOfDay) == -1 && state.AlarmMode == AlarmModes.Silent)
                            {
                                preAlarmLightingState = state.Lighting;

                                state.AlarmMode = AlarmModes.Sirene;
                                state.Lighting = true;
                            }
                        }

                        break;
                    case DeviceAlarmModes.Activating:
                        TimeSpan timeEstimateAlarmIsOn = timeActivatedAlarm.Add(new TimeSpan(0, 0, alarmTimeOutInSeconds));

                        // check if door opend if so than owners not home
                        if (TimeSpan.Compare(timeLastDoorOpend, timeActivatedAlarm) == 1 && TimeSpan.Compare(timeLastDoorOpend, timeEstimateAlarmIsOn) == -1)
                        {
                            state.OwnersHome = false;
                        }

                        // if activating time has passed, alarm is on
                        if (TimeSpan.Compare(timeEstimateAlarmIsOn, DateTime.Now.TimeOfDay) == -1)
                        {
                            state.DeviceAlarmMode = DeviceAlarmModes.On;
                            timeAlarmIsOn = DateTime.Now.TimeOfDay;
                        }

                        break;
                    default:
                        break;
                }

                Update();
                Thread.Sleep(100);
            }
        }

        public void DoorOpened()
        {
            if (!(state.AlarmMode == AlarmModes.Silent) || state.OwnersHome)
            {
                timeLastDoorOpend = DateTime.Now.TimeOfDay;
            }
        }

        public void MovementDetected()
        {
            if (!(state.AlarmMode == AlarmModes.Silent) || !state.OwnersHome)
            {
                timeLastMovementDetected = DateTime.Now.TimeOfDay;
            }
        }

        public AlarmState GetAlarmState()
        {
            return state.GetState();
        }

        public void SetAlarmMode(bool mode, ActionScopes scope)
        {
            if (mode)
            {
                if (state.DeviceAlarmMode == DeviceAlarmModes.Off)
                {
                    if (scope == ActionScopes.Home)
                    {
                        // turn alarm on activating mode, asume owners at home.
                        state.DeviceAlarmMode = DeviceAlarmModes.Activating;
                        state.OwnersHome = true;
                        timeActivatedAlarm = DateTime.Now.TimeOfDay;
                    }
                    else
                    {
                        // asume owners not at home.
                        state.DeviceAlarmMode = DeviceAlarmModes.On;
                        state.OwnersHome = false;
                        timeAlarmIsOn = DateTime.Now.TimeOfDay;
                    }
                }
            }
            else
            {
                if (state.DeviceAlarmMode != DeviceAlarmModes.Off)
                {
                    state.DeviceAlarmMode = DeviceAlarmModes.Off;
                    state.AlarmMode = AlarmModes.Off;
                    state.Lighting = preAlarmLightingState;
                }
            }

            Update();
        }

        public void Update()
        {
            api.RaiseStateChanged(state.GetState());
        }
    }
}
