using System;
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
                return (int)hwrs.StatusCode;
            }
            catch (WebException ex)
            {
                // Console.WriteLine(ex.Status);
                var ddd = Convert.ToInt32(ex.Status);

                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    if (response != null)
                    {
                        return (int)response.StatusCode;
                    }
                }
                return 1;
            }

        }
    }
}
