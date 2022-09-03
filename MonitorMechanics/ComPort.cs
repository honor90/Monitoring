using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;

namespace MonitorMechanics
{
    
    class ComPort
    {
        private SerialPort com_port;
        public string[] ports;

        public byte[] buffer = new byte[100];
        public int buferSize = 0;

        public ComPort()
        {
            com_port = new SerialPort();

            ports = SerialPort.GetPortNames();
        }

        public Boolean Open(String Port_Name, int Baud_Rate)
        {
            com_port.PortName = Port_Name;
            com_port.BaudRate = Baud_Rate;
            com_port.DataBits = 8;
            com_port.StopBits = StopBits.One;
            com_port.Parity = Parity.None;
            com_port.WriteTimeout = 100;
            com_port.ReadTimeout = 100;


            try//блочим элементы управления при открытии порта
            {

                //  тут подписываемся на событие прихода данных в порт
                //  для вашей задачи это должно подойт идеально
                com_port.DataReceived += SerialPort_DataReceived;

                com_port.Open();

                if (com_port.IsOpen)
                {
                    com_port.DiscardInBuffer();
                    com_port.DiscardOutBuffer();
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Close()
        {
            com_port.DiscardInBuffer();
            com_port.DiscardOutBuffer();
            com_port.Close();
        }

        private int indificator = 77;
        private int _stepIndex;
        private bool _startRead;
        public bool Receive_complete = false;

        //  эта функция вызвется каждый раз, когда в порт что то будет передано 
        void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port = (SerialPort)sender;
            try
            {
                //  узнаем сколько байт пришло
                buferSize = port.BytesToRead;

                _stepIndex = 0;
                _startRead = true;

                for (int i = 0; i < buferSize; ++i)
                {
                    //  читаем по одному байту
                    byte bt = (byte)port.ReadByte();
                    //if (bt == indificator)
                    //{
                        
                        
                    //}
                    //  дописываем в буфер все остальное
                    if (_startRead)
                    {
                        buffer[_stepIndex] = bt;
                        ++_stepIndex;
                       
                    }

                    
                    //  когда буфер наполнлся данными
                    if (_stepIndex == buferSize && _startRead)
                    {
                        _startRead = false;
                        Receive_complete = true;
                    }
                    
                }
            }

            catch { }
        }

        public void WriteBuff(byte[] buffer, int offset, int count)
        {
            com_port.Write(buffer, offset, count);
        }

    }
}
