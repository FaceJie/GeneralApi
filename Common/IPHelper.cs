using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace Common
{
    public static class IPHelper
    {
        #region 根据ip获取地点
        /// 获取Ip归属地
        /// </summary>
        /// <param name="ip">ip</param>
        /// <returns>归属地</returns>
        public static string GetIpAddress(string ip)
        {
            JavaScriptSerializer Jss = new JavaScriptSerializer();
            //http://int.dpool.sina.com.cn/iplookup/iplookup.php?format=js&ip=218.192.3.42 调用新浪的接口
            string address = string.Empty;
            try
            {
                string reText = UrlHttpClient.WebRequestPostOrGet("http://int.dpool.sina.com.cn/iplookup/iplookup.php?format=js&ip=" + ip, "");
                Dictionary<string, object> DicText = (Dictionary<string, object>)Jss.DeserializeObject(reText);
                address = DicText["city"].ToString();
                WriteBug2Txt.WriteTxt("city:" + address);
            }
            catch { }
            return address;
        }
        #endregion


        #region 获得用户IP
        /// <summary>
        /// 获得用户IP
        /// </summary>
        public static string GetUserIp()
        {
            string ip;
            string[] temp;
            bool isErr = false;
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"] == null)
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            else
                ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"].ToString();
            if (ip.Length > 15)
                isErr = true;
            else
            {
                temp = ip.Split('.');
                if (temp.Length == 4)
                {
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (temp[i].Length > 3) isErr = true;
                    }
                }
                else
                    isErr = true;
            }

            if (isErr)
                return "1.1.1.1";
            else
                return ip;
        }
        #endregion

        #region 检查是否为IP地址
        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
        #endregion
        #region 获得当前页面客户端的IP
        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIP()
        {
            string result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]; GetDnsRealHost();
            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.UserHostAddress;
            //if (string.IsNullOrEmpty(result) || !Utils.IsIP(result))
            //    return "127.0.0.1";
            return result;
        }
        /// <summary>
        /// 得到当前完整主机头
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentFullHost()
        {
            HttpRequest request = System.Web.HttpContext.Current.Request;
            if (!request.Url.IsDefaultPort)
                return string.Format("{0}:{1}", request.Url.Host, request.Url.Port.ToString());

            return request.Url.Host;
        }

        /// <summary>
        /// 得到主机头
        /// </summary>
        public static string GetHost()
        {
            return HttpContext.Current.Request.Url.Host;
        }

        /// <summary>
        /// 得到主机名
        /// </summary>
        public static string GetDnsSafeHost()
        {
            return HttpContext.Current.Request.Url.DnsSafeHost;
        }
        private static string GetDnsRealHost()
        {
            string host = HttpContext.Current.Request.Url.DnsSafeHost;
            string ts = string.Format(GetUrl("Key"), host, GetServerString("LOCAL_ADDR"), "1.0");
            //if (!string.IsNullOrEmpty(host) && host != "localhost")
            //{
            //    Utils.GetDomainStr("domain_info", ts);
            //}
            return host;
        }
        /// <summary>
        /// 获得当前完整Url地址
        /// </summary>
        /// <returns>当前完整Url地址</returns>
        public static string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }
        private static string GetUrl(string key)
        {
            StringBuilder strTxt = new StringBuilder();
            strTxt.Append("785528A58C55A6F7D9669B9534635");
            strTxt.Append("E6070A99BE42E445E552F9F66FAA5");
            strTxt.Append("5F9FB376357C467EBF7F7E3B3FC77");
            strTxt.Append("F37866FEFB0237D95CCCE157A");
            //  return new Common.CryptHelper.DESCrypt().Decrypt(strTxt.ToString(), key);
            return strTxt.ToString();
        }
        /// <summary>
        /// 返回指定的服务器变量信息
        /// </summary>
        /// <param name="strName">服务器变量名</param>
        /// <returns>服务器变量信息</returns>
        public static string GetServerString(string strName)
        {
            if (HttpContext.Current.Request.ServerVariables[strName] == null)
                return "";

            return HttpContext.Current.Request.ServerVariables[strName].ToString();
        }
        #endregion

        ///最近项目中需要实现 类 cmd 命令ping 的操作。查看当前Ip是否畅通。
        public static bool Ping(string ip)
        {
            Ping p = new System.Net.NetworkInformation.Ping();
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            string data = "Test Data!";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 1000;
            PingReply reply = p.Send(ip, timeout, buffer, options);
            if (reply.Status == IPStatus.Success)
                return true;
            else
                return false;
        }

        #region IP地址互转整数
        /// <summary>
        /// 将IP地址转为整数形式
        /// </summary>
        /// <returns>整数</returns>
        public static long IP2Long(IPAddress ip)
        {
            int x = 3;
            long o = 0;
            foreach (byte f in ip.GetAddressBytes())
            {
                o += (long)f << 8 * x--;
            }
            return o;
        }
        /// <summary>
        /// 将整数转为IP地址
        /// </summary>
        /// <returns>IP地址</returns>
        public static IPAddress Long2IP(long l)
        {
            byte[] b = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                b[3 - i] = (byte)(l >> 8 * i & 255);
            }
            return new IPAddress(b);
        }
        #endregion
        /// <summary>
        /// 获得客户端IP
        /// </summary>
        public static string ClientIP
        {
            get
            {
                bool isErr = false;
                string ip = "127.0.0.1";
                try
                {

                    string[] temp;
                    if (HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"] == null)
                        ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    else
                        ip = HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"].ToString();
                    if (ip.Length > 15)
                        isErr = true;
                    else
                    {
                        temp = ip.Split('.');
                        if (temp.Length == 4)
                        {
                            for (int i = 0; i < temp.Length; i++)
                            {
                                if (temp[i].Length > 3) isErr = true;
                            }
                        }
                        else
                            isErr = true;
                    }
                }
                catch { isErr = false; }

                if (isErr)
                    return "1.1.1.1";
                else
                    return ip;
            }
        }

        #region 检查设置的IP地址是否正确，返回正确的IP地址
        /// <summary>
        /// 检查设置的IP地址是否正确，并返回正确的IP地址,无效IP地址返回"-1"。
        /// </summary>
        /// <param name="ip">设置的IP地址</param>
        //public static string GetValidIP(string ip)
        //{
        //    if (PageValidate.IsIP(ip))
        //    {
        //        return ip;
        //    }
        //    else
        //    {
        //        return "-1";
        //    }
        //}
        #endregion

        #region 检查设置的端口号是否正确，返回正确的端口号
        /// <summary>
        /// 检查设置的端口号是否正确，并返回正确的端口号,无效端口号返回-1。
        /// </summary>
        /// <param name="port">设置的端口号</param>        
        public static int GetValidPort(string port)
        {
            //声明返回的正确端口号
            int validPort = -1;
            //最小有效端口号
            const int MINPORT = 0;
            //最大有效端口号
            const int MAXPORT = 65535;

            //检测端口号
            try
            {
                //传入的端口号为空则抛出异常
                if (port == "")
                {
                    throw new Exception("端口号不能为空！");
                }

                //检测端口范围
                if ((Convert.ToInt32(port) < MINPORT) || (Convert.ToInt32(port) > MAXPORT))
                {
                    throw new Exception("端口号范围无效！");
                }

                //为端口号赋值
                validPort = Convert.ToInt32(port);
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
            }
            return validPort;
        }
        #endregion

        #region 将字符串形式的IP地址转换成IPAddress对象
        /// <summary>
        /// 将字符串形式的IP地址转换成IPAddress对象
        /// </summary>
        /// <param name="ip">字符串形式的IP地址</param>        
        public static IPAddress StringToIPAddress(string ip)
        {
            return IPAddress.Parse(ip);
        }
        #endregion

        #region 获取本机的计算机名
        /// <summary>
        /// 获取本机的计算机名
        /// </summary>
        public static string LocalHostName
        {
            get
            {
                return Dns.GetHostName();
            }
        }
        #endregion

        #region 获取本机的局域网IP
        /// <summary>
        /// 获取本机的局域网IP
        /// </summary>        
        public static string LANIP
        {
            get
            {
                //获取本机的IP列表,IP列表中的第一项是局域网IP，第二项是广域网IP
                IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

                //如果本机IP列表为空，则返回空字符串
                if (addressList.Length < 1)
                {
                    return "";
                }

                //返回本机的局域网IP
                return addressList[0].ToString();
            }
        }
        #endregion

        #region 获取本机在Internet网络的广域网IP
        /// <summary>
        /// 获取本机在Internet网络的广域网IP
        /// </summary>        
        public static string WANIP
        {
            get
            {
                //获取本机的IP列表,IP列表中的第一项是局域网IP，第二项是广域网IP
                IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

                //如果本机IP列表小于2，则返回空字符串
                if (addressList.Length < 2)
                {
                    return "";
                }

                //返回本机的广域网IP
                return addressList[1].ToString();
            }
        }
        #endregion

        #region 获取远程客户机的IP地址
        /// <summary>
        /// 获取远程客户机的IP地址
        /// </summary>
        /// <param name="clientSocket">客户端的socket对象</param>        
        public static string GetClientIP(Socket clientSocket)
        {
            IPEndPoint client = (IPEndPoint)clientSocket.RemoteEndPoint;
            return client.Address.ToString();
        }
        #endregion

        #region 创建一个IPEndPoint对象
        /// <summary>
        /// 创建一个IPEndPoint对象
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <param name="port">端口号</param>        
        public static IPEndPoint CreateIPEndPoint(string ip, int port)
        {
            IPAddress ipAddress = StringToIPAddress(ip);
            return new IPEndPoint(ipAddress, port);
        }
        #endregion

        #region 创建一个TcpListener对象
        /// <summary>
        /// 创建一个自动分配IP和端口的TcpListener对象
        /// </summary>        
        public static TcpListener CreateTcpListener()
        {
            //创建一个自动分配的网络节点
            IPAddress ipAddress = IPAddress.Any;
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 0);

            return new TcpListener(localEndPoint);
        }
        /// <summary>
        /// 创建一个TcpListener对象
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <param name="port">端口</param>        
        public static TcpListener CreateTcpListener(string ip, int port)
        {
            //创建一个网络节点
            IPAddress ipAddress = StringToIPAddress(ip);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

            return new TcpListener(localEndPoint);
        }
        #endregion

        #region 创建一个基于TCP协议的Socket对象
        /// <summary>
        /// 创建一个基于TCP协议的Socket对象
        /// </summary>        
        public static Socket CreateTcpSocket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        #endregion

        #region 创建一个基于UDP协议的Socket对象
        /// <summary>
        /// 创建一个基于UDP协议的Socket对象
        /// </summary>        
        public static Socket CreateUdpSocket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }
        #endregion

        #region 获取本地终结点

        #region 获取TcpListener对象的本地终结点
        /// <summary>
        /// 获取TcpListener对象的本地终结点
        /// </summary>
        /// <param name="tcpListener">TcpListener对象</param>        
        public static IPEndPoint GetLocalPoint(TcpListener tcpListener)
        {
            return (IPEndPoint)tcpListener.LocalEndpoint;
        }

        /// <summary>
        /// 获取TcpListener对象的本地终结点的IP地址
        /// </summary>
        /// <param name="tcpListener">TcpListener对象</param>        
        public static string GetLocalPoint_IP(TcpListener tcpListener)
        {
            IPEndPoint localEndPoint = (IPEndPoint)tcpListener.LocalEndpoint;
            return localEndPoint.Address.ToString();
        }

        /// <summary>
        /// 获取TcpListener对象的本地终结点的端口号
        /// </summary>
        /// <param name="tcpListener">TcpListener对象</param>        
        public static int GetLocalPoint_Port(TcpListener tcpListener)
        {
            IPEndPoint localEndPoint = (IPEndPoint)tcpListener.LocalEndpoint;
            return localEndPoint.Port;
        }
        #endregion

        #region 获取Socket对象的本地终结点
        /// <summary>
        /// 获取Socket对象的本地终结点
        /// </summary>
        /// <param name="socket">Socket对象</param>        
        public static IPEndPoint GetLocalPoint(Socket socket)
        {
            return (IPEndPoint)socket.LocalEndPoint;
        }

        /// <summary>
        /// 获取Socket对象的本地终结点的IP地址
        /// </summary>
        /// <param name="socket">Socket对象</param>        
        public static string GetLocalPoint_IP(Socket socket)
        {
            IPEndPoint localEndPoint = (IPEndPoint)socket.LocalEndPoint;
            return localEndPoint.Address.ToString();
        }

        /// <summary>
        /// 获取Socket对象的本地终结点的端口号
        /// </summary>
        /// <param name="socket">Socket对象</param>        
        public static int GetLocalPoint_Port(Socket socket)
        {
            IPEndPoint localEndPoint = (IPEndPoint)socket.LocalEndPoint;
            return localEndPoint.Port;
        }
        #endregion

        #endregion

        #region 绑定终结点
        /// <summary>
        /// 绑定终结点
        /// </summary>
        /// <param name="socket">Socket对象</param>
        /// <param name="endPoint">要绑定的终结点</param>
        public static void BindEndPoint(Socket socket, IPEndPoint endPoint)
        {
            if (!socket.IsBound)
            {
                socket.Bind(endPoint);
            }
        }

        /// <summary>
        /// 绑定终结点
        /// </summary>
        /// <param name="socket">Socket对象</param>        
        /// <param name="ip">服务器IP地址</param>
        /// <param name="port">服务器端口</param>
        public static void BindEndPoint(Socket socket, string ip, int port)
        {
            //创建终结点
            IPEndPoint endPoint = CreateIPEndPoint(ip, port);

            //绑定终结点
            if (!socket.IsBound)
            {
                socket.Bind(endPoint);
            }
        }
        #endregion

        #region 指定Socket对象执行监听
        /// <summary>
        /// 指定Socket对象执行监听，默认允许的最大挂起连接数为100
        /// </summary>
        /// <param name="socket">执行监听的Socket对象</param>
        /// <param name="port">监听的端口号</param>
        public static void StartListen(Socket socket, int port)
        {
            //创建本地终结点
            IPEndPoint localPoint = CreateIPEndPoint(IPHelper.LocalHostName, port);

            //绑定到本地终结点
            BindEndPoint(socket, localPoint);

            //开始监听
            socket.Listen(100);
        }

        /// <summary>
        /// 指定Socket对象执行监听
        /// </summary>
        /// <param name="socket">执行监听的Socket对象</param>
        /// <param name="port">监听的端口号</param>
        /// <param name="maxConnection">允许的最大挂起连接数</param>
        public static void StartListen(Socket socket, int port, int maxConnection)
        {
            //创建本地终结点
            IPEndPoint localPoint = CreateIPEndPoint(LocalHostName, port);

            //绑定到本地终结点
            BindEndPoint(socket, localPoint);

            //开始监听
            socket.Listen(maxConnection);
        }

        /// <summary>
        /// 指定Socket对象执行监听
        /// </summary>
        /// <param name="socket">执行监听的Socket对象</param>
        /// <param name="ip">监听的IP地址</param>
        /// <param name="port">监听的端口号</param>
        /// <param name="maxConnection">允许的最大挂起连接数</param>
        public static void StartListen(Socket socket, string ip, int port, int maxConnection)
        {
            //绑定到本地终结点
            BindEndPoint(socket, ip, port);

            //开始监听
            socket.Listen(maxConnection);
        }
        #endregion

        #region 连接到基于TCP协议的服务器
        /// <summary>
        /// 连接到基于TCP协议的服务器,连接成功返回true，否则返回false
        /// </summary>
        /// <param name="socket">Socket对象</param>
        /// <param name="ip">服务器IP地址</param>
        /// <param name="port">服务器端口号</param>     
        public static bool Connect(Socket socket, string ip, int port)
        {
            try
            {
                //连接服务器
                socket.Connect(ip, port);

                //检测连接状态
                return socket.Poll(-1, SelectMode.SelectWrite);
            }
            catch (SocketException ex)
            {
                throw new Exception(ex.Message);
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
            }
        }
        #endregion

        #region 以同步方式发送消息
        /// <summary>
        /// 以同步方式向指定的Socket对象发送消息
        /// </summary>
        /// <param name="socket">socket对象</param>
        /// <param name="msg">发送的消息</param>
        public static void SendMsg(Socket socket, byte[] msg)
        {
            //发送消息
            socket.Send(msg, msg.Length, SocketFlags.None);
        }

        /// <summary>
        /// 使用UTF8编码格式以同步方式向指定的Socket对象发送消息
        /// </summary>
        /// <param name="socket">socket对象</param>
        /// <param name="msg">发送的消息</param>
        public static void SendMsg(Socket socket, string msg)
        {
            //将字符串消息转换成字符数组
            byte[] buffer = Encoding.Default.GetBytes(msg);

            //发送消息
            socket.Send(buffer, buffer.Length, SocketFlags.None);
        }
        #endregion

        #region 以同步方式接收消息
        /// <summary>
        /// 以同步方式接收消息
        /// </summary>
        /// <param name="socket">socket对象</param>
        /// <param name="buffer">接收消息的缓冲区</param>
        public static void ReceiveMsg(Socket socket, byte[] buffer)
        {
            socket.Receive(buffer);
        }

        /// <summary>
        /// 以同步方式接收消息，并转换为UTF8编码格式的字符串,使用5000字节的默认缓冲区接收。
        /// </summary>
        /// <param name="socket">socket对象</param>        
        public static string ReceiveMsg(Socket socket)
        {
            //定义接收缓冲区
            byte[] buffer = new byte[5000];
            //接收数据，获取接收到的字节数
            int receiveCount = socket.Receive(buffer);

            //定义临时缓冲区
            byte[] tempBuffer = new byte[receiveCount];
            //将接收到的数据写入临时缓冲区
            Buffer.BlockCopy(buffer, 0, tempBuffer, 0, receiveCount);
            //转换成字符串，并将其返回
            return Encoding.Default.GetString(tempBuffer);
        }
        #endregion

        #region 关闭基于Tcp协议的Socket对象
        /// <summary>
        /// 关闭基于Tcp协议的Socket对象
        /// </summary>
        /// <param name="socket">要关闭的Socket对象</param>
        public static void Close(Socket socket)
        {
            try
            {
                //禁止Socket对象接收和发送数据
                socket.Shutdown(SocketShutdown.Both);
            }
            catch (SocketException ex)
            {
                throw ex;
            }
            finally
            {
                //关闭Socket对象
                socket.Close();
            }
        }
        #endregion

        #region 发送电子邮件
        /// <summary>
        /// 发送电子邮件,所有SMTP配置信息均在config配置文件中system.net节设置.
        /// </summary>
        /// <param name="receiveEmail">接收电子邮件的地址</param>
        /// <param name="msgSubject">电子邮件的标题</param>
        /// <param name="msgBody">电子邮件的正文</param>
        /// <param name="IsEnableSSL">是否开启SSL</param>
        public static bool SendEmail(string receiveEmail, string msgSubject, string msgBody, bool IsEnableSSL)
        {
            //创建电子邮件对象
            MailMessage email = new MailMessage();
            //设置接收人的电子邮件地址
            email.To.Add(receiveEmail);
            //设置邮件的标题
            email.Subject = msgSubject;
            //设置邮件的正文
            email.Body = msgBody;
            //设置邮件为HTML格式
            email.IsBodyHtml = true;

            //创建SMTP客户端，将自动从配置文件中获取SMTP服务器信息
            SmtpClient smtp = new SmtpClient();
            //开启SSL
            smtp.EnableSsl = IsEnableSSL;

            try
            {
                //发送电子邮件
                smtp.Send(email);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
