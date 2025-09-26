using System;
using System.Data.Entity;
using System.Windows.Forms;

namespace EFcrud2
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Database.SetInitializer(new AppDbInitializer());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new EFCRUD());
        }
    }
}
