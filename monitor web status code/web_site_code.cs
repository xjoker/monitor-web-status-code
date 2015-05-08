using System;
using System.IO;
using System.Net;

namespace monitor_web_status_code
{
    class web_site_code
    {
        public int Get_code(string url, string ip = "NOIP")
        {

            string test_ip = string.Empty; //要测试的IP
           // Console.WriteLine(url.Substring(0, 7));
            if(url.Substring(0,7)!="http://")
            {
                url = "http://" + url;
            }
            //Console.WriteLine(url);
            Uri u = new Uri(url);
            url = u.Host;
            //判断是否传入了IP
            if (ip != "NOIP")
            {
                //使用IP直接访问站点
                test_ip = "http://" + ip+u.AbsolutePath;

            }
            else
            {
                test_ip = u.AbsoluteUri;

            }
            //Console.WriteLine(test_ip);
            HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(test_ip);

            hwr.KeepAlive = false;

            hwr.Host = url;

            hwr.Method = "GET";

            hwr.Timeout = 10000; //1秒超时

            try
            {
                //网页正常访问
                HttpWebResponse hwrs = (HttpWebResponse)hwr.GetResponse();

                return 200;
            }
            catch (WebException ex)
            {
                // Console.WriteLine(ex.Status);
                switch (ex.Status)
                {
                    case WebExceptionStatus.Success:
                        return 200;

                    case WebExceptionStatus.Timeout:
                        return 408;

                    case WebExceptionStatus.ConnectFailure:
                        return 404;

                    case WebExceptionStatus.ProtocolError:
                        HttpWebResponse errors = (HttpWebResponse)ex.Response;
                        switch (errors.StatusCode)
                        {
                            case HttpStatusCode.Forbidden:
                                return 403;

                            case HttpStatusCode.InternalServerError:
                                return 500;

                            case HttpStatusCode.NotFound:
                                return 404;

                            case HttpStatusCode.OK:
                                return 200;

                            case HttpStatusCode.RequestTimeout:
                                return 408;
                            default:
                                Console.WriteLine(errors.StatusCode);
                                return 1;
                        }
                    default:
                        //Console.WriteLine(ex.Status);
                        return 1;
                }




            }

        }
    }
}
