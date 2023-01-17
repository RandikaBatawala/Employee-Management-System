using System;
using System.Windows.Forms;

namespace EmpManagementSystem
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PL_AllEmp());
            //Application.Run(new PL_AllEmpDataSet());
            
        }
    }
}
