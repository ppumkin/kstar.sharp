﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;

namespace kstar.sharp.console
{
    //UDP Multicast must be supported if running in VM or Continers. Needs full access to network.
    //example kstar.console.exe --ip-192.168.1.50 --sqlite-"Data Source=c:\sqlite\inverter-data.db"

    internal class Program
    {
        private static int REFRESH_SECONDS = 10; //30; //30 seconds is 2880 rows per day, and ~1mln rows per year. SQLite should handle a few billion no problem
        private static string IP_ADDRESS_INVERTER = "0.0.0.0";
        private static kstar.sharp.datacollect.Client client;

        private static string SQL_LITE_CONNECTION_STRING = "";
        private static string MQTT_CONNECTION_STRING = "";


        private static IMqttClient mqttClient;

        private static async Task Main(string[] args)
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


            Console.WriteLine("Configuring MQTT");
            Console.WriteLine("-------------------------------\n");
            await ConfigureAndConnectMqtt();
            Console.WriteLine("");

            // Display some information
            Console.WriteLine("Starting UDP Data");// on port: " + receiverPort);
            Console.WriteLine("-------------------------------\n");
            client.DataRecieved += new kstar.sharp.datacollect.DataRecievedEventHandler(DataRecievedUpdateConsole);
            //while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)) //does not work in docker!

            nextDbSaveTime = DateTime.Now.AddSeconds(30);

            while (true)  // ctrl+c or sigterm kills this
            {
                try
                {
                    client.SendDataRequest();

                }
                catch (Exception x)
                {
                    Console.WriteLine("SendDataRequest FAILED - " + x.Message);

                }

                await Task.Delay(REFRESH_SECONDS * 1000);

                Console.WriteLine("Send");
            }
        }


        private static async Task ConfigureAndConnectMqtt()
        {
            var factory = new MqttFactory();

            mqttClient = factory.CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(MQTT_CONNECTION_STRING, 1883) // Port is optional
                .WithClientId("kstar.sharp.console")
                .WithCredentials("mqtt", "mqtt")
                .Build();

            try
            {
                await mqttClient.ConnectAsync(options, CancellationToken.None);

            }
            catch (Exception x)
            {
                Console.WriteLine("Could not connect to MQTT - " + x.Message);
            }

            //mqttClient.UseDisconnectedHandler(async e =>
            //{
            //    Console.WriteLine("### DISCONNECTED FROM SERVER ###");
            //    await Task.Delay(TimeSpan.FromSeconds(5));

            //    try
            //    {
            //        await mqttClient.ConnectAsync(options, CancellationToken.None); // Since 3.0.5 with CancellationToken
            //    }
            //    catch
            //    {
            //        Console.WriteLine("### RECONNECTING FAILED ###");
            //    }
            //});

        }

        private static async Task PublishSensorTopic(domain.Models.InverterData inverterDataModel)
        {
            Console.WriteLine("Publishing MQTT Topics");

            if (!mqttClient.IsConnected)
                await ConfigureAndConnectMqtt();

            if (!mqttClient.IsConnected)
                return;

            try
            {
                var messages = new List<MqttApplicationMessage>();
                messages.Add(CreateMqttMessage("sensor/inverter/pvpower", inverterDataModel.PVData.PVPower.ToString()));
                messages.Add(CreateMqttMessage("sensor/inverter/grid", inverterDataModel.GridData.GridPower.ToString()));
                messages.Add(CreateMqttMessage("sensor/inverter/load", inverterDataModel.LoadData.LoadPower.ToString()));
                messages.Add(CreateMqttMessage("sensor/inverter/temp", inverterDataModel.StatData.InverterTemperature.ToString()));
                messages.Add(CreateMqttMessage("sensor/inverter/etoday", inverterDataModel.StatData.EnergyToday.ToString()));


                await mqttClient.PublishAsync(messages);
            }
            catch (Exception x)
            {

                Console.WriteLine($"Publishing MQTT Topics - FAILED - ${x.Message}");
            }

        }

        private static MqttApplicationMessage CreateMqttMessage(string topic, string value)
        {
            return new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(value)
            .WithAtMostOnceQoS()
            .Build();
        }

        private static DateTime nextDbSaveTime;

        private static void DataRecievedUpdateConsole(domain.Models.InverterData inverterDataModel)
        {
            Console.Clear();

            Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " (localtime)");
            Console.WriteLine(inverterDataModel.PVData);
            Console.WriteLine(inverterDataModel.GridData);
            Console.WriteLine(inverterDataModel.LoadData);
            Console.WriteLine(inverterDataModel.BatteryData);
            Console.WriteLine(inverterDataModel.StatData);
            Console.WriteLine(string.Empty);

            PublishSensorTopic(inverterDataModel).GetAwaiter().GetResult();

            if (DateTime.Now >= nextDbSaveTime)
            {
                Console.WriteLine("Saving DB Entry");
                nextDbSaveTime = DateTime.Now.AddSeconds(30);

                using (var dbContext = new ef.InverterDataContext(SQL_LITE_CONNECTION_STRING))
                {
                    Services.DbService db = new Services.DbService(dbContext);

                    db.Save(inverterDataModel);
                }

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

                if (args[i].StartsWith("--mqtt-"))
                {
                    MQTT_CONNECTION_STRING = args[i].Replace("--mqtt-", "");
                    Console.WriteLine("Set MQTT connection string from parameter: " + MQTT_CONNECTION_STRING);
                }
            }
        }
    }
}
