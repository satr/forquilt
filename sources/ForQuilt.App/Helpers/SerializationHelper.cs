//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ForQuilt.App.Helpers
{
    static class SerializationHelper
    {
        public static MemoryStream SerializeBinary(object request)
        {
            var serializer = new BinaryFormatter();
            var memStream = new MemoryStream();
            serializer.Serialize(memStream, request);
            return memStream;
        }

        public static object DeSerializeBinary(MemoryStream memStream)
        {
            memStream.Position = 0;
            var deserializer = new BinaryFormatter();
            object newobj = deserializer.Deserialize(memStream);
            memStream.Close();
            return newobj;
        }
    }
}
