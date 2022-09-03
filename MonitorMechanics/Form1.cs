using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MonitorMechanics
{
    public partial class Form1 : Form
    {
        private ComPort port;
        private ModbusRTU Modbus;

        private Axis AxisTemp_drv1;
        private Axis AxisSpeed_drv1;
        private Axis AxisVibration_1_drv1;
        private Axis AxisVibration_2_drv1;

        private Axis AxisTemp_drv2;
        private Axis AxisSpeed_drv2;
        private Axis AxisVibration_1_drv2;
        private Axis AxisVibration_2_drv2;

        private Line Temp_drv1;
        private Line Speed_Motor_drv1;
        private Line Speed_Generator_1_drv1; 
        private Line Speed_Generator_2_drv1;
        private Line Vibration_1_Z_drv1;
        private Line Vibration_2_Z_drv1;

        private Line Temp_drv2;
        private Line Speed_Motor_drv2;
        private Line Speed_Generator_1_drv2;
        private Line Speed_Generator_2_drv2;
        private Line Vibration_1_Z_drv2;
        private Line Vibration_2_Z_drv2;


        private ushort[] data_limit = new ushort[24];



        public Form1()
        {
            InitializeComponent();

            label_stat_connect.Text = "Соединение не установлено";

            label_temp_1.Text = "0";
            label_speed_driver_1.Text = "0";
            label_speed_gen_11.Text = "0";
            label_speed_gen_12.Text = "0";
            label_vib_11.Text = "0";
            label_vib_12.Text = "0";

            label_temp_2.Text = "0";
            label_speed_driver_2.Text = "0";
            label_speed_gen_21.Text = "0";
            label_speed_gen_22.Text = "0";
            label_vib_21.Text = "0";
            label_vib_22.Text = "0";

            label_stat_exchange.Text = "Не установлено";
            label_count_er_crc.Text = "0";

            Enable_TabControl(false);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            port = new ComPort();
            Modbus = new ModbusRTU();

            comboBox1.Items.Clear();
            foreach (string port in port.ports)
            {
                comboBox1.Items.Add(port);
            }

            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }

            //Enable_TabControl(false);

            AxisTemp_drv1 = new Axis(zedGraph_Temp_drv1);
            AxisSpeed_drv1 = new Axis(zedGraph_Speed_drv1);
            AxisVibration_1_drv1 = new Axis(zedGraph_Vibration_1_drv1);
            AxisVibration_2_drv1 = new Axis(zedGraph_Vibration_2_drv1);
            AxisTemp_drv2 = new Axis(zedGraph_Temp_drv2);
            AxisSpeed_drv2 = new Axis(zedGraph_Speed_drv2);
            AxisVibration_1_drv2 = new Axis(zedGraph_Vibration_1_drv2);
            AxisVibration_2_drv2 = new Axis(zedGraph_Vibration_2_drv2);

            Temp_drv1 = new Line(zedGraph_Temp_drv1);
            Speed_Motor_drv1 = new Line(zedGraph_Speed_drv1);
            Speed_Generator_1_drv1 = new Line(zedGraph_Speed_drv1);
            Speed_Generator_2_drv1 = new Line(zedGraph_Speed_drv1);
            Vibration_1_Z_drv1 = new Line(zedGraph_Vibration_1_drv1);
            Vibration_2_Z_drv1 = new Line(zedGraph_Vibration_2_drv1);


            Temp_drv2 = new Line(zedGraph_Temp_drv2);
            Speed_Motor_drv2 = new Line(zedGraph_Speed_drv2);
            Speed_Generator_1_drv2 = new Line(zedGraph_Speed_drv2);
            Speed_Generator_2_drv2 = new Line(zedGraph_Speed_drv2);
            Vibration_1_Z_drv2 = new Line(zedGraph_Vibration_1_drv2);
            Vibration_2_Z_drv2 = new Line(zedGraph_Vibration_2_drv2);


            AxisTemp_drv1.TITLE = "";
            AxisTemp_drv1.NAME_X = "сек";
            AxisTemp_drv1.NAME_Y = "°C";
            AxisTemp_drv1.X_MIN_LIMIT = 0;
            AxisTemp_drv1.X_MAX_LIMIT = 60;
            AxisTemp_drv1.Y_MIN_LIMIT = 0;
            AxisTemp_drv1.Y_MAX_LIMIT = 200;
            AxisTemp_drv1.SIZE_TEXT = 10;

            AxisTemp_drv1.GreateAxis();

            AxisSpeed_drv1.TITLE = "";
            AxisSpeed_drv1.NAME_X = "сек";
            AxisSpeed_drv1.NAME_Y = "об / мин";
            AxisSpeed_drv1.X_MIN_LIMIT = 0;
            AxisSpeed_drv1.X_MAX_LIMIT = 60;
            AxisSpeed_drv1.Y_MIN_LIMIT = 0;
            AxisSpeed_drv1.Y_MAX_LIMIT = 15000;
            AxisSpeed_drv1.SIZE_TEXT = 10;

            AxisSpeed_drv1.GreateAxis();

            AxisVibration_1_drv1.TITLE = "";
            AxisVibration_1_drv1.NAME_X = "сек";
            AxisVibration_1_drv1.NAME_Y = "мм / сек";
            AxisVibration_1_drv1.X_MIN_LIMIT = 0;
            AxisVibration_1_drv1.X_MAX_LIMIT = 60;
            AxisVibration_1_drv1.Y_MIN_LIMIT = 0;
            AxisVibration_1_drv1.Y_MAX_LIMIT = 40;
            AxisVibration_1_drv1.SIZE_TEXT = 10;

            AxisVibration_1_drv1.GreateAxis();

            AxisVibration_2_drv1.TITLE = "";
            AxisVibration_2_drv1.NAME_X = "сек";
            AxisVibration_2_drv1.NAME_Y = "мм / сек";
            AxisVibration_2_drv1.X_MIN_LIMIT = 0;
            AxisVibration_2_drv1.X_MAX_LIMIT = 60;
            AxisVibration_2_drv1.Y_MIN_LIMIT = 0;
            AxisVibration_2_drv1.Y_MAX_LIMIT = 40;
            AxisVibration_2_drv1.SIZE_TEXT = 10;

            AxisVibration_2_drv1.GreateAxis();


            AxisTemp_drv2.TITLE = "";
            AxisTemp_drv2.NAME_X = "сек";
            AxisTemp_drv2.NAME_Y = "°C";
            AxisTemp_drv2.X_MIN_LIMIT = 0;
            AxisTemp_drv2.X_MAX_LIMIT = 60;
            AxisTemp_drv2.Y_MIN_LIMIT = 0;
            AxisTemp_drv2.Y_MAX_LIMIT = 200;
            AxisTemp_drv2.SIZE_TEXT = 10;

            AxisTemp_drv2.GreateAxis();

            AxisSpeed_drv2.TITLE = "";
            AxisSpeed_drv2.NAME_X = "сек";
            AxisSpeed_drv2.NAME_Y = "об / мин";
            AxisSpeed_drv2.X_MIN_LIMIT = 0;
            AxisSpeed_drv2.X_MAX_LIMIT = 60;
            AxisSpeed_drv2.Y_MIN_LIMIT = 0;
            AxisSpeed_drv2.Y_MAX_LIMIT = 15000;
            AxisSpeed_drv2.SIZE_TEXT = 10;

            AxisSpeed_drv2.GreateAxis();

            AxisVibration_1_drv2.TITLE = "";
            AxisVibration_1_drv2.NAME_X = "сек";
            AxisVibration_1_drv2.NAME_Y = "мм / сек";
            AxisVibration_1_drv2.X_MIN_LIMIT = 0;
            AxisVibration_1_drv2.X_MAX_LIMIT = 60;
            AxisVibration_1_drv2.Y_MIN_LIMIT = 0;
            AxisVibration_1_drv2.Y_MAX_LIMIT = 40;
            AxisVibration_1_drv2.SIZE_TEXT = 10;

            AxisVibration_1_drv2.GreateAxis();

            AxisVibration_2_drv2.TITLE = "";
            AxisVibration_2_drv2.NAME_X = "сек";
            AxisVibration_2_drv2.NAME_Y = "мм / сек";
            AxisVibration_2_drv2.X_MIN_LIMIT = 0;
            AxisVibration_2_drv2.X_MAX_LIMIT = 60;
            AxisVibration_2_drv2.Y_MIN_LIMIT = 0;
            AxisVibration_2_drv2.Y_MAX_LIMIT = 40;
            AxisVibration_2_drv2.SIZE_TEXT = 10;

            AxisVibration_2_drv2.GreateAxis();


            Temp_drv1.ColorLine = Color.Green;
            Temp_drv1.NameLine = "Температура двигателя";
            Temp_drv1.CreateLine();

            Speed_Motor_drv1.ColorLine = Color.Green;
            Speed_Motor_drv1.NameLine = "Частота вращения двигателя";
            Speed_Motor_drv1.CreateLine();

            Speed_Generator_1_drv1.ColorLine = Color.Red;
            Speed_Generator_1_drv1.NameLine = "Частота вращения генератора №1";
            Speed_Generator_1_drv1.CreateLine();

            Speed_Generator_2_drv1.ColorLine = Color.Blue;
            Speed_Generator_2_drv1.NameLine = "Частота вращения генератора №2";
            Speed_Generator_2_drv1.CreateLine();


            Vibration_1_Z_drv1.ColorLine = Color.Blue;
            Vibration_1_Z_drv1.NameLine = "Z";
            Vibration_1_Z_drv1.CreateLine();


            Vibration_2_Z_drv1.ColorLine = Color.Blue;
            Vibration_2_Z_drv1.NameLine = "Z";
            Vibration_2_Z_drv1.CreateLine();



            Temp_drv2.ColorLine = Color.Green;
            Temp_drv2.NameLine = "Температура двигателя";
            Temp_drv2.CreateLine();

            Speed_Motor_drv2.ColorLine = Color.Green;
            Speed_Motor_drv2.NameLine = "Частота вращения двигателя";
            Speed_Motor_drv2.CreateLine();

            Speed_Generator_1_drv2.ColorLine = Color.Red;
            Speed_Generator_1_drv2.NameLine = "Частота вращения генератора №1";
            Speed_Generator_1_drv2.CreateLine();

            Speed_Generator_2_drv2.ColorLine = Color.Blue;
            Speed_Generator_2_drv2.NameLine = "Частота вращения генератора №2";
            Speed_Generator_2_drv2.CreateLine();


            Vibration_1_Z_drv2.ColorLine = Color.Blue;
            Vibration_1_Z_drv2.NameLine = "Z";
            Vibration_1_Z_drv2.CreateLine();


            Vibration_1_Z_drv2.ColorLine = Color.Blue;
            Vibration_2_Z_drv2.NameLine = "Z";
            Vibration_2_Z_drv2.CreateLine();

        }

  
        private byte mun_next_pack = 1;
        private int count_ms = 0;
        private int count_error_crc16 = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (port.Receive_complete)
            {
                port.Receive_complete = false;
                Modbus.Handler_Master(port.buffer);
                if (Modbus.Pack_Is_Received)
                {
                    count_ms = 0;
                    Modbus.Enable_Send = true;
                }

                switch (Modbus.ERROR_Handler_Master)
                {
                    case 0:
                        label_stat_exchange.Text = "Установлено";
                        break;
                    case ModbusRTU.ERROR_CRC16:
                        Modbus.ERROR_Handler_Master = 0;
                        count_error_crc16++;
                        label_count_er_crc.Text = count_error_crc16.ToString();
                        break;
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            count_ms++;
            if(count_ms == 5)
            {
                count_ms = 0;
                Modbus.Enable_Send = true;
            }

            if (Modbus.Enable_Send)
            {
                Modbus.Enable_Send = false;
                switch(mun_next_pack)
                {
                    case 1:
                        Modbus.READ_INPUT_Pack(77, ModbusRTU.DRV1_ERROR, 4);
                        port.WriteBuff(Modbus.out_buff, 0, Modbus.out_lenght);
                        mun_next_pack = 2;
                        break;
                    case 2:
                        Modbus.READ_INPUT_Pack(77, ModbusRTU.DRV1_ROT_FREQ_MOTOR_Lo, 4);
                        port.WriteBuff(Modbus.out_buff, 0, Modbus.out_lenght);
                        mun_next_pack = 3;
                        break;
                    case 3:
                        Modbus.READ_INPUT_Pack(77, ModbusRTU.DRV1_ROT_FREQ_GEN_2_Lo, 4);
                        port.WriteBuff(Modbus.out_buff, 0, Modbus.out_lenght);
                        mun_next_pack = 4;
                        break;
                    case 4:
                        Modbus.READ_INPUT_Pack(77, ModbusRTU.DRV1_RATIO_2_Lo, 6);
                        port.WriteBuff(Modbus.out_buff, 0, Modbus.out_lenght);
                        mun_next_pack = 5;
                        break;
                    case 5:
                        Modbus.READ_HOLDING_Pack(77, ModbusRTU.DRV1_COMMAND_Lo, 6);
                        port.WriteBuff(Modbus.out_buff, 0, Modbus.out_lenght);
                        mun_next_pack = 6;
                        break;
                    case 6:
                        Modbus.READ_HOLDING_Pack(77, ModbusRTU.DRV1_LIMIT_RATIO_2_Lo, 6);
                        port.WriteBuff(Modbus.out_buff, 0, Modbus.out_lenght);
                        mun_next_pack = 7;
                        break;
                    case 7:
                        Modbus.READ_INPUT_Pack(77, ModbusRTU.DRV2_ERROR, 4);
                        port.WriteBuff(Modbus.out_buff, 0, Modbus.out_lenght);
                        mun_next_pack = 8;
                        break;
                    case 8:
                        Modbus.READ_INPUT_Pack(77, ModbusRTU.DRV2_ROT_FREQ_MOTOR_Lo, 4);
                        port.WriteBuff(Modbus.out_buff, 0, Modbus.out_lenght);
                        mun_next_pack = 9;
                        break;
                    case 9:
                        Modbus.READ_INPUT_Pack(77, ModbusRTU.DRV2_ROT_FREQ_GEN_2_Lo, 4);
                        port.WriteBuff(Modbus.out_buff, 0, Modbus.out_lenght);
                        mun_next_pack = 10;
                        break;
                    case 10:
                        Modbus.READ_INPUT_Pack(77, ModbusRTU.DRV2_RATIO_2_Lo, 6);
                        port.WriteBuff(Modbus.out_buff, 0, Modbus.out_lenght);
                        mun_next_pack = 11;
                        break;
                    case 11:
                        Modbus.READ_HOLDING_Pack(77, ModbusRTU.DRV2_COMMAND_Lo, 6);
                        port.WriteBuff(Modbus.out_buff, 0, Modbus.out_lenght);
                        mun_next_pack = 12;
                        break;
                    case 12:
                        Modbus.READ_HOLDING_Pack(77, ModbusRTU.DRV2_LIMIT_RATIO_2_Lo, 6);
                        port.WriteBuff(Modbus.out_buff, 0, Modbus.out_lenght);
                        mun_next_pack = 1;
                        break;
                    case 13:
                        Modbus.WRITE_MULTIPLE_REG_Pack(77, ModbusRTU.DRV1_COMMAND_Lo, 12, data_limit);
                        port.WriteBuff(Modbus.out_buff, 0, Modbus.out_lenght);
                        mun_next_pack = 1;
                        break;
                    case 14:
                        Modbus.WRITE_MULTIPLE_REG_Pack(77, ModbusRTU.DRV2_COMMAND_Lo, 12, data_limit);
                        port.WriteBuff(Modbus.out_buff, 0, Modbus.out_lenght);
                        mun_next_pack = 1;
                        break;
                }
            }
        }

        private double POINT_X = 0;
        private float y1 = 0;

        private Boolean Enable_First_Num = true;

        private void timer3_Tick(object sender, EventArgs e)
        {
            Heandler_TabControl();

            if(Enable_First_Num && Modbus.Holding_Registers[ModbusRTU.DRV2_LIMIT_VIB_SPEED_2_Z_Lo]!=0)
            {
                Enable_First_Num = false;
                threshold_temp_1.Value = (Decimal)Modbus.ValueFloat(Modbus.Holding_Registers, ModbusRTU.DRV1_LIMIT_TEMP_Lo);
                ratio_11.Value = (Decimal)Modbus.ValueFloat(Modbus.Holding_Registers, ModbusRTU.DRV1_LIMIT_RATIO_1_Lo);
                ratio_12.Value = (Decimal)Modbus.ValueFloat(Modbus.Holding_Registers, ModbusRTU.DRV1_LIMIT_RATIO_2_Lo);
                threshold_vib_11.Value = (Decimal)Modbus.ValueFloat(Modbus.Holding_Registers, ModbusRTU.DRV1_LIMIT_VIB_SPEED_1_Z_Lo);
                threshold_vib_12.Value = (Decimal)Modbus.ValueFloat(Modbus.Holding_Registers, ModbusRTU.DRV1_LIMIT_VIB_SPEED_2_Z_Lo);

                threshold_temp_2.Value = (Decimal)Modbus.ValueFloat(Modbus.Holding_Registers, ModbusRTU.DRV2_LIMIT_TEMP_Lo);
                ratio_21.Value = (Decimal)Modbus.ValueFloat(Modbus.Holding_Registers, ModbusRTU.DRV2_LIMIT_RATIO_1_Lo);
                ratio_22.Value = (Decimal)Modbus.ValueFloat(Modbus.Holding_Registers, ModbusRTU.DRV2_LIMIT_RATIO_2_Lo);
                threshold_vib_21.Value = (Decimal)Modbus.ValueFloat(Modbus.Holding_Registers, ModbusRTU.DRV2_LIMIT_VIB_SPEED_1_Z_Lo);
                threshold_vib_22.Value = (Decimal)Modbus.ValueFloat(Modbus.Holding_Registers, ModbusRTU.DRV2_LIMIT_VIB_SPEED_2_Z_Lo);
            }

            if ((Modbus.Input_Registers[ModbusRTU.DRV1_CODE_ALARM] & ModbusRTU.ALARM_TEMP) == ModbusRTU.ALARM_TEMP) label_alarm_temp_1.BackColor = System.Drawing.Color.Red;
            else label_alarm_temp_1.BackColor = SystemColors.ControlDark;

            if ((Modbus.Input_Registers[ModbusRTU.DRV1_CODE_ALARM] & ModbusRTU.ALARM_SPEED) == ModbusRTU.ALARM_SPEED) label_alarm_speed_1.BackColor = System.Drawing.Color.Red;
            else label_alarm_speed_1.BackColor = SystemColors.ControlDark;

            if ((Modbus.Input_Registers[ModbusRTU.DRV1_CODE_ALARM] & ModbusRTU.ALARM_VIB_1) == ModbusRTU.ALARM_VIB_1) label_alarm_vib_11.BackColor = System.Drawing.Color.Red;
            else label_alarm_vib_11.BackColor = SystemColors.ControlDark;

            if ((Modbus.Input_Registers[ModbusRTU.DRV1_CODE_ALARM] & ModbusRTU.ALARM_VIB_2) == ModbusRTU.ALARM_VIB_2) label_alarm_vib_12.BackColor = System.Drawing.Color.Red;
            else label_alarm_vib_12.BackColor = SystemColors.ControlDark;

            if ((Modbus.Input_Registers[ModbusRTU.DRV2_CODE_ALARM] & ModbusRTU.ALARM_TEMP) == ModbusRTU.ALARM_TEMP) label_alarm_temp_2.BackColor = System.Drawing.Color.Red;
            else label_alarm_temp_2.BackColor = SystemColors.ControlDark;

            if ((Modbus.Input_Registers[ModbusRTU.DRV2_CODE_ALARM] & ModbusRTU.ALARM_SPEED) == ModbusRTU.ALARM_SPEED) label_alarm_speed_2.BackColor = System.Drawing.Color.Red;
            else label_alarm_speed_2.BackColor = SystemColors.ControlDark;

            if ((Modbus.Input_Registers[ModbusRTU.DRV2_CODE_ALARM] & ModbusRTU.ALARM_VIB_1) == ModbusRTU.ALARM_VIB_1) label_alarm_vib_21.BackColor = System.Drawing.Color.Red;
            else label_alarm_vib_21.BackColor = SystemColors.ControlDark;

            if ((Modbus.Input_Registers[ModbusRTU.DRV2_CODE_ALARM] & ModbusRTU.ALARM_VIB_2) == ModbusRTU.ALARM_VIB_2) label_alarm_vib_22.BackColor = System.Drawing.Color.Red;
            else label_alarm_vib_22.BackColor = SystemColors.ControlDark;


            label_temp_1.Text = Math.Round(Modbus.ValueFloat(Modbus.Input_Registers, ModbusRTU.DRV1_TEMP_Lo), 2).ToString();
            label_speed_driver_1.Text = Math.Round(Modbus.ValueFloat(Modbus.Input_Registers, ModbusRTU.DRV1_ROT_FREQ_MOTOR_Lo), 2).ToString();
            label_speed_gen_11.Text = Math.Round(Modbus.ValueFloat(Modbus.Input_Registers, ModbusRTU.DRV1_ROT_FREQ_GEN_1_Lo), 2).ToString();
            label_speed_gen_12.Text = Math.Round(Modbus.ValueFloat(Modbus.Input_Registers, ModbusRTU.DRV1_ROT_FREQ_GEN_2_Lo), 2).ToString();
            label_vib_11.Text = Math.Round(Modbus.ValueFloat(Modbus.Input_Registers, ModbusRTU.DRV1_VIB_SPEED_1_Z_Lo), 2).ToString();
            label_vib_12.Text = Math.Round(Modbus.ValueFloat(Modbus.Input_Registers, ModbusRTU.DRV1_VIB_SPEED_2_Z_Lo), 2).ToString();


            label_temp_2.Text = Math.Round(Modbus.ValueFloat(Modbus.Input_Registers, ModbusRTU.DRV2_TEMP_Lo), 2).ToString();
            label_speed_driver_2.Text = Math.Round(Modbus.ValueFloat(Modbus.Input_Registers, ModbusRTU.DRV2_ROT_FREQ_MOTOR_Lo), 2).ToString();
            label_speed_gen_21.Text = Math.Round(Modbus.ValueFloat(Modbus.Input_Registers, ModbusRTU.DRV2_ROT_FREQ_GEN_1_Lo), 2).ToString();
            label_speed_gen_22.Text = Math.Round(Modbus.ValueFloat(Modbus.Input_Registers, ModbusRTU.DRV2_ROT_FREQ_GEN_2_Lo), 2).ToString();
            label_vib_21.Text = Math.Round(Modbus.ValueFloat(Modbus.Input_Registers, ModbusRTU.DRV2_VIB_SPEED_1_Z_Lo), 2).ToString();
            label_vib_22.Text = Math.Round(Modbus.ValueFloat(Modbus.Input_Registers, ModbusRTU.DRV2_VIB_SPEED_2_Z_Lo), 2).ToString();


            
            if (POINT_X < 60)
            {
                POINT_X += 0.1;
                POINT_X = Math.Round(POINT_X, 1);
            }
            if (POINT_X == 60)
            {
                POINT_X = 60;
            }


            
            label_limit_temp_drv1.Text = Math.Round(Modbus.ValueFloat(Modbus.Holding_Registers, ModbusRTU.DRV1_LIMIT_TEMP_Lo),2).ToString();
            label_ratio_1_drv1.Text = Math.Round(Modbus.ValueFloat(Modbus.Holding_Registers, ModbusRTU.DRV1_LIMIT_RATIO_1_Lo), 2).ToString();
            label_ratio_2_drv1.Text = Math.Round(Modbus.ValueFloat(Modbus.Holding_Registers, ModbusRTU.DRV1_LIMIT_RATIO_2_Lo), 2).ToString();
            label_limit_vib_1_drv1.Text = Math.Round(Modbus.ValueFloat(Modbus.Holding_Registers, ModbusRTU.DRV1_LIMIT_VIB_SPEED_1_Z_Lo), 2).ToString();
            label_limit_vib_2_drv1.Text = Math.Round(Modbus.ValueFloat(Modbus.Holding_Registers, ModbusRTU.DRV1_LIMIT_VIB_SPEED_2_Z_Lo), 2).ToString();


            label_limit_temp_drv2.Text = Math.Round(Modbus.ValueFloat(Modbus.Holding_Registers, ModbusRTU.DRV2_LIMIT_TEMP_Lo),2).ToString();
            label_ratio_1_drv2.Text = Math.Round(Modbus.ValueFloat(Modbus.Holding_Registers, ModbusRTU.DRV2_LIMIT_RATIO_1_Lo),2).ToString();
            label_ratio_2_drv2.Text = Math.Round(Modbus.ValueFloat(Modbus.Holding_Registers, ModbusRTU.DRV2_LIMIT_RATIO_2_Lo),2).ToString();
            label_limit_vib_1_drv2.Text = Math.Round(Modbus.ValueFloat(Modbus.Holding_Registers, ModbusRTU.DRV2_LIMIT_VIB_SPEED_1_Z_Lo),2).ToString();
            label_limit_vib_2_drv2.Text = Math.Round(Modbus.ValueFloat(Modbus.Holding_Registers, ModbusRTU.DRV2_LIMIT_VIB_SPEED_2_Z_Lo),2).ToString();

            Temp_drv1.ClearPane();
            Speed_Motor_drv1.ClearPane();
            Speed_Generator_1_drv1.ClearPane();
            Speed_Generator_2_drv1.ClearPane();
            Vibration_1_Z_drv1.ClearPane();
            Vibration_2_Z_drv1.ClearPane();

            Temp_drv2.ClearPane();
            Speed_Motor_drv2.ClearPane();
            Speed_Generator_1_drv2.ClearPane();
            Speed_Generator_2_drv2.ClearPane();
            Vibration_1_Z_drv2.ClearPane();
            Vibration_2_Z_drv2.ClearPane();

            y1 = Modbus.ValueFloat(Modbus.Holding_Registers, ModbusRTU.DRV1_LIMIT_TEMP_Lo);
            Temp_drv1.DrawLine((ushort)y1);
            y1 = Modbus.ValueFloat(Modbus.Holding_Registers, ModbusRTU.DRV1_LIMIT_VIB_SPEED_1_Z_Lo);
            Vibration_1_Z_drv1.DrawLine((ushort)y1);
            y1 = Modbus.ValueFloat(Modbus.Holding_Registers, ModbusRTU.DRV1_LIMIT_VIB_SPEED_2_Z_Lo);
            Vibration_2_Z_drv1.DrawLine((ushort)y1);

            y1 = Modbus.ValueFloat(Modbus.Holding_Registers, ModbusRTU.DRV2_LIMIT_TEMP_Lo);
            Temp_drv2.DrawLine((ushort)y1);
            y1 = Modbus.ValueFloat(Modbus.Holding_Registers, ModbusRTU.DRV2_LIMIT_VIB_SPEED_1_Z_Lo);
            Vibration_1_Z_drv2.DrawLine((ushort)y1);
            y1 = Modbus.ValueFloat(Modbus.Holding_Registers, ModbusRTU.DRV2_LIMIT_VIB_SPEED_2_Z_Lo);
            Vibration_2_Z_drv2.DrawLine((ushort)y1);

            Temp_drv1.AddPoint(POINT_X, Modbus.ValueFloat(Modbus.Input_Registers, ModbusRTU.DRV1_TEMP_Lo));
            Speed_Motor_drv1.AddPoint(POINT_X, Modbus.ValueFloat(Modbus.Input_Registers, ModbusRTU.DRV1_ROT_FREQ_MOTOR_Lo));
            Speed_Generator_1_drv1.AddPoint(POINT_X, Modbus.ValueFloat(Modbus.Input_Registers, ModbusRTU.DRV1_ROT_FREQ_GEN_1_Lo));
            Speed_Generator_2_drv1.AddPoint(POINT_X, Modbus.ValueFloat(Modbus.Input_Registers, ModbusRTU.DRV1_ROT_FREQ_GEN_2_Lo));
            Vibration_1_Z_drv1.AddPoint(POINT_X, Modbus.ValueFloat(Modbus.Input_Registers, ModbusRTU.DRV1_VIB_SPEED_1_Z_Lo));
            Vibration_2_Z_drv1.AddPoint(POINT_X, Modbus.ValueFloat(Modbus.Input_Registers, ModbusRTU.DRV1_VIB_SPEED_2_Z_Lo));

            Temp_drv2.AddPoint(POINT_X, Modbus.ValueFloat(Modbus.Input_Registers, ModbusRTU.DRV2_TEMP_Lo));
            Speed_Motor_drv2.AddPoint(POINT_X, Modbus.ValueFloat(Modbus.Input_Registers, ModbusRTU.DRV2_ROT_FREQ_MOTOR_Lo));
            Speed_Generator_1_drv2.AddPoint(POINT_X, Modbus.ValueFloat(Modbus.Input_Registers, ModbusRTU.DRV2_ROT_FREQ_GEN_1_Lo));
            Speed_Generator_2_drv2.AddPoint(POINT_X, Modbus.ValueFloat(Modbus.Input_Registers, ModbusRTU.DRV2_ROT_FREQ_GEN_2_Lo));
            Vibration_1_Z_drv2.AddPoint(POINT_X, Modbus.ValueFloat(Modbus.Input_Registers, ModbusRTU.DRV2_VIB_SPEED_1_Z_Lo));
            Vibration_2_Z_drv2.AddPoint(POINT_X, Modbus.ValueFloat(Modbus.Input_Registers, ModbusRTU.DRV2_VIB_SPEED_2_Z_Lo));


            Temp_drv1.UpdateLine();
            Speed_Motor_drv1.UpdateLine();
            Speed_Generator_1_drv1.UpdateLine();
            Speed_Generator_2_drv1.UpdateLine();
            Vibration_1_Z_drv1.UpdateLine();
            Vibration_2_Z_drv1.UpdateLine();

            Temp_drv2.UpdateLine();
            Speed_Motor_drv2.UpdateLine();
            Speed_Generator_1_drv2.UpdateLine();
            Speed_Generator_2_drv2.UpdateLine();
            Vibration_1_Z_drv2.UpdateLine();
            Vibration_2_Z_drv2.UpdateLine();

        }

        private Boolean Push_Button_Connect = true;
        private Boolean CP_open = false;

        private void Button_Connect_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                if (Push_Button_Connect)
                {
                    if (port.Open(comboBox1.SelectedItem.ToString(), 9600))
                    {
                        Push_Button_Connect = false;
                        Button_Connect.Text = "Отключить";
                        label_stat_connect.Text = "Подключен";
                        timer1.Enabled = true;
                        Enable_TabControl(true);

                        CP_open = true;
                    }
                    else
                    {
                        label_stat_connect.Text = "Занят";
                        CP_open = false;
                    }
                }
                else
                {
                    Push_Button_Connect = true;
                    Button_Connect.Text = "Подключить";
                    label_stat_connect.Text = "Отключен";
                    Enable_TabControl(false);
                    port.Close();
                    CP_open = false;

                }
            }
        }

        private Boolean Push_Button_Start = true;

        private void Button_Start_Click(object sender, EventArgs e)
        {
            if(Push_Button_Start)
            {
                timer1.Start();
                timer2.Start();
                timer3.Start();
            }
            else
            {
                timer1.Stop();
                timer2.Stop();
                timer3.Stop();
            }
        }

        private void Enable_TabControl(Boolean stat)
        {
            //tabControl_drv_1.Enabled = stat;
            //tabControl_drv_2.Enabled = stat;

            tabControl_drv.Enabled = stat;
        }


        private int index_tab_drv = 0;
        private int latest_index_tab_drv = 0;

        private void Heandler_TabControl()
        {
            index_tab_drv = tabControl_drv.SelectedIndex;

            if(index_tab_drv == 0)
            {
                latest_index_tab_drv = tabControl_drv_1.SelectedIndex;
            }

            if (index_tab_drv == 1)
            {
                latest_index_tab_drv = tabControl_drv_2.SelectedIndex;
            }

            tabControl_drv_1.SelectedIndex = latest_index_tab_drv;

            tabControl_drv_2.SelectedIndex = latest_index_tab_drv;
        }


        private void but_threshold_1_Click(object sender, EventArgs e)
        {
            float f;
            byte[] byteArray;
            data_limit[ModbusRTU.DRV1_COMMAND_Lo] = ModbusRTU.COMMAND_WRITE_FLASH;
            f = Convert.ToSingle(threshold_temp_1.Value);
            byteArray = BitConverter.GetBytes(f);
            data_limit[ModbusRTU.DRV1_LIMIT_TEMP_Hi] = (ushort)((byteArray[3] << 8) | byteArray[2]);
            data_limit[ModbusRTU.DRV1_LIMIT_TEMP_Lo] = (ushort)((byteArray[1] << 8) | byteArray[0]);
            f = Convert.ToSingle(ratio_11.Value);
            byteArray = BitConverter.GetBytes(f);
            data_limit[ModbusRTU.DRV1_LIMIT_RATIO_1_Hi] = (ushort)((byteArray[3] << 8) | byteArray[2]);
            data_limit[ModbusRTU.DRV1_LIMIT_RATIO_1_Lo] = (ushort)((byteArray[1] << 8) | byteArray[0]);
            f = Convert.ToSingle(ratio_12.Value);
            byteArray = BitConverter.GetBytes(f);
            data_limit[ModbusRTU.DRV1_LIMIT_RATIO_2_Hi] = (ushort)((byteArray[3] << 8) | byteArray[2]);
            data_limit[ModbusRTU.DRV1_LIMIT_RATIO_2_Lo] = (ushort)((byteArray[1] << 8) | byteArray[0]);
            f = Convert.ToSingle(threshold_vib_11.Value);
            byteArray = BitConverter.GetBytes(f);
            data_limit[ModbusRTU.DRV1_LIMIT_VIB_SPEED_1_Z_Hi] = (ushort)((byteArray[3] << 8) | byteArray[2]);
            data_limit[ModbusRTU.DRV1_LIMIT_VIB_SPEED_1_Z_Lo] = (ushort)((byteArray[1] << 8) | byteArray[0]);
            f = Convert.ToSingle(threshold_vib_12.Value);
            byteArray = BitConverter.GetBytes(f);
            data_limit[ModbusRTU.DRV1_LIMIT_VIB_SPEED_2_Z_Hi] = (ushort)((byteArray[3] << 8) | byteArray[2]);
            data_limit[ModbusRTU.DRV1_LIMIT_VIB_SPEED_2_Z_Lo] = (ushort)((byteArray[1] << 8) | byteArray[0]);

            mun_next_pack = 13;
        }

        private void but_threshold_2_Click(object sender, EventArgs e)
        {
            float f;
            byte[] byteArray;
            data_limit[ModbusRTU.DRV2_COMMAND_Lo] = ModbusRTU.COMMAND_WRITE_FLASH;
            f = Convert.ToSingle(threshold_temp_2.Value);
            byteArray = BitConverter.GetBytes(f);
            data_limit[ModbusRTU.DRV2_LIMIT_TEMP_Hi] = (ushort)((byteArray[3] << 8) | byteArray[2]);
            data_limit[ModbusRTU.DRV2_LIMIT_TEMP_Lo] = (ushort)((byteArray[1] << 8) | byteArray[0]);
            f = Convert.ToSingle(ratio_21.Value);
            byteArray = BitConverter.GetBytes(f);
            data_limit[ModbusRTU.DRV2_LIMIT_RATIO_1_Hi] = (ushort)((byteArray[3] << 8) | byteArray[2]);
            data_limit[ModbusRTU.DRV2_LIMIT_RATIO_1_Lo] = (ushort)((byteArray[1] << 8) | byteArray[0]);
            f = Convert.ToSingle(ratio_22.Value);
            byteArray = BitConverter.GetBytes(f);
            data_limit[ModbusRTU.DRV2_LIMIT_RATIO_2_Hi] = (ushort)((byteArray[3] << 8) | byteArray[2]);
            data_limit[ModbusRTU.DRV2_LIMIT_RATIO_2_Lo] = (ushort)((byteArray[1] << 8) | byteArray[0]);
            f = Convert.ToSingle(threshold_vib_21.Value);
            byteArray = BitConverter.GetBytes(f);
            data_limit[ModbusRTU.DRV2_LIMIT_VIB_SPEED_1_Z_Hi] = (ushort)((byteArray[3] << 8) | byteArray[2]);
            data_limit[ModbusRTU.DRV2_LIMIT_VIB_SPEED_1_Z_Lo] = (ushort)((byteArray[1] << 8) | byteArray[0]);
            f = Convert.ToSingle(threshold_vib_22.Value);
            byteArray = BitConverter.GetBytes(f);
            data_limit[ModbusRTU.DRV2_LIMIT_VIB_SPEED_2_Z_Hi] = (ushort)((byteArray[3] << 8) | byteArray[2]);
            data_limit[ModbusRTU.DRV2_LIMIT_VIB_SPEED_2_Z_Lo] = (ushort)((byteArray[1] << 8) | byteArray[0]);

            mun_next_pack = 14;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
            timer2.Stop();
            timer3.Stop();

            timer1.Enabled = false;
            timer2.Enabled = false;
            timer3.Enabled = false;
            if (CP_open) port.Close();
        }

    }
}
