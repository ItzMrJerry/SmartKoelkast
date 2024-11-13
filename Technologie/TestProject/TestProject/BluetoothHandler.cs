using System;
using System.IO;
using System.Text;
using InTheHand.Net.Sockets; // Ensure you have the InTheHand.Net.Bluetooth library
using InTheHand.Net.Bluetooth;
using InTheHand.Net;
using System.Net.Sockets;

public class BluetoothHandler
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

    public void ConnectToDevice(string btAddressString)
    {
        BluetoothAddress btAddress = BluetoothAddress.Parse(btAddressString);

        BluetoothClient btClient = new BluetoothClient();

        try
        {
            // Attempt to connect to the device's serial port service
            btClient.Connect(btAddress, BluetoothService.SerialPort);
            Console.WriteLine("Connected to Bluetooth device!");

            // Read from the Bluetooth stream
            using (NetworkStream stream = btClient.GetStream())
            {
                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Received: " + message);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error connecting to Bluetooth device: " + ex.Message);
        }
    }
}
