using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MonitorMechanics
{
    class ModbusRTU
    {

        public const byte Device_ID = 88;

        private const byte INDEX_ADDR = 0x00;
        private const byte INDEX_COMMAND = 0x01;
        private const byte INDEX_COUNT_REG_HI = 0x02;
        private const byte INDEX_COUNT_REG_LO = 0x03;

        public const byte COMMAND_READ_HOLDING_REG = 0x03;
        public const byte COMMAND_READ_INPUT_REG = 0x04;
        public const byte COMMAND_WRITE_MULTIPLE_REG = 0x10;


        public ushort ERROR_Handler_Master = 0;
        public const byte ERROR_CRC16 = 0x01;

        //private byte COUNT_REG = 0;

        public const ushort ALARM_SMOKE = 0x01;
        public const ushort ALARM_TEMP  = 0x02;
        public const ushort ALARM_SPEED = 0x04;
        public const ushort ALARM_VIB_1 = 0x08;
        public const ushort ALARM_VIB_2 = 0x16;

        public const byte COMMAND_WRITE_FLASH = 0x01;

        //Holding Registers
        public const ushort DRV1_COMMAND_Lo             = 0;
        public const ushort DRV1_COMMAND_Hi             = 1;
        public const ushort DRV1_LIMIT_TEMP_Lo          = 2;
        public const ushort DRV1_LIMIT_TEMP_Hi          = 3;
        public const ushort DRV1_LIMIT_RATIO_1_Lo       = 4;
        public const ushort DRV1_LIMIT_RATIO_1_Hi       = 5;
        public const ushort DRV1_LIMIT_RATIO_2_Lo       = 6;
        public const ushort DRV1_LIMIT_RATIO_2_Hi       = 7;
        public const ushort DRV1_LIMIT_VIB_SPEED_1_Z_Lo = 8;
        public const ushort DRV1_LIMIT_VIB_SPEED_1_Z_Hi = 9;
        public const ushort DRV1_LIMIT_VIB_SPEED_2_Z_Lo = 10;
        public const ushort DRV1_LIMIT_VIB_SPEED_2_Z_Hi = 11;

        public const ushort DRV2_COMMAND_Lo             = 12;
        public const ushort DRV2_COMMAND_Hi             = 13;
        public const ushort DRV2_LIMIT_TEMP_Lo          = 14;
        public const ushort DRV2_LIMIT_TEMP_Hi          = 15;
        public const ushort DRV2_LIMIT_RATIO_1_Lo       = 16;
        public const ushort DRV2_LIMIT_RATIO_1_Hi       = 17;
        public const ushort DRV2_LIMIT_RATIO_2_Lo       = 18;
        public const ushort DRV2_LIMIT_RATIO_2_Hi       = 19;
        public const ushort DRV2_LIMIT_VIB_SPEED_1_Z_Lo = 20;
        public const ushort DRV2_LIMIT_VIB_SPEED_1_Z_Hi = 21;
        public const ushort DRV2_LIMIT_VIB_SPEED_2_Z_Lo = 22;
        public const ushort DRV2_LIMIT_VIB_SPEED_2_Z_Hi = 23;

        //Input Registers
        public const ushort DRV1_ERROR              = 0;
        public const ushort DRV1_CODE_ALARM         = 1;
        public const ushort DRV1_TEMP_Lo            = 2;
        public const ushort DRV1_TEMP_Hi            = 3;
        public const ushort DRV1_ROT_FREQ_MOTOR_Lo  = 4;
        public const ushort DRV1_ROT_FREQ_MOTOR_Hi  = 5;
        public const ushort DRV1_ROT_FREQ_GEN_1_Lo  = 6;
        public const ushort DRV1_ROT_FREQ_GEN_1_Hi  = 7;
        public const ushort DRV1_ROT_FREQ_GEN_2_Lo  = 8;
        public const ushort DRV1_ROT_FREQ_GEN_2_Hi  = 9;
        public const ushort DRV1_RATIO_1_Lo         = 10;
        public const ushort DRV1_RATIO_1_Hi         = 11;
        public const ushort DRV1_RATIO_2_Lo         = 12;
        public const ushort DRV1_RATIO_2_Hi         = 13;
        public const ushort DRV1_VIB_SPEED_1_Z_Lo   = 14;
        public const ushort DRV1_VIB_SPEED_1_Z_Hi   = 15;
        public const ushort DRV1_VIB_SPEED_2_Z_Lo   = 16;
        public const ushort DRV1_VIB_SPEED_2_Z_Hi   = 17;

        public const ushort DRV2_ERROR              = 18;
        public const ushort DRV2_CODE_ALARM         = 19;
        public const ushort DRV2_TEMP_Lo            = 20;
        public const ushort DRV2_TEMP_Hi            = 21;
        public const ushort DRV2_ROT_FREQ_MOTOR_Lo  = 22;
        public const ushort DRV2_ROT_FREQ_MOTOR_Hi  = 23;
        public const ushort DRV2_ROT_FREQ_GEN_1_Lo  = 24;
        public const ushort DRV2_ROT_FREQ_GEN_1_Hi  = 25;
        public const ushort DRV2_ROT_FREQ_GEN_2_Lo  = 26;
        public const ushort DRV2_ROT_FREQ_GEN_2_Hi  = 27;
        public const ushort DRV2_RATIO_1_Lo         = 28;
        public const ushort DRV2_RATIO_1_Hi         = 29;
        public const ushort DRV2_RATIO_2_Lo         = 30;
        public const ushort DRV2_RATIO_2_Hi         = 31;
        public const ushort DRV2_VIB_SPEED_1_Z_Lo   = 32;
        public const ushort DRV2_VIB_SPEED_1_Z_Hi   = 33;
        public const ushort DRV2_VIB_SPEED_2_Z_Lo   = 34;
        public const ushort DRV2_VIB_SPEED_2_Z_Hi   = 35;



        public ushort[] Holding_Registers = new ushort[300];
        public ushort[] Input_Registers = new ushort[300];

        public byte[] out_buff = new byte[100];
        public ushort out_lenght = 0;
        public ushort in_lenght = 0;
        public Boolean Enable_Send = false;
        public Boolean Pack_Is_Received = false;
        //private ushort COUNT_INDEX = 0;
        private ushort Start_count = 0;
        private ushort Count_reg = 0;
        private byte Count_byte = 0;
        //private ushort End_count = 0;
        private byte Index_buff = 0;
        private ushort crc_calc = 0;
        private ushort crc_pack = 0;

        public void Handler_Master(byte[] buff)
        {

            crc_calc = CRC16(buff, in_lenght - 2);
            crc_pack = (ushort)((buff[in_lenght - 2] << 8) | buff[in_lenght - 1]);

            if (crc_calc == crc_pack)
            {
                if(buff[INDEX_ADDR] == 77)
                {
                    switch(buff[INDEX_COMMAND])
                    {
                        case COMMAND_WRITE_MULTIPLE_REG:
                            buff[0] = 0;

                            //Start_count = (byte)(buff[2] << 8 | buff[3]);
                            Count_reg = (byte)(buff[4] << 8 | buff[5]);
                            //End_count = (byte)(Start_count + Count_reg);
                            Index_buff = 7;
                            /*
                            for (COUNT_INDEX = Start_count; COUNT_INDEX < End_count; COUNT_INDEX++)
                            {
                                register[COUNT_INDEX] = (ushort)((buff[Index_buff] << 8) | buff[Index_buff+1]);
                                Index_buff+=2;
                            }
                            Index_buff = 0;
                            */
                            out_buff[INDEX_ADDR] = Device_ID;
                            out_buff[INDEX_COMMAND] = COMMAND_WRITE_MULTIPLE_REG;
                            out_buff[INDEX_COUNT_REG_HI] = (byte)(Count_reg >> 8);
                            out_buff[INDEX_COUNT_REG_LO] = (byte)(Count_reg & 0xFF);
                            out_buff[4] = (byte)(CRC16(out_buff, 4) >> 8);
                            out_buff[5] = (byte)(CRC16(out_buff, 4) & 0xFF);

                            out_lenght = 6;
                            Enable_Send = true;
                            Pack_Is_Received = true;
                            break;

                        case COMMAND_READ_HOLDING_REG:
                            buff[0] = 0;

                            Write_REG(Holding_Registers, buff, Start_count, Count_reg);
                            break;
                        case COMMAND_READ_INPUT_REG:
                            buff[0] = 0;

                            Write_REG(Input_Registers, buff, Start_count, Count_reg);
                            break;
                    }
                }
            }
            else
            {
                ERROR_Handler_Master = ERROR_CRC16; 
            }
        }

        private void Write_REG(ushort[] buff_reg, byte[] buff, ushort start_reg, ushort count_reg)
        {
            ushort COUNT_INDEX = 0;
            ushort End_count = 0;

            End_count = (ushort)(Start_count + Count_reg);

            Index_buff = 3;

            for (COUNT_INDEX = Start_count; COUNT_INDEX < End_count; COUNT_INDEX++)
            {
                buff_reg[COUNT_INDEX] = (ushort)((buff[Index_buff] << 8) | buff[Index_buff + 1]);
                Index_buff += 2;
            }
            Index_buff = 0;
        }

        public void READ_HOLDING_Pack(byte addr_slave, ushort start_reg, ushort count_reg)
        {
            out_buff[0] = addr_slave;
            out_buff[1] = COMMAND_READ_HOLDING_REG;
            out_buff[2] = (byte)(start_reg >> 8);
            out_buff[3] = (byte)(start_reg & 0xFF);
            out_buff[4] = (byte)(count_reg >> 8);
            out_buff[5] = (byte)(count_reg & 0xFF);
            out_buff[6] = (byte)(CRC16(out_buff, 6) >> 8);
            out_buff[7] = (byte)(CRC16(out_buff, 6)  & 0xFF);

            Start_count = start_reg;
            Count_reg = count_reg;
            out_lenght = 8;
            in_lenght = (byte)((count_reg * 2) + 5);
        }

        public void READ_INPUT_Pack(byte addr_slave, ushort start_reg, ushort count_reg)
        {
            out_buff[0] = addr_slave;
            out_buff[1] = COMMAND_READ_INPUT_REG;
            out_buff[2] = (byte)(start_reg >> 8);
            out_buff[3] = (byte)(start_reg & 0xFF);
            out_buff[4] = (byte)(count_reg >> 8);
            out_buff[5] = (byte)(count_reg & 0xFF);
            out_buff[6] = (byte)(CRC16(out_buff, 6) >> 8);
            out_buff[7] = (byte)(CRC16(out_buff, 6) & 0xFF);

            Start_count = start_reg;
            Count_reg = count_reg;
            in_lenght = (byte)((count_reg * 2) + 5);
            out_lenght = 8;
            
        }

        public void WRITE_MULTIPLE_REG_Pack(byte addr_slave, ushort start_reg, ushort count_reg, ushort[] buff)
        {
            ushort i = 0;
            ushort j = start_reg;

            out_buff[0] = addr_slave;
            out_buff[1] = COMMAND_WRITE_MULTIPLE_REG;
            out_buff[2] = (byte)(start_reg >> 8);
            out_buff[3] = (byte)(start_reg & 0xFF);
            out_buff[4] = (byte)(count_reg >> 8);
            out_buff[5] = (byte)(count_reg & 0xFF);
            Count_byte = (byte)(count_reg * 2);
            out_buff[6] = Count_byte;
            
            for (i = 7; i< (Count_byte+7); i+=2)
            {
                out_buff[i] = (byte)(buff[j] >> 8); ;
                out_buff[i+1] = (byte)(buff[j] & 0xFF); ;
                j++;
            }

            out_buff[i] = (byte)(CRC16(out_buff, i ) >> 8);
            out_buff[i+1] = (byte)(CRC16(out_buff, i ) & 0xFF);

            out_lenght = (ushort)(i + 2);
        }

        public float ValueFloat(ushort[] regs, ushort start_reg)
        {
            float value = 0;
            byte[] bytes = new byte[4];
            bytes[0] = (byte)(regs[start_reg] & 0xFF);
            bytes[1] = (byte)(regs[start_reg] >> 8);
            bytes[2] = (byte)(regs[start_reg+1] & 0xFF);
            bytes[3] = (byte)(regs[start_reg+1] >> 8);
            value = BitConverter.ToSingle(bytes, 0);
            return value;
        }


        // Расчет CRC16 для проверки пакетов ModBusRTU
        // для добавления в пакет старший и младший байты должны быть поменяны местами
        static readonly ushort[] crc16Table = new ushort[]
        {
        0x0000, 0xC1C0, 0x81C1, 0x4001, 0x01C3, 0xC003, 0x8002, 0x41C2,
        0x01C6, 0xC006, 0x8007, 0x41C7, 0x0005, 0xC1C5, 0x81C4, 0x4004,
        0x01CC, 0xC00C, 0x800D, 0x41CD, 0x000F, 0xC1CF, 0x81CE, 0x400E,
        0x000A, 0xC1CA, 0x81CB, 0x400B, 0x01C9, 0xC009, 0x8008, 0x41C8,
        0x01D8, 0xC018, 0x8019, 0x41D9, 0x001B, 0xC1DB, 0x81DA, 0x401A,
        0x001E, 0xC1DE, 0x81DF, 0x401F, 0x01DD, 0xC01D, 0x801C, 0x41DC,
        0x0014, 0xC1D4, 0x81D5, 0x4015, 0x01D7, 0xC017, 0x8016, 0x41D6,
        0x01D2, 0xC012, 0x8013, 0x41D3, 0x0011, 0xC1D1, 0x81D0, 0x4010,
        0x01F0, 0xC030, 0x8031, 0x41F1, 0x0033, 0xC1F3, 0x81F2, 0x4032,
        0x0036, 0xC1F6, 0x81F7, 0x4037, 0x01F5, 0xC035, 0x8034, 0x41F4,
        0x003C, 0xC1FC, 0x81FD, 0x403D, 0x01FF, 0xC03F, 0x803E, 0x41FE,
        0x01FA, 0xC03A, 0x803B, 0x41FB, 0x0039, 0xC1F9, 0x81F8, 0x4038,
        0x0028, 0xC1E8, 0x81E9, 0x4029, 0x01EB, 0xC02B, 0x802A, 0x41EA,
        0x01EE, 0xC02E, 0x802F, 0x41EF, 0x002D, 0xC1ED, 0x81EC, 0x402C,
        0x01E4, 0xC024, 0x8025, 0x41E5, 0x0027, 0xC1E7, 0x81E6, 0x4026,
        0x0022, 0xC1E2, 0x81E3, 0x4023, 0x01E1, 0xC021, 0x8020, 0x41E0,
        0x01A0, 0xC060, 0x8061, 0x41A1, 0x0063, 0xC1A3, 0x81A2, 0x4062,
        0x0066, 0xC1A6, 0x81A7, 0x4067, 0x01A5, 0xC065, 0x8064, 0x41A4,
        0x006C, 0xC1AC, 0x81AD, 0x406D, 0x01AF, 0xC06F, 0x806E, 0x41AE,
        0x01AA, 0xC06A, 0x806B, 0x41AB, 0x0069, 0xC1A9, 0x81A8, 0x4068,
        0x0078, 0xC1B8, 0x81B9, 0x4079, 0x01BB, 0xC07B, 0x807A, 0x41BA,
        0x01BE, 0xC07E, 0x807F, 0x41BF, 0x007D, 0xC1BD, 0x81BC, 0x407C,
        0x01B4, 0xC074, 0x8075, 0x41B5, 0x0077, 0xC1B7, 0x81B6, 0x4076,
        0x0072, 0xC1B2, 0x81B3, 0x4073, 0x01B1, 0xC071, 0x8070, 0x41B0,
        0x0050, 0xC190, 0x8191, 0x4051, 0x0193, 0xC053, 0x8052, 0x4192,
        0x0196, 0xC056, 0x8057, 0x4197, 0x0055, 0xC195, 0x8194, 0x4054,
        0x019C, 0xC05C, 0x805D, 0x419D, 0x005F, 0xC19F, 0x819E, 0x405E,
        0x005A, 0xC19A, 0x819B, 0x405B, 0x0199, 0xC059, 0x8058, 0x4198,
        0x0188, 0xC048, 0x8049, 0x4189, 0x004B, 0xC18B, 0x818A, 0x404A,
        0x004E, 0xC18E, 0x818F, 0x404F, 0x018D, 0xC04D, 0x804C, 0x418C,
        0x0044, 0xC184, 0x8185, 0x4045, 0x0187, 0xC047, 0x8046, 0x4186,
        0x0182, 0xC042, 0x8043, 0x4183, 0x0041, 0xC181, 0x8180, 0x4040
        };

        private static ushort CRC16(byte[] bytes, int len)
        {
            ushort crc = 0xFFFF;
            for (var i = 0; i < len; i++)
                crc = (ushort)((crc << 8) ^ crc16Table[(crc >> 8) ^ bytes[i]]);
            return crc;
        }
    }

}
