using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public static class WriteBug2Txt
    {
        #region 记录bug，以便调试
        /// <summary>
        /// 记录bug，以便调试
        /// </summary>
        public static bool WriteTxt(string str)
        {
            try
            {
                string LogPath = HttpContext.Current.Server.MapPath("/err_log/");
                if (!Directory.Exists(LogPath))
                {
                    Directory.CreateDirectory(LogPath);
                }
                FileStream FileStream = new FileStream(System.Web.HttpContext.Current.Server.MapPath("/err_log//xiejun_" + DateTime.Now.ToLongDateString() + "_.txt"), FileMode.Append);
                StreamWriter StreamWriter = new StreamWriter(FileStream);
                //开始写入
                StreamWriter.WriteLine(str);
                //清空缓冲区
                StreamWriter.Flush();
                //关闭流
                StreamWriter.Close();
                FileStream.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
