# monitor-web-status-code
程序会自动读取同目录的url.txt
文件内容格式必须是一行一个网址，无需在前面加上http://

参数

	MWSC.exe website.discovery  
	输出 Zabbix 可接收的 JSON 格式
	
	MWSC.exe website.code <siteName>
	第二个参数跟着网址，无需加上http:// 可测试本机指定站点名称的连接情况
	
	MWSC.exe website.get 
	获取本机所有的IIS站点名称，存储至程序目录下的url.txt
	这个命令只适合IIS站点名为站点域名的情况下使用
	
