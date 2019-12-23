using System;
using System.IO;
using System.Net;
using System.Security.Authentication;

/*
 * Name: GetExternalIp
 * Description: Gets the Grunt's External IP Address
 * Help: Fetches the host's IP address by making a web request to ifconfig.co
 * Reference Assemblies: mscorelib.dll, System.Security.dll, System.dll, System.Core.dll
 */
namespace GetExternalIp
{
    public static class Task
    {
        static void Main(string[] args)
        {
            string result = Execute();
            Console.WriteLine(result);
            Console.ReadKey();
        }

        public static string Execute()
        {
            try
            {
                const string url = "https://ifconfig.co/ip";
                const SslProtocols _Tls12 = (SslProtocols)0x00000C00;
                const SecurityProtocolType Tls12 = (SecurityProtocolType)_Tls12;

                string ipAddress = "";

                ServicePointManager.SecurityProtocol = Tls12;
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    ipAddress = reader.ReadToEnd();
                }

                response.Close();

                return ipAddress.Replace("\n", "");
            }
            catch (Exception e) { return e.GetType().FullName + ": " + e.Message + Environment.NewLine + e.StackTrace; }
        }
    }
}