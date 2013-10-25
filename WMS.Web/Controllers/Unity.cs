using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using Oracle.DataAccess.Client;
using System.IO;
using ICSharpCode.SharpZipLib.GZip;
using System.Xml.Serialization;
namespace WMS.Web.Controllers
{
    public static class Unity
    {

        public static string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["WMS"].ConnectionString;
            }
        }

        public static DbConnection GetConnection()
        {
            var con = new Oracle.DataAccess.Client.OracleConnection(ConnectionString);

            return con;
        }

        private static Stream InputStream(Stream inputStream)
        {
            return new GZipInputStream(inputStream)
            {
                IsStreamOwner = false
            };
        }

        public static MemoryStream DeCompress(Stream Input)
        {
            Input.Seek(0, SeekOrigin.Begin);
            try
            {
                Stream s2 = InputStream(Input);
                MemoryStream outStream = new MemoryStream();

                byte[] writeData = new byte[4096];

                while (true)
                {
                    int size = s2.Read(writeData, 0, writeData.Length);
                    if (size > 0)
                    {
                        outStream.Write(writeData, 0, size);
                    }
                    else
                    {
                        break;
                    }
                }
                s2.Close();
                return outStream;
            }
            catch (GZipException)
            {
                Input.Seek(0, SeekOrigin.Begin);
                if (Input is MemoryStream)
                    return Input as MemoryStream;
                else
                    return null;
            }

        }


        public static T XmlDeserialize<T>(this Stream ms) where T : class
        {
            ms.Seek(0, SeekOrigin.Begin);
            XmlSerializer xml = new XmlSerializer(typeof(T));
            return xml.Deserialize(ms) as T;
        }
    }
}