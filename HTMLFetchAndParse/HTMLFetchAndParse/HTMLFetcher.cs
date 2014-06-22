using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Net.NetworkInformation;

namespace HTMLFetchAndParse {
    public class HTMLFetcher {

        //for load page
        public HttpWebRequest myHttpWebRequest;
        public HttpWebResponse myHttpWebResponse;
        public CookieContainer Cookies { set; get; }
        public String UserAgent { set; get; }
        public String Referer { set; get; }
        public String Origin { set; get; }
        public String ContentType { set; get; }
        public String AcceptEncoding { set; get; }
        Ping pingSender = new Ping();

        public HTMLFetcher() {
            Cookies = new CookieContainer(1000, 100, 4096);
            UserAgent = @"Mozilla/5.0 (Windows NT 5.1; rv:7.0.1) Gecko/20100101 Firefox/7.0.1";
            AcceptEncoding = @"text/html,application/xhtml+xml,application/xml;q=0.9,*//*;q=0.8";
            ContentType = @"application/x-www-form-urlencoded; charset=utf-8";
        }

        public void AddCookie(string filename) {
            var text = File.ReadAllLines(filename);
            foreach(var line in text) {
                var groups = line.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if(groups.Count() > 1) {
                    AddCookie(groups[1], groups[0]);
                }
            }
        }

        public void AddCookie(String cookieString, String domain) {
            var cookieCollection = new CookieCollection();
            var cookies = cookieString.Trim().Split(new char[] { ';' },
                                                    StringSplitOptions.RemoveEmptyEntries);
            foreach(var cookie in cookies) {
                var datas = cookie.Trim().Split(new char[] { '=' }, 2);
                cookieCollection.Add(new Cookie(datas[0], datas[1], "/", domain));
            }
            Cookies.Add(cookieCollection);

        }

