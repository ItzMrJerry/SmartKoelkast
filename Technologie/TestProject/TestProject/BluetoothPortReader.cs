        using System;
        using System.IO.Ports;

        public class BluetoothPortReader
        {
            private SerialPort _serialPort;

            public BluetoothPortReader(string portName)
            {
                _serialPort = new SerialPort(portName);
                _serialPort.BaudRate = 9600;  // Baud rate of the HC-05
                _serialPort.Parity = Parity.None;
                _serialPort.StopBits = StopBits.One;
                _serialPort.DataBits = 8;
                _serialPort.Handshake = Handshake.None;

                _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            }

            // Event handler for reading incoming data
            private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
            {
                SerialPort sp = (SerialPort)sender;
                string incomingData = sp.ReadLine();
                Console.WriteLine("Received Data: " + incomingData);
            }

            // Open the serial port
            public void Open()
            {
                _serialPort.Open();
                Console.WriteLine("Connection established. Waiting for data...");
            }

            // Close the serial port
            public void Close()
            {
                _serialPort.Close();
                Console.WriteLine("Connection closed.");
            }
        }
