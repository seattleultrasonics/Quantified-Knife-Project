using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QKPRobot
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new QKPRobotGUI());
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Properties.Settings.Default.Save();
            }
        }
    }
}
