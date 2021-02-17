using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ProfileBook.Service
{
    public class BaseService
    {
        public string EndPoint { get; protected set; }
        public NameValueCollection CustomHeaders { get; set; }
        protected static readonly IDictionary<string, Type> operationReturnTypeMap = new Dictionary<string, Type>();
        public const string APPKEY_HEADER = "X-Application";
        public const string SESSION_TOKEN_HEADER = "X-Authentication";

        protected HttpWebRequest CreateWebRequest(String restEndPoint)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(restEndPoint);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = 0;
            request.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");
            request.Accept = "application/json";
            request.Headers.Add(CustomHeaders);
            ServicePointManager.Expect100Continue = false;
            return request;
        }

        public async Task<T> InvokeAsync<T>(string method, IDictionary<string, object> args = null)
        {
            if (method == null)
                throw new ArgumentNullException("method");
            if (method.Length == 0)
                throw new ArgumentException(null, "method");


            var restEndpoint = EndPoint + method + "/";
            var request = CreateWebRequest(restEndpoint);

            var postData = JsonConvert.Serialize<IDictionary<string, object>>(args) + "}";
#if DEBUG

            Debug.WriteLine("BEGIN >>>>>>");
            Debug.WriteLine("Request Method:");
            Debug.WriteLine(restEndpoint);
            Debug.WriteLine("Request Headers:");
            string header = "[{";
            foreach (var h in request.Headers.AllKeys)
            {
                if (!string.IsNullOrEmpty(header))
                    header += ",";
                header += "\"" + h.ToString() + "\":\"" + request.Headers[h] + "\"";
            }
            header += "}]";
            Debug.WriteLine(header);
            Debug.WriteLine("Request PostData:");
            Debug.WriteLine(postData);
#endif

            var bytes = Encoding.GetEncoding("UTF-8").GetBytes(postData);
            request.ContentLength = bytes.Length;

            using (Stream stream = await request.GetRequestStreamAsync())
            {
                stream.Write(bytes, 0, bytes.Length);
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {

                        var jsonResponse = await reader.ReadToEndAsync();
#if DEBUG
                        Debug.WriteLine("Response:");
                        Debug.WriteLine(jsonResponse);
                        Debug.WriteLine("END >>>>>>");
#endif

                        if (response.StatusCode != HttpStatusCode.OK)
                        {
                            //throw ReconstituteException(JsonConvert.Deserialize<Exception>(jsonResponse));
                        }

                        return JsonConvert.Deserialize<T>(jsonResponse);
                    }
                }
            }
        }
    }
}
