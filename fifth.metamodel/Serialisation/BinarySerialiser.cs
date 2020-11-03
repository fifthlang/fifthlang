namespace Fifth.Serialisation
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    public static class BinarySerialiser
    {
        public static T Deserialise<T>(this byte[] buf) where T : new()
        {
            using (var s = new MemoryStream(buf, false))
            {
                return Deserialise<T>(s);
            }
        }

        public static T Deserialise<T>(this Stream fs) where T : new()
        {
            if (!fs.CanRead)
            {
                throw new Fifth.Exception("Unable to read from stream");
            }

            return (T)new BinaryFormatter().Deserialize(fs);
        }

        public static byte[] ReadToEnd(this Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                var readBuffer = new byte[4096];

                var totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        var nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                var buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }

        public static byte[] Serialise<T>(this T obj)
        {
            using (var s = new MemoryStream())
            {
                s.Serialise(obj);
                return s.ReadToEnd();
            }
        }

        public static void Serialise<T>(this Stream fs, T obj)
        {
            if (!fs.CanWrite)
            {
                throw new Fifth.Exception("Unable to write to stream");
            }

            new BinaryFormatter().Serialize(fs, obj);
        }
    }
}
