using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;

namespace HTMLFetchAndParse
{
    public class HTMLFetcher
    {

        //for load page
        public HttpWebRequest myHttpWebRequest;
        public HttpWebResponse myHttpWebResponse;
        public CookieContainer Cookies { set; get; }
        public String UserAgent { set; get; }

        public HTMLFetcher()
        {
            Cookies = new CookieContainer(1000, 100, 4096);
            UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:7.0.1) Gecko/20100101 Firefox/7.0.1";
        }

        public void AddCookie(String cookieString, String domain)
        {
            var cookieCollection = new CookieCollection();
            var cookies = cookieString.Trim().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var cookie in cookies)
            {
                var datas = cookie.Trim().Split(new char[] { '=' }, 2);
                cookieCollection.Add(new Cookie(datas[0], datas[1]));
            }
            Cookies.Add(new Uri(domain), cookieCollection); 
            
        }
        public void LoadPage(string uri, string filename, string encodingName)
        {
            try
            {
                myHttpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(uri));
                myHttpWebRequest.CookieContainer = Cookies;


                myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
                myHttpWebRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*//*;q=0.8";
                myHttpWebRequest.UserAgent = UserAgent;


                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                using (var myResponseStream = myHttpWebResponse.GetResponseStream())
                using (var myResponseReader = new StreamReader(myResponseStream, Encoding.GetEncoding(encodingName)))
                {
                    var myResponseResult = myResponseReader.ReadToEnd();
                    //Console.WriteLine(myResponseResult);
                    StreamWriter sw = new StreamWriter(File.Open(filename, FileMode.Create), Encoding.GetEncoding("utf-8"));
                    sw.Write(myResponseResult);
                    sw.Close();
                }
                myHttpWebResponse.Close();
            }
            catch (Exception e)
            {

                Console.WriteLine("Exception Occurs: " + e.Message);
                Console.WriteLine("Uri : " + uri);

//                Thread.Sleep(1200000);

                myHttpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(uri));
                myHttpWebRequest.CookieContainer = Cookies;
                myHttpWebRequest.UserAgent = UserAgent;

                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                using (var myResponseStream = myHttpWebResponse.GetResponseStream())
                using (var myResponseReader = new StreamReader(myResponseStream, Encoding.GetEncoding(encodingName)))
                {
                    var myResponseResult = myResponseReader.ReadToEnd();
                    //Console.WriteLine(myResponseResult);
                    StreamWriter sw = new StreamWriter(File.Open(filename, FileMode.Create), Encoding.GetEncoding("utf-8"));
                    sw.Write(myResponseResult); 
                    sw.Close();
                }
                myHttpWebResponse.Close();
            }
        }

        public String LoadPage(string uri, string encodingName)
        {
            String myResponseResult = String.Empty;
            try
            {
                myHttpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(uri));
                myHttpWebRequest.CookieContainer = Cookies;


                myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
                myHttpWebRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*//*;q=0.8";
                myHttpWebRequest.UserAgent = UserAgent;


                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                using (var myResponseStream = myHttpWebResponse.GetResponseStream())
                using (var myResponseReader = new StreamReader(myResponseStream, Encoding.GetEncoding(encodingName)))
                {
                    myResponseResult = myResponseReader.ReadToEnd();
                    //Console.WriteLine(myResponseResult);
                }
                myHttpWebResponse.Close();
                return myResponseResult;

            }
            catch (Exception e)
            {

                Console.WriteLine("Exception Occurs: " + e.Message);
                Console.WriteLine("Uri : " + uri);

                //                Thread.Sleep(1200000);

                myHttpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(uri));
                myHttpWebRequest.CookieContainer = Cookies;
                myHttpWebRequest.UserAgent = UserAgent;

                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                using (var myResponseStream = myHttpWebResponse.GetResponseStream())
                using (var myResponseReader = new StreamReader(myResponseStream, Encoding.GetEncoding(encodingName)))
                {
                    myResponseResult = myResponseReader.ReadToEnd();
                    //Console.WriteLine(myResponseResult);
                }
                myHttpWebResponse.Close();
                return myResponseResult;
            }
        }

        public void PostPage(string uri, string data, string filename, string encodingName)
        {
            try
            {
                myHttpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(uri));
                myHttpWebRequest.CookieContainer = Cookies;

                myHttpWebRequest.Method = "POST";
                myHttpWebRequest.KeepAlive = true;
                myHttpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                myHttpWebRequest.Accept = "application/json, text/javascript, */*; q=0.01";
                myHttpWebRequest.UserAgent = UserAgent;
                myHttpWebRequest.ServicePoint.Expect100Continue = false;

                using (var requestStream = myHttpWebRequest.GetRequestStream())
                using (var writer = new StreamWriter(requestStream))
                {
                    writer.Write(data);
                }

                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                using (var myResponseStream = myHttpWebResponse.GetResponseStream())
                using (var myResponseReader = new StreamReader(myResponseStream, Encoding.GetEncoding(encodingName)))
                {
                    var myResponseResult = myResponseReader.ReadToEnd();
                    //Console.WriteLine(myResponseResult);
                    StreamWriter sw = new StreamWriter(File.Open(filename, FileMode.Create), Encoding.GetEncoding("utf-8"));
                    sw.Write(myResponseResult);
                    sw.Close();
                }
                myHttpWebResponse.Close();
            }
            catch (Exception e)
            {

                Console.WriteLine("Exception Occurs: " + e.Message);
                Console.WriteLine("Uri : " + uri);

                myHttpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(uri));
                myHttpWebRequest.CookieContainer = Cookies;
                myHttpWebRequest.UserAgent = UserAgent;

                myHttpWebRequest.Method = "POST";
                myHttpWebRequest.KeepAlive = true;
                myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
                myHttpWebRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*//*;q=0.8";
                myHttpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:7.0.1) Gecko/20100101 Firefox/7.0.1";

                using (var requestStream = myHttpWebRequest.GetRequestStream())
                using (var writer = new StreamWriter(requestStream))
                {
                    writer.Write(data);
                }

                myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                using (var myResponseStream = myHttpWebResponse.GetResponseStream())
                using (var myResponseReader = new StreamReader(myResponseStream, Encoding.GetEncoding(encodingName)))
                {
                    var myResponseResult = myResponseReader.ReadToEnd();
                    //Console.WriteLine(myResponseResult);
                    StreamWriter sw = new StreamWriter(File.Open(filename, FileMode.Create), Encoding.GetEncoding("utf-8"));
                    sw.Write(myResponseResult);
                    sw.Close();
                }
                myHttpWebResponse.Close();
            }
        }

    }
}
