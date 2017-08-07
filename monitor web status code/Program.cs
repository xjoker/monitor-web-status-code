using System;
using System.DirectoryServices;
using System.IO;

namespace monitor_web_status_code
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var urlSavePath = Path.Combine(Environment.CurrentDirectory, "url.txt");


                if (args.Length != 0)
                {
                    var parameters = args;
                    
                    if (parameters.Length != 0)
                    {
                        #region web.dis功能
                        //参数为“web_site_discovery”时
                        if (parameters[0] == "website.discovery")
                        {
                            read_url_text rut = new read_url_text();
                            string[] aaa = rut.get_url_in_file(urlSavePath);
                            Console.WriteLine("{");
                            Console.WriteLine("\t\"data\":[");
                            int a = 0;
                            foreach (var item in aaa)
                            {

                                Console.WriteLine("\t\t{");
                                Console.Write("\t\t\t\"{#SITENAME}\":\"" + item + "\"}");
                                a++;
                                if (aaa.Length == a)
                                {
                                    Console.Write("]}");
                                }
                                else
                                {
                                    Console.Write(",\n");
                                }
                            }
                        }
                        #endregion

                        #region 网站状态码
                        
                        int code;
                        if (parameters[0] == "website.code" && parameters[1] != null)
                        {

                            if (parameters.Length>2)
                            {
                                
                                try
                                {
                                    web_site_code wsc = new web_site_code();
                                    code = wsc.Get_code(parameters[1], parameters[2]);
                                    Console.Write(code);
                                }
                                catch
                                {
                                    Console.Write("404");
                                }
                            }
                            else
                            {
                               
                                try
                                {

                                    web_site_code wsc = new web_site_code();

                                    if (parameters[1].Substring(0, 1) == "$")
                                    {
                                        //遇到带 "$" 的处理方法
                                        //Console.WriteLine("Test info: " + parameters[1].ToString());

                                        code = wsc.Get_code(parameters[1].Substring(1, parameters[1].Length - 1));
                                    }
                                    else
                                    {
                                        //一般模式检测IP地址127.0.0.1
                                        Console.WriteLine("Test info: " + parameters[1].ToString() + "/zabbix/zabbix.aspx");
                                        code = wsc.Get_code(parameters[1].ToString() + "/zabbix/zabbix.aspx", "127.0.0.1");
                                    }


                                    Console.Write(code);
                                }
                                catch //(Exception ex)
                                {
                                    //Console.WriteLine(ex);
                                    Console.WriteLine(1);
                                }
                            }
                        }
                        #endregion

                        #region 获得IIS站点并写入文件
                        if (parameters[0] == "website.get")
                        {
                            DirectoryEntry w3svc = new DirectoryEntry("IIS://localhost/w3svc");
                            if (w3svc.Children != null)
                            {
                                if (File.Exists(urlSavePath))
                                {
                                    File.Delete(urlSavePath);
                                }
                                foreach (DirectoryEntry de in w3svc.Children)
                                {
                                    if (de.Properties.Contains("ServerComment"))
                                    {
                                        var siteName = de.Properties["ServerComment"][0].ToString();
                                        if (!string.IsNullOrWhiteSpace(siteName))
                                        {
                                            Taxi.FileHelper.FileHelper.WriteFile(urlSavePath, $"{siteName}\r\n", true);
                                        }
                                    }
                                }
                            }

                        }

                        #endregion
                    }
                }
                else 
                {
                    Console.WriteLine("\t\t Parameters must input!");
                    Console.WriteLine("\t\t mwsc  website.code | website.discovery | website.Get");
                    Console.WriteLine("\t\t website.code  \"website.code www.baidu.com\"");
                    Console.WriteLine("\t\t website.discovery");
                    Console.WriteLine("\t\t website.Get");
                    Console.WriteLine("\t\t Return code list: 404 200 500 408");
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                Console.WriteLine("Parameters error!");
            }


        }



    }

}

