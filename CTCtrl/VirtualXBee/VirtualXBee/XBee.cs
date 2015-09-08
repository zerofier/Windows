using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VirtualXBee
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

        public static Frame recvFrame(System.IO.Ports.SerialPort port)
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

        public static byte[] union(params object[] list)
        {
            List<byte> buffer = new List<byte>();

            foreach (object param in list)
            {
                if (param.GetType() == typeof(byte))
                    buffer.Add((byte)param);
                else if (param.GetType() == typeof(byte[]))
                    buffer.AddRange((byte[])param);
            }

            return buffer.ToArray();
        }

        public class Frame
        {
            public const byte Delimiter = 0x7F;

            protected byte[] frameData;

            protected byte checksum;

            public ushort Length
            {
                get { return (ushort)frameData.Length; }
            }

            public FrameType FrameType
            {
                get { return (FrameType)frameData[0]; }
            }

            public byte[] FrameData
            {
                get { return frameData; }
            }

            public byte Checksum
            {
                get { return checksum; }
            }

            public Frame(byte[] bytes, byte checksum)
            {
                this.frameData = new byte[bytes.Length];
                Array.Copy(bytes, frameData, frameData.Length);
                this.checksum = checksum;
            }
        }


        public class TransmitRequest : Frame
        {
            public TransmitRequest(byte frameID, byte[] address64, byte[] address16, byte[] data)
                : base(XBee.union(new object[] { frameID, address64, address16, data }), 0)
            {
            }
        }


        public class ReceivePacket : Frame
        {
            public byte[] Address64
            {
                get { return new byte[] { frameData[1], frameData[2], frameData[3], frameData[4], frameData[5], frameData[6], frameData[7], frameData[8] }; }
            }

            public byte[] Address16
            {
                get { return new byte[] { frameData[9], frameData[10] }; }
            }
        }
    }
}
