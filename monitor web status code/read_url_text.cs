using System.Collections.Generic;
using System.IO;
using Taxi.StringHelper;

namespace monitor_web_status_code
{
    class read_url_text
    {
        public string[] get_url_in_file (string filePath)
        {
            List<string> url_list = new List<string>();
            FileStream sFile = new FileStream(filePath, FileMode.Open);
            StreamReader srReadFile = new StreamReader(sFile);
            // 读取流直至文件末尾结束
            while (!srReadFile.EndOfStream)
            {
                string strReadLine = srReadFile.ReadLine(); //读取每行数据
                if (!strReadLine.IsNullOrWhiteSpace())
                {
                    url_list.Add(strReadLine);
                }
            }


            return url_list.ToArray();
        }


    }
}
