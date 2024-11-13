using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class ArduinoConnectie
    {
        private SerialPort serialPort;

        public event Action UpCommandReceived;
        public event Action DownCommandReceived;
        public event Action minusCommandReceived;
        public event Action plusCommandReceived;
        public event Action SelectCommandReceived;

        public ArduinoConnectie(string portName, int baudRate)
        {
            serialPort = new SerialPort(portName, baudRate);
            serialPort.DataReceived += DataReceived2;
        }

        public void OpenConnection()
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine("Error connecting arduino. " + ex.Message);
            }
        }

        public void CloseConnection()
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }

        private void DataReceived2(object sender, SerialDataReceivedEventArgs e)
        {
            string command = serialPort.ReadLine().Trim();

            // Roep het juiste evenement aan op basis van het commando
            MainThread.BeginInvokeOnMainThread(() =>
            {
                // Roep het juiste evenement aan op basis van het commando
                if (command == "UP" && UpCommandReceived != null)
                {
                    UpCommandReceived.Invoke();
                }
                else if (command == "DOWN" && DownCommandReceived != null)
                {
                    DownCommandReceived.Invoke();
                }
                else if (command == "-" && minusCommandReceived != null)
                {
                    minusCommandReceived.Invoke();
                }
                else if (command == "+" && plusCommandReceived != null)
                {
                    plusCommandReceived.Invoke();
                }

                else if(command == "SELECT" && SelectCommandReceived != null)
                {
                    SelectCommandReceived.Invoke();
                }
            });
        }
    }
}
