using System;

namespace kstar.sharp.console
{
    //UDP Multicast must be supported if running in VM or Continers. Needs full access to network.
    //example kstar.console.exe --ip-192.168.1.50 --sqlite-"Data Source=c:\sqlite\inverter-data.db"

    internal class Program
    {
        private static int REFRESH_SECONDS = 30; //30 seconds is 2880 rows per day, and ~1mln rows per year. SQLite should handle a few billion no problem
        private static string IP_ADDRESS_INVERTER = "0.0.0.0";
        private static kstar.sharp.datacollect.Client client;

        private static string SQL_LITE_CONNECTION_STRING = "";

        private static void Main(string[] args)
        {
            Console.WriteLine("------------------------");

            parseArguments(args);

            //Initialise the client
            client = new sharp.datacollect.Client(IP_ADDRESS_INVERTER);



            //test database access
            using (var dbContext = new sharp.ef.InverterDataContext(SQL_LITE_CONNECTION_STRING))
            {
                kstar.sharp.Services.DbService db = new sharp.Services.DbService(dbContext);
                db.Get(DateTime.Now, DateTime.Now);

                Console.WriteLine("");
                Console.WriteLine("SQLite is accessible");
            }

            //Console.WriteLine("Using SQLite Database file : " + db.DatabasePath);
            //kstar.sharp.Services.DBService.EnsureDatabase();

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
            using (var dbContext = new sharp.ef.InverterDataContext(SQL_LITE_CONNECTION_STRING))
            {
                kstar.sharp.Services.DbService db = new sharp.Services.DbService(dbContext);

                db.Save(inverterDataModel);

                Console.Clear();
                Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " (localtime)");
                Console.WriteLine(inverterDataModel.PVData);
                Console.WriteLine(inverterDataModel.GridData);
                Console.WriteLine(inverterDataModel.LoadData);
                Console.WriteLine(inverterDataModel.BatteryData);
                Console.WriteLine(inverterDataModel.StatData);
            }
        }

        private static void parseArguments(string[] args)
        {

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].StartsWith("--ip-"))
                {
                    IP_ADDRESS_INVERTER = args[i].Replace("--ip-", "");
                    Console.WriteLine("Set IP address from parameter: " + IP_ADDRESS_INVERTER);
                }

                if (args[i].StartsWith("--sqlite-"))
                {
                    SQL_LITE_CONNECTION_STRING = args[i].Replace("--sqlite-", "");
                    Console.WriteLine("Set SQLite connection string from parameter: " + SQL_LITE_CONNECTION_STRING);
                }
            }
        }
    }
}
