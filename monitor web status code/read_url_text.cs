using System.Collections.Generic;
using System.IO;

namespace monitor_web_status_code
{
    class read_url_text
    {
        public string[] get_url_in_file ()
        {
            string[] url_in_file;
            List<string> url_list = new List<string>();
            //string file_url = System.Environment.CurrentDirectory;
            //string strReadFilePath = file_url + "\\url.txt";
            string strReadFilePath = @"D:\Program Files\zabbix\url.txt";
            FileStream sFile = new FileStream(strReadFilePath, FileMode.Open);
            StreamReader srReadFile = new StreamReader(sFile);
            // 读取流直至文件末尾结束
            while (!srReadFile.EndOfStream)
            {
                string strReadLine = srReadFile.ReadLine(); //读取每行数据
                url_list.Add(strReadLine);
            }

            url_in_file=url_list.ToArray();

            return url_in_file;
        }


    }
}
