using System.Net;

namespace monitor_web_status_code
{
    class web_site_code
    {
        public int Get_code (string url)
        {
            HttpWebRequest hwr = (HttpWebRequest) WebRequest.Create(url);

            hwr.Method = "GET"; 

            HttpWebResponse  hwrs = (HttpWebResponse) hwr.GetResponse();

            int http_code = (int)hwrs.StatusCode;//获得状态编码

            return http_code;
        }
    }
}
