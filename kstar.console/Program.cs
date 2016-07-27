using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace kstar.console
{
    class Program
    {

        static int REFRESH_SECONDS = 30; //30 seconds is 2880 rows per day, and ~1mln rows per year. SQLite should handle a few billion no problem
        static string IP_ADDRESS_INVERTER = "0.0.0.0";
        static kstar.sharp.datacollect.Client client;



        static void Main(string[] args)
        {
            Console.WriteLine("------------------------");

            parseArguments(args);

            //Initialise the client
            client = new sharp.datacollect.Client(IP_ADDRESS_INVERTER);

            kstar.sharp.Services.DBService db = new sharp.Services.DBService();
            Console.WriteLine("Using SQLite Database file : " + db.DatabasePath);
            kstar.sharp.Services.DBService.EnsureDatabase();

            //receiver_Communication = new UdpClient(PORT_UDP_Communication);

            // Display some information
            Console.WriteLine("");
            Console.WriteLine("Starting UDP Broadcast");// on port: " + receiverPort);
            Console.WriteLine("-------------------------------\n");

            while (!client.BroadcastRequest_ResponseRecieved)
            {
                Console.WriteLine("Sending Broadcast Request to " + IP_ADDRESS_INVERTER);
                client.SendBroadcastRequest();
                System.Threading.Thread.Sleep(2000);
            }
            IP_ADDRESS_INVERTER = client.IPAddressInverter;
            Console.WriteLine("IP Address set to " + IP_ADDRESS_INVERTER);
            Console.WriteLine("");


            // Display some information
            Console.WriteLine("Starting UDP Data");// on port: " + receiverPort);
            Console.WriteLine("-------------------------------\n");
            client.DataRecieved += new kstar.sharp.datacollect.DataRecievedEventHandler(DataRecievedUpdateConsole);
            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
            {
                client.SendDataRequest();

                System.Threading.Thread.Sleep(REFRESH_SECONDS * 1000);
                Console.WriteLine("Send");
            }
        }


        private static void DataRecievedUpdateConsole(kstar.sharp.domain.Models.InverterData inverterDataModel)
        {
            kstar.sharp.Services.DBService db = new sharp.Services.DBService();
            db.Save(inverterDataModel);

            Console.Clear();
            Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " (localtime)");
            Console.WriteLine(inverterDataModel.PVData);
            Console.WriteLine(inverterDataModel.GridData);
            Console.WriteLine(inverterDataModel.LoadData);
            Console.WriteLine(inverterDataModel.BatteryData);
            Console.WriteLine(inverterDataModel.StatData);
        }


        static void parseArguments(string[] args)
        {

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].StartsWith("--ip-"))
                {
                    IP_ADDRESS_INVERTER = args[i].Replace("--ip-", "");
                    Console.WriteLine("Set IP address from parameter: " + IP_ADDRESS_INVERTER);
                }
            }
        }


    }
}
