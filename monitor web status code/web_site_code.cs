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

                url = "http://" + url;
                Uri u = new Uri(url);
                url = u.Host;
                //判断是否传入了IP
                if (ip != "NOIP")
                {
                    //使用IP直接访问站点
                    test_ip = "http://" + ip;

                }
                else
                {
                    test_ip = u.AbsoluteUri;
                    
                }
                HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(test_ip);
                
                hwr.KeepAlive = false;
                
                hwr.Host = url;

                hwr.Method = "GET";

                hwr.Timeout = 2000; //1秒超时

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
                        case WebExceptionStatus.ProtocolError:
                            return 404;
                        default:
                            return 1;
                    }


                    if(ex.Status==WebExceptionStatus.ProtocolError)
                    {
                        HttpWebResponse errors = (HttpWebResponse)ex.Response;
                        switch (errors.StatusCode)
                        {
                           
                            case HttpStatusCode.InternalServerError:
                                return 500;
                              
                            case HttpStatusCode.NotFound:
                                return 404;
                              
                            case HttpStatusCode.OK:
                                return 200;

                            case HttpStatusCode.RequestTimeout:
                                return 408;
                            default:
                                return 1;
                        }
                    }
                    
                }
                
        }
    }
}
