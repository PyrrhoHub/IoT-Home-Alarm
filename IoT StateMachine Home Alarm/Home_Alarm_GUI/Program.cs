using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using IoT_StateMachine_Home_Alarm;

namespace Home_Alarm_GUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AlarmApi api = new AlarmApi();
            Form1 form = new Form1(api);

            Application.Run(form);
        }
    }
}
