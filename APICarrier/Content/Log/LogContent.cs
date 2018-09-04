using log4net.Layout;
using log4net.Layout.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace APICarrier.Content.Log
{
    /// <summary>
    /// 包含了所有的自定字段属性
    /// </summary>
    public class LogContent
    {
        public LogContent(string macAddress, string computerName, string actionsclick, string description)
        {
            UserIP = macAddress;
            UserName = computerName;
            ActionsClick = actionsclick;
            Message = description;
        }

        /// <summary>
        /// 访问IP
        /// </summary>
        public string UserIP { get; set; }

        /// <summary>
        /// 系统登陆用户
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 动作事件
        /// </summary>
        public string ActionsClick { get; set; }

        /// <summary>
        /// 日志描述信息
        /// </summary>
        public string Message { get; set; }


    }
    public class MyLayout : PatternLayout
    {
        public MyLayout()
        {
            this.AddConverter("property", typeof(LogInfoPatternConverter));
        }
    }

    public class LogInfoPatternConverter : PatternLayoutConverter
    {

        protected override void Convert(System.IO.TextWriter writer, log4net.Core.LoggingEvent loggingEvent)
        {
            if (Option != null)
            {
                // Write the value for the specified key
                WriteObject(writer, loggingEvent.Repository, LookupProperty(Option, loggingEvent));
            }
            else
            {
                // Write all the key value pairs
                WriteDictionary(writer, loggingEvent.Repository, loggingEvent.GetProperties());
            }
        }
        /// <summary>
        /// 通过反射获取传入的日志对象的某个属性的值
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>

        private object LookupProperty(string property, log4net.Core.LoggingEvent loggingEvent)
        {
            object propertyValue = string.Empty;
            PropertyInfo propertyInfo = loggingEvent.MessageObject.GetType().GetProperty(property);
            if (propertyInfo != null)
                propertyValue = propertyInfo.GetValue(loggingEvent.MessageObject, null);
            return propertyValue;
        }
    }
}