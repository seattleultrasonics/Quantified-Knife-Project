using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.Globalization;
using System.Diagnostics;

namespace QKPRobot
{

    public class RuishanScale
    {
        private SerialPort serialPort;
        public double Mass = 0;

        
        public RuishanScale(string portName)
        {
            serialPort = new SerialPort(portName, 19200, Parity.None, 8, StopBits.One)
            {
                //ReadTimeout = 500,
                //WriteTimeout = 500
            };
        }

        public bool Open()
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                }
                serialPort.DataReceived += SerialPort_DataReceived;
                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error opening serial port: " + ex.Message);
                return false;
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // Read the available data
            string data = serialPort.ReadLine();
            data = data.Trim().TrimEnd('g');

            // Update the label in a thread-safe way
            double? mass = ParseDoubleResponse(data);
            if (mass.HasValue )
            { 
                Mass = mass.Value;
                Console.WriteLine(Mass);
            }
            else
            {
                Console.WriteLine("parse fail");
            }
        }

        public bool Close()
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                    serialPort.DataReceived -= SerialPort_DataReceived;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error closing serial port: " + ex.Message);
                return false;
            }
        }

        private double? ParseDoubleResponse(string response)
        {
            if (double.TryParse(response, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
            {
                return result;
            }
            Console.WriteLine("Failed to parse response as double: " + response);
            return null;
        }

        public string SendCommand(string command)
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    Console.WriteLine("Serial port is not open.");
                    return null;
                }

                serialPort.WriteLine(command);
                Thread.Sleep(10); // Wait for the device to respond
                return serialPort.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send command: " + ex.Message);
                return null;
            }
        }


        public void Dispose()
        {
            if (serialPort != null)
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                }
                serialPort.Dispose();
            }
        }

        public override string ToString()
        {
            return Mass.ToString("F2");
        }
    }




}
