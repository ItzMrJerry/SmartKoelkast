using System;
using System.Linq;
using System.IO.Ports;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using InTheHand.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestProject
{
    internal class BluetoothTest
    {
        public void GetDevices()
        {
            BluetoothClient btClient = new BluetoothClient();
            BluetoothDeviceInfo[] devices = btClient.DiscoverDevices();

            foreach (BluetoothDeviceInfo device in devices)
            {
                Console.WriteLine("Device Found: " + device.DeviceName);
                Console.WriteLine("Address: " + device.DeviceAddress);
            }
        }


        public void ConnectToDevice(string ComPort)
        {
            string btAddressString = "00220100052C"; // Replace with your Bluetooth device address
            BluetoothAddress btAddress = BluetoothAddress.Parse(btAddressString);

            // Create a Bluetooth client
            BluetoothClient btClient = new BluetoothClient();

            // Discover devices and match the one we want based on the Bluetooth address
            BluetoothDeviceInfo[] devices = btClient.DiscoverDevices();
            BluetoothDeviceInfo device = devices.FirstOrDefault(d => d.DeviceAddress == btAddress);
            

            if (device == null)
            {
                Console.WriteLine("Device not found.");
                return;
            }

            // Attempt to connect to the Serial Port Profile (SPP) service
            try
            {
                // Connect to the device's serial port service
                btClient.Connect(btAddress, BluetoothService.SerialPort);
                //btClient.GetStream().Read();

         
                Console.WriteLine("Connected to Bluetooth device! Port: " + BluetoothService.SerialPort);

                // Initialize the BluetoothReader to listen for incoming data
                BluetoothPortReader reader = new BluetoothPortReader(ComPort);
                reader.Open();

                // Keep the program running to listen for data
                Console.WriteLine("Press Enter to exit...");
                Console.ReadLine();

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to Bluetooth device: " + ex.Message);
            }
        }
        public void CheckPorts()
        {
            string btAddressString = "00220100052C"; // Replace with your Bluetooth device address
            BluetoothAddress btAddress = BluetoothAddress.Parse(btAddressString);

            // Create a Bluetooth client
            BluetoothClient btClient = new BluetoothClient();

            // Discover devices and match the one we want based on the Bluetooth address
            BluetoothDeviceInfo[] devices = btClient.DiscoverDevices();
            BluetoothDeviceInfo device = devices.FirstOrDefault(d => d.DeviceAddress == btAddress);

            if (device == null)
            {
                Console.WriteLine("Device not found.");
                return;
            }

            // Connect to the device's serial port service
            try
            {
                btClient.Connect(btAddress, BluetoothService.SerialPort);
                Console.WriteLine("Connected to Bluetooth device!");

                // List all available COM ports on the machine
                string[] comPorts = SerialPort.GetPortNames();
                Console.WriteLine("Available COM ports:");
                foreach (var port in comPorts)
                {
                    Console.WriteLine(port);
                }

                // Test each COM port
                foreach (string portName in comPorts)
                {
                    Console.WriteLine($"Attempting to connect to {portName}...");
                    SerialPort serialPort = new SerialPort(portName, 9600); // Set appropriate baud rate
                    try
                    {
                        serialPort.Open();
                        Console.WriteLine($"Successfully opened {portName}");

                        // Check if we can read data from the port
                        string data = string.Empty;
                        if (serialPort.BytesToRead > 0)
                        {
                            data = serialPort.ReadLine().Trim();
                            if (!string.IsNullOrEmpty(data))
                            {
                                Console.WriteLine($"Received data from {portName}: {data}");
                            }
                        }

                        // Close port after checking
                        serialPort.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error with {portName}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to Bluetooth device: " + ex.Message);
            }
        }


    }
}
