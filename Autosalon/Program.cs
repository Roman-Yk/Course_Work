using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Autosalon
{
    class Program
    {
        static void Main(string[] args)
        {
            Salon salon = new Salon();
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(ConsoleExit);
            ProgramFlow flow = new ProgramFlow(salon);

            flow.Run();

            salon.SaveData();

            void ConsoleExit(object sender, EventArgs e)
            {
                salon.SaveData();
            }
        }
    }
}
