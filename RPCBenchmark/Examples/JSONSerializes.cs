using Swifter.Json;
using System;
using System.Collections.Generic;
using System.Text;
using ZYSocket.Interface;

namespace NetxSerializes
{
    public class JSONSerializes : ISerialization
    {
        public T Deserialize<T>(byte[] data, int offset, int length)
        => JsonFormatter.DeserializeObject<T>(new ReadOnlySpan<byte>(data, offset, length), Encoding.ASCII, JsonFormatterOptions.DeflateDeserialize);


        public object Deserialize(Type type, byte[] data, int offset, int length)
        => JsonFormatter.DeserializeObject(new ReadOnlySpan<byte>(data, offset, length), Encoding.ASCII, type, JsonFormatterOptions.DeflateDeserialize);


        public byte[] Serialize(object obj)
          => JsonFormatter.SerializeObject<object>(obj, Encoding.ASCII, JsonFormatterOptions.DeflateDeserialize);


    }
}
