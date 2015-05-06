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

            hwr.Host = url;

            hwr.Method = "GET";

            int http_code;

            HttpWebResponse hwrs = (HttpWebResponse)hwr.GetResponse();

            http_code = (int)hwrs.StatusCode;//获得状态编码
            return http_code;







        }
    }
}
