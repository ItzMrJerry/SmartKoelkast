using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Security;

namespace SmartKitchen
{
    public class ArduinoConnectie
    {
        private SerialPort serialPort;

       public ArduinoConnectie(string portName, int baudRate)
       {
            serialPort = new SerialPort(portName, baudRate);
            serialPort.DataReceived += OnDataReceived;
       }

        public void OpenConnection()
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                    Console.WriteLine("Connected to arduino on " + serialPort.PortName);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error connecting arduino. " + ex.Message);
            }
        }

        public void CloseConnection()
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
                Console.WriteLine("Disconnected from arduino");
            }
        }

        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = serialPort.ReadLine();
                Console.WriteLine("Received: " + data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading data " + ex);
            }
        }

        public void SendData(string data)
        {
            if (serialPort.IsOpen)
            {
                serialPort.WriteLine(data);
            }
            else
            {
                Console.WriteLine("Port is not open");
            }
        }

    }
}
