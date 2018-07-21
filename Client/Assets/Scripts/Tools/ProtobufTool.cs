using System.IO;
using ProtoBuf;


public class ProtobufTool
{

    public static byte[] ProtoBufToBytes<T>(T t)
    {
        byte[] result;
        using (MemoryStream memoryStream = new MemoryStream())
        {
            Serializer.Serialize<T>(memoryStream, t);
            result = memoryStream.ToArray();
        }
        return result;
    }

    public static T BytesToProtoBuf<T>(byte[] bytes)
    {
        T result;
        using (MemoryStream memoryStream = new MemoryStream(bytes, 0, bytes.Length))
        {
            result = Serializer.Deserialize<T>(memoryStream);
        }
        return result;
    }

    public static T BytesToProtoBuf<T>(byte[] bytes,int offset,int length)
    {
        T result;
        using (MemoryStream memoryStream = new MemoryStream(bytes, offset, length))
        {
            result = Serializer.Deserialize<T>(memoryStream);
        }
        return result;
    }
}


