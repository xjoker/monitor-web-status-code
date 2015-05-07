using System;


namespace monitor_web_status_code
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {


                string[] parameters = new string[3];
                if (args.Length != 0)
                {
                    parameters = args;

                    if (parameters.Length != 0)
                    {
                        #region web.dis功能
                        //参数为“web_site_discovery”时
                        if (parameters[0] == "web.dis")
                        {
                            read_url_text rut = new read_url_text();
                            string[] aaa = rut.get_url_in_file();
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
                        if (parameters[0] == "web.code" && parameters[1] != null)
                        {

                            if (parameters.Length > 2)
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
                                        code = wsc.Get_code(parameters[1].Substring(1, parameters[1].Length - 1));
                                    }
                                    else
                                    {
                                        //一般模式检测IP地址127.0.0.1
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
                    }
                }
                else
                {
                    Console.WriteLine("\t\t Parameters must input!");
                    Console.WriteLine("\t\t mwsc  web.code | web.dis");
                    Console.WriteLine("\t\t web.code  \"web.code www.baidu.com\"");
                    Console.WriteLine("\t\t web.dis");
                    Console.WriteLine("\t\t Return code list: 404 200 500 408");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Parameters error!");
            }


        }



    }

}