        public void AddCookie(String cookieString, String domain, String path) {
            var cookieCollection = new CookieCollection();
            var cookies = cookieString.Trim().Split(new char[] { ';' },
                                                    StringSplitOptions.RemoveEmptyEntries);
            foreach(var cookie in cookies) {
                var datas = cookie.Trim().Split(new char[] { '=' }, 2);
                cookieCollection.Add(new Cookie(datas[0], datas[1], path, domain));
            }
            Cookies.Add(cookieCollection);

        }
        public void LoadPage(string uri, string filename, string encodingName) {
            try {
                myHttpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(uri));
                myHttpWebRequest.CookieContainer = Cookies;

                myHttpWebRequest.ContentType = ContentType;
                myHttpWebRequest.Accept = AcceptEncoding;
                myHttpWebRequest.UserAgent = UserAgent;
                myHttpWebRequest.AllowAutoRedirect = true;
                myHttpWebRequest.Referer = Referer;

                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                using(var myResponseStream = myHttpWebResponse.GetResponseStream())
                using(var myResponseReader = new StreamReader(myResponseStream,
                    Encoding.GetEncoding(encodingName))) {
                    var myResponseResult = myResponseReader.ReadToEnd();
                    //Console.WriteLine(myResponseResult);
                    StreamWriter sw = new StreamWriter(
                        File.Open(filename, FileMode.Create),
                        Encoding.GetEncoding("utf-8"));
                    sw.Write(myResponseResult);
                    sw.Close();
                }
                myHttpWebResponse.Close();
            } catch(Exception e) {

                Console.WriteLine("Exception Occurs: " + e.Message);

                string datass = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(datass);
                int timeout = 120;
                PingReply reply = pingSender.Send("8.8.8.8", timeout, buffer);
                while(reply.Status != IPStatus.Success) {
                    reply = pingSender.Send("8.8.8.8", timeout, buffer);
                }

                myHttpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(uri));
                myHttpWebRequest.CookieContainer = Cookies;
                myHttpWebRequest.UserAgent = UserAgent;
                myHttpWebRequest.AllowAutoRedirect = true;
                myHttpWebRequest.Referer = Referer;
                myHttpWebRequest.ContentType = ContentType;

                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                using(var myResponseStream = myHttpWebResponse.GetResponseStream())
                using(var myResponseReader =
                    new StreamReader(myResponseStream, Encoding.GetEncoding(encodingName))) {

                    var myResponseResult = myResponseReader.ReadToEnd();
                    //Console.WriteLine(myResponseResult);
                    StreamWriter sw = new StreamWriter(File.Open(filename, FileMode.Create),
                                                       Encoding.GetEncoding("utf-8"));
                    sw.Write(myResponseResult);
                    sw.Close();
                }
                myHttpWebResponse.Close();
            }
        }

        public void LoadImage(string uri, string filename) {
            try {
                myHttpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(uri));
                myHttpWebRequest.CookieContainer = Cookies;

                myHttpWebRequest.ContentType = ContentType;
                myHttpWebRequest.Accept = AcceptEncoding;
                //myHttpWebRequest.UserAgent = UserAgent;
                myHttpWebRequest.AllowAutoRedirect = true;
                myHttpWebRequest.Referer = Referer;

                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                using(var myResponseStream = myHttpWebResponse.GetResponseStream())
                using(var myResponseReader = new BinaryReader(myResponseStream)) {
                    Byte[] lnByte = myResponseReader.ReadBytes(1 * 1024 * 1024 * 10);
                    using(FileStream lxFS = new FileStream(filename, FileMode.Create)) {
                        lxFS.Write(lnByte, 0, lnByte.Length);
                    }
                }
                myHttpWebResponse.Close();
            } catch(Exception e) {

                Console.WriteLine("Exception Occurs: " + e.Message);

                //                Thread.Sleep(1200000);
                string datass = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(datass);
                int timeout = 120;
                PingReply reply = pingSender.Send("8.8.8.8", timeout, buffer);
                while(reply.Status != IPStatus.Success) {
                    reply = pingSender.Send("8.8.8.8", timeout, buffer);
                }

                myHttpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(uri));
                myHttpWebRequest.CookieContainer = Cookies;
                // myHttpWebRequest.UserAgent = UserAgent;
                myHttpWebRequest.AllowAutoRedirect = true;
                myHttpWebRequest.Referer = Referer;

                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                using(var myResponseStream = myHttpWebResponse.GetResponseStream())
                using(var myResponseReader = new BinaryReader(myResponseStream)) {
                    Byte[] lnByte = myResponseReader.ReadBytes(1 * 1024 * 1024 * 10);
                    using(FileStream lxFS = new FileStream(filename, FileMode.Create)) {
                        lxFS.Write(lnByte, 0, lnByte.Length);
                    }
                }
                myHttpWebResponse.Close();
            }
        }


        public String LoadPage(string uri, string encodingName) {
            String myResponseResult = String.Empty;
            try {
                myHttpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(uri));
                myHttpWebRequest.CookieContainer = Cookies;
                myHttpWebRequest.AllowAutoRedirect = true;

                myHttpWebRequest.ContentType = ContentType;
                myHttpWebRequest.Accept = AcceptEncoding;
                myHttpWebRequest.UserAgent = UserAgent;
                myHttpWebRequest.Referer = Referer;

                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                //if (myHttpWebResponse.StatusCode == HttpStatusCode.Redirect ||
                //    myHttpWebResponse.StatusCode == HttpStatusCode.MovedPermanently)
                //{

                //}

                using(var myResponseStream = myHttpWebResponse.GetResponseStream())
                using(var myResponseReader = new StreamReader(myResponseStream,
                    Encoding.GetEncoding(encodingName))) {
                    myResponseResult = myResponseReader.ReadToEnd();
                    //Console.WriteLine(myResponseResult);
                }
                myHttpWebResponse.Close();
                return myResponseResult;

            } catch(Exception e) {

                Console.WriteLine("Exception Occurs: " + e.Message);
                // Console.WriteLine("Uri : " + uri);

                // Use the default Ttl value which is 128, 
                // but change the fragmentation behavior.

                // Create a buffer of 32 bytes of data to be transmitted. 
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;
                PingReply reply = pingSender.Send("8.8.8.8", timeout, buffer);
                while(reply.Status != IPStatus.Success) {
                    reply = pingSender.Send("8.8.8.8", timeout, buffer);
                }


                myHttpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(uri));
                myHttpWebRequest.CookieContainer = Cookies;
                myHttpWebRequest.UserAgent = UserAgent;
                myHttpWebRequest.Referer = Referer;
                myHttpWebRequest.AllowAutoRedirect = true;

                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                using(var myResponseStream = myHttpWebResponse.GetResponseStream())
                using(var myResponseReader =
                    new StreamReader(myResponseStream, Encoding.GetEncoding(encodingName))) {
                    myResponseResult = myResponseReader.ReadToEnd();
                    //Console.WriteLine(myResponseResult);
                }
                myHttpWebResponse.Close();
                return myResponseResult;
            }
        }


        public void PostPage(string uri, string data, string filename, string encodingName) {
            try {
                myHttpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(uri));
                myHttpWebRequest.CookieContainer = Cookies;

                myHttpWebRequest.Method = "POST";
                myHttpWebRequest.KeepAlive = true;
                myHttpWebRequest.ContentType = ContentType;
                myHttpWebRequest.Accept = AcceptEncoding;
                myHttpWebRequest.UserAgent = UserAgent;
                //     myHttpWebRequest.Referer = Referer;
                myHttpWebRequest.ServicePoint.Expect100Continue = false;
                myHttpWebRequest.AllowAutoRedirect = true;

                using(var requestStream = myHttpWebRequest.GetRequestStream())
                using(var writer = new StreamWriter(requestStream)) {
                    writer.Write(data);
                }

                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                using(var myResponseStream = myHttpWebResponse.GetResponseStream())
                using(var myResponseReader =
                    new StreamReader(myResponseStream, Encoding.GetEncoding(encodingName))) {

                    var myResponseResult = myResponseReader.ReadToEnd();
                    //Console.WriteLine(myResponseResult);
                    StreamWriter sw = new StreamWriter(File.Open(filename, FileMode.Create),
                                                       Encoding.GetEncoding("utf-8"));
                    sw.Write(myResponseResult);
                    sw.Close();
                }
                myHttpWebResponse.Close();
            } catch(Exception e) {

                Console.WriteLine("Exception Occurs: " + e.Message);
                // Console.WriteLine("Uri : " + uri);
                string datass = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(datass);
                int timeout = 120;
                PingReply reply = pingSender.Send("8.8.8.8", timeout, buffer);
                while(reply.Status != IPStatus.Success) {
                    reply = pingSender.Send("8.8.8.8", timeout, buffer);
                }

                myHttpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(uri));
                myHttpWebRequest.CookieContainer = Cookies;
                myHttpWebRequest.UserAgent = UserAgent;
                myHttpWebRequest.AllowAutoRedirect = true;

                myHttpWebRequest.Method = "POST";
                myHttpWebRequest.KeepAlive = true;
                myHttpWebRequest.ContentType = ContentType;
                myHttpWebRequest.Accept = AcceptEncoding;
                myHttpWebRequest.UserAgent = UserAgent;
                myHttpWebRequest.ServicePoint.Expect100Continue = false;

                using(var requestStream = myHttpWebRequest.GetRequestStream())
                using(var writer = new StreamWriter(requestStream)) {
                    writer.Write(data);
                }

                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                using(var myResponseStream = myHttpWebResponse.GetResponseStream())
                using(var myResponseReader =
                    new StreamReader(myResponseStream, Encoding.GetEncoding(encodingName))) {

                    var myResponseResult = myResponseReader.ReadToEnd();
                    //Console.WriteLine(myResponseResult);
                    StreamWriter sw = new StreamWriter(File.Open(filename, FileMode.Create),
                                                       Encoding.GetEncoding("utf-8"));
                    sw.Write(myResponseResult);
                    sw.Close();
                }
                myHttpWebResponse.Close();
            }
        }

        public String PostPage(string uri, string data, string encodingName) {
            try {
                myHttpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(uri));
                myHttpWebRequest.CookieContainer = Cookies;

                myHttpWebRequest.Method = "POST";
                myHttpWebRequest.KeepAlive = true;
                myHttpWebRequest.ContentType = ContentType;
                myHttpWebRequest.Accept = AcceptEncoding;
                myHttpWebRequest.UserAgent = UserAgent;
                //  myHttpWebRequest.Referer = Referer;
                myHttpWebRequest.ServicePoint.Expect100Continue = false;
                myHttpWebRequest.AllowAutoRedirect = true;

                using(var requestStream = myHttpWebRequest.GetRequestStream())
                using(var writer = new StreamWriter(requestStream)) {
                    writer.Write(data);
                }

                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                using(var myResponseStream = myHttpWebResponse.GetResponseStream())
                using(var myResponseReader =
                    new StreamReader(myResponseStream, Encoding.GetEncoding(encodingName))) {
                    return myResponseReader.ReadToEnd();

                }
            } catch(Exception e) {

                Console.WriteLine("Exception Occurs: " + e.Message);
                //   Console.WriteLine("Uri : " + uri);
                string datass = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(datass);
                int timeout = 120;
                PingReply reply = pingSender.Send("8.8.8.8", timeout, buffer);
                while(reply.Status != IPStatus.Success) {
                    reply = pingSender.Send("8.8.8.8", timeout, buffer);
                }

                myHttpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(uri));
                myHttpWebRequest.CookieContainer = Cookies;
                myHttpWebRequest.UserAgent = UserAgent;
                myHttpWebRequest.AllowAutoRedirect = true;

                myHttpWebRequest.Method = "POST";
                myHttpWebRequest.KeepAlive = true;
                myHttpWebRequest.ContentType = ContentType;
                myHttpWebRequest.Accept = AcceptEncoding;
                myHttpWebRequest.UserAgent = UserAgent;
                myHttpWebRequest.ServicePoint.Expect100Continue = false;

                using(var requestStream = myHttpWebRequest.GetRequestStream())
                using(var writer = new StreamWriter(requestStream)) {
                    writer.Write(data);
                }

                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                using(var myResponseStream = myHttpWebResponse.GetResponseStream())
                using(var myResponseReader =
                    new StreamReader(myResponseStream, Encoding.GetEncoding(encodingName))) {
                    return myResponseReader.ReadToEnd();

                }
            }
        }

    }
}
