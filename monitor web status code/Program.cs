using System;


namespace monitor_web_status_code
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] parameters = new string[2];
            if (args.Length != 0)
            {
                parameters = args;

                if (parameters.Length != 0)
                {
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
                            if(aaa.Length==a)
                            {
                                Console.Write("]}");
                            }
                            else
                            {
                                Console.Write(",\n");
                            }
                        }
                    }

                    //网站状态码
                    if (parameters[0] == "web.code" && parameters[1] != null)
                    {
                        try
                        {
                            if (parameters[1].StartsWith("http://") == false)
                            {
                                parameters[1] = "http://" + parameters[1];
                            }


                            web_site_code wsc = new web_site_code();
                            int code = wsc.Get_code(parameters[1]);
                            Console.Write(code);
                        }
                        catch
                        {

                            Console.WriteLine("Parameters error!");
                            Console.WriteLine("URL typing errors");
                        }  
                    }
                }
            }
            else
            {
                Console.WriteLine("\t\t Parameters must input!");
                Console.WriteLine("\t\t mwsc  web.code | web_site_discovery");
                Console.WriteLine("\t\t web.code  \"web.code www.baidu.com\"");
                Console.WriteLine("\t\t web.dis");
            }


        }



    }

}

