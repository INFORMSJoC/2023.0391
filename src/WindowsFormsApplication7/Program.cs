using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication7
{
    static class Program
    {
        
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]

        static void Main()
        {
            //

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
           System.Windows.Application app = new System.Windows.Application();
            //app.MainWindow.Show(new Form1());
            //Window1 WPFWindow = new Window1();
            //app.StartupUri = new Uri("Window1.xaml", UriKind.Relative);
            //app.Run();
            //WPFWindo;
            Application.Run(new Form1());
        }
    }
}
