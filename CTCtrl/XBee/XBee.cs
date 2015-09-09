using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBee
{
    public class XBee
    {
        public enum FrameType : byte
        {
            TxRequest64 = 0x00,
            TxRequest16 = 0x01,
            RemoteATCmdWiFi = 0x07,
            ATCommand = 0x08,
            ATCmdQueueRegisterValue = 0x09,
            TransmitRequest = 0x10,
            ExplicitAddressingCmd = 0x11,
            RemoteATCmd = 0x17,
            TxIPv4 = 0x20,
            CreateSourceRoute = 0x21,
            RegisterJoinDevice = 0x24,
            PutRequest = 0x28,
            DeviceResponse = 0x29,
            RxPacket64 = 0x80,
            RxPacket16 = 0x81,
            RxPacket64IO = 0x82,
            RxPacket16IO = 0x83,
            RemoteATCmdWiFiResponse = 0x87,
            ATCommandResponse = 0x88,
            TxStatus = 0x89,
            ModemStatus = 0x8A,
            TransmitStatus = 0x8B,
            RouteInfomationPacket = 0x8D,
            AggregateAddressingUpdate = 0x8E,
            IODataSampleIndicator = 0x8F,
            ReceivePacket = 0x90,
            ExplicitRxIndicator = 0x91,
            IODataSampleRxIndicator = 0x92,
            XBeeSensorReadIndicator = 0x94,
            NodeIndenficationIndicator = 0x95,
            RemoteATCmdResponse = 0x97,
            ExtendedModemStatus = 0x98,
            OtAFirmwareUpdateStatus = 0xA0,
            RouteRecordIndicator = 0xA1,
            DeviceAuthenticatedIndicator = 0xA2,
            MtORouteRecordIndicator = 0xA3,
            RegisterJoinDeviceStatus = 0xA4,
            JoinNotificationStatus = 0xA5,
            RxIPv4 = 0xB0,
            PutResponse = 0xB8,
            DeviceRequest = 0xB9,
            DeviceResponseStatus = 0xBA,
            FrameError = 0xFE,
            Generic = 0xFF
        }

        public static Frame RecvFrame(System.IO.Ports.SerialPort port)
        {
            int readByte;
            int length;
            List<byte> buffer = new List<byte>();

            // find Delimiter
            do
            {
                readByte = port.ReadByte();
            } while (readByte != 0x7E);

            length = port.ReadByte();
            length = (length << 8) | port.ReadByte();

            // read frame data
            for (int i = 0; i < length; i++)
                buffer.Add((byte)port.ReadByte());

            return new Frame(buffer.ToArray(), (byte)port.ReadByte());
        }

        public static void SendFrame(Frame frame, System.IO.Ports.SerialPort port)
        {
            byte[] buffer;
            port.Write(new byte[] { Frame.Delimiter }, 0, 1);
            buffer = BitConverter.GetBytes(frame.Length);
            Array.Reverse(buffer);
            port.Write(buffer, 0, buffer.Length);
            port.Write(frame.FrameData, 0, frame.Length);
            port.Write(new byte[] { Checksum(frame.FrameData) }, 0, 1);
        }

        private static byte Checksum(byte[] array)
        {
            byte sum = array[0];
            for (int i = 1; i < array.Length; i++)
                sum ^= array[i];

            return sum;
        }

        private static byte GetByte(List<byte> list, int index)
        {
            return list[index];
        }

        private static void SetByte(byte value, List<byte> list, int index)
        {
            if (list.Count > index)
                list.RemoveAt(index);
            list.Insert(index, value);
        }

        private static UInt64 GetAddress64(List<byte> list, int index)
        {
            byte[] buffer = new byte[8];
            list.CopyTo(index, buffer, 0, buffer.Length);
            Array.Reverse(buffer);
            return BitConverter.ToUInt64(buffer, 0);
        }

        private static void SetAddress64(UInt64 value, List<byte> list, int index)
        {
            if (list.Count > index)
                list.RemoveRange(index, Math.Min(sizeof(UInt64), list.Count - index));

            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            list.InsertRange(index, buffer);
        }

        private static UInt16 GetAddress16(List<byte> list, int index)
        {
            byte[] buffer = new byte[2];
            list.CopyTo(index, buffer, 0, buffer.Length);
            Array.Reverse(buffer);
            return BitConverter.ToUInt16(buffer, 0);
        }

        private static void SetAddress16(UInt16 value, List<byte> list, int index)
        {
            if (list.Count > index)
                list.RemoveRange(index, Math.Min(sizeof(UInt16), list.Count - index));

            byte[] buffer = BitConverter.GetBytes(value);
            Array.Reverse(buffer);
            list.InsertRange(index, buffer);
        }

        public class Frame
        {
            public const byte Delimiter = 0x7F;

            protected List<byte> frameData = new List<byte>();

            protected byte checksum;

            public ushort Length
            {
                get { return (ushort)frameData.Count; }
                protected set { }
            }

            public FrameType FrameType
            {
                get { return (FrameType)frameData[0]; }
                protected set { }
            }

            public byte[] FrameData
            {
                get { return frameData.ToArray(); }
                protected set
                {
                    frameData.Clear();
                    frameData.AddRange(value);
                }
            }

            public byte Checksum
            {
                get { return checksum; }
                protected set { }
            }

            public Frame()
            {

            }

            public Frame(byte[] bytes, byte checksum)
            {
                this.frameData.AddRange(bytes);
                this.checksum = checksum;
            }

            public void Add(byte data)
            {
                this.frameData.Add(data);
            }

            public void AddRenge(IEnumerable<byte> data)
            {
                this.frameData.AddRange(data);
            }
        }

        public class TransmitRequest : Frame
        {
            public byte FrameID
            {
                get { return GetByte(this.frameData, 1); }
                set { SetByte(value, this.frameData, 1); }
            }

            public UInt64 Address64
            {
                get { return GetAddress64(this.frameData, 2); }
                set { SetAddress64(value, this.frameData, 2); }
            }

            public UInt16 Address16
            {
                get { return GetAddress16(this.frameData, 10); }
                set { SetAddress16(value, this.frameData, 10); }
            }

            public TransmitRequest()
            {
            }

            public TransmitRequest(byte frameID, UInt64 address64, UInt16 address16 = 0xFFFE)
            {
                this.FrameID = frameID;
                this.Address64 = address64;
                this.Address16 = address16;
            }
        }

        public class ReceivePacket : Frame
        {
            public UInt64 Address64
            {
                get { return GetAddress64(this.frameData, 1); }
                set { SetAddress64(value, this.frameData, 1); }
            }

            public UInt16 Address16
            {
                get { return GetAddress16(this.frameData, 9); }
                set { SetAddress16(value, this.frameData, 9); }
            }

            public byte Options
            {
                get { return GetByte(this.frameData, 11); }
                set { SetByte(value, this.frameData, 11); }
            }

            public byte[] Data
            {
                get {
                    byte[] buffer = new byte[this.Length - 12];
                    this.frameData.CopyTo(12, buffer, 0, buffer.Length);
                    return buffer;
                }
            }

            public ReceivePacket()
            {
            }

            public ReceivePacket(Frame frame)
                : base(frame.FrameData, frame.Checksum)
            {
            }

            public ReceivePacket(UInt64 address64, UInt16 address16 = 0xFFFE)
            {
                this.Address64 = address64;
                this.Address16 = address16;
            }
        }
    }
}
