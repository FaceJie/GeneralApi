using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public static class Cookie
    {
        /// <summary>
        /// 创建Cookies
        /// </summary>
        /// <param name="strName">Cookie 主键</param>
        /// <param name="strValue">Cookie 键值</param>
        /// <param name="strDay">Cookie 天数</param>
        /// <code>Cookie ck = new Cookie();</code>
        /// <code>ck.setCookie("主键","键值","天数");</code>
        public static bool SetCookie(string strName, string strValue, int strDay)
        {
            try
            {
                HttpCookie Cookie = new HttpCookie(strName);
                if (strDay > 0) Cookie.Expires = DateTime.Now.AddDays(strDay);
                Cookie.Value = strValue;
                System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 读取Cookies
        /// </summary>
        /// <param name="strName">Cookie 主键</param>
        /// <code>Cookie ck = new Cookie();</code>
        /// <code>ck.getCookie("主键");</code>
        public static string GetCookie(string strName)
        {
            HttpCookie Cookie = System.Web.HttpContext.Current.Request.Cookies[strName];
            if (Cookie != null)
            {
                return Cookie.Value.ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 删除Cookies
        /// </summary>
        /// <param name="strName">Cookie 主键</param>
        /// <code>Cookie ck = new Cookie();</code>
        /// <code>ck.delCookie("主键");</code>
        public static bool DelCookie(string strName)
        {
            try
            {
                HttpCookie Cookie = new HttpCookie(strName);
                Cookie.Expires = DateTime.Now.AddDays(-1);
                System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
