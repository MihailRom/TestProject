namespace TaskListSimulator.ConsoleApp.Components
{
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    public static class Web
    {
        public static string Get(string uri)
        {
            string responseFromServer = string.Empty;
            WebRequest request = WebRequest.Create(uri);
            request.Method = "GET";
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = null;
            try
            {
                response = request.GetResponse();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
            }
            response.Close();
            return responseFromServer;
        }

        public static string Post(string uri, string body)
        {
            string responseFromServer = string.Empty;
            WebRequest request = WebRequest.Create(uri);
            request.Method = "POST";
            request.Credentials = CredentialCache.DefaultCredentials;

            byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body));
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            var rs = request.GetRequestStream();
            rs.Write(data, 0, data.Length);
            rs.Close();

            WebResponse response = null;
            try
            {
                response = request.GetResponse();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
            }
            response.Close();
            return responseFromServer;
        }
    }
}

