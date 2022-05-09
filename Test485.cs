using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;  // SerialPort

namespace RS485Test
{
    public class Test485
    {
        SerialPort _serialPort;
        public void Connect(string portName)
        {
            int baudRate = 115200;
            int msReadTimeout = 1000;    //100;
            _serialPort = new SerialPort(portName, baudRate);
            _serialPort.ReadTimeout = msReadTimeout;
            _serialPort.DataReceived += _serialPort_DataReceived; ;
            _serialPort.ReadBufferSize = 256;
            _serialPort.WriteBufferSize = 256;
            try
            {
                _serialPort.Open();
            }
            catch (Exception ex)
            {
                Log.WriteLog(Log.LogType.Log_485, ex.Message);
            }
        }

        object _object = new object();
        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            lock(_object)
            {
                byte[] buffer = new byte[9];
                string rcv_str = "";
                try
                {
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        buffer[i] = (byte)_serialPort.ReadByte();
                        rcv_str += buffer[i] + ", ";
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteLog(Log.LogType.Log_485, ex.Message);
                }
                Log.WriteLog(Log.LogType.Log_485, rcv_str);
            }
        }
    }
}
