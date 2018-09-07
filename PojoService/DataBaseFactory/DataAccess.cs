using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PojoService
{
    public sealed class DataAccess
    {
        private static readonly string AssemblyPath = ConfigurationManager.AppSettings["DAL"];
        public DataAccess()
        { }

        #region CreateObject 

        //不使用缓存
        private static object CreateObjectNoCache(string AssemblyPath, string classNamespace)
        {
            try
            {
                object objType = Assembly.GetExecutingAssembly().CreateInstance(classNamespace);
                return objType;
            }
            catch
            {
                return null;
            }

        }
        //使用缓存
        private static object CreateObject(string AssemblyPath, string classNamespace)
        {
            object objType = Common.DataCache.GetCache(classNamespace);
            if (objType == null)
            {
                try
                {
                    objType = Assembly.GetExecutingAssembly().CreateInstance(classNamespace);
                    Common.DataCache.SetCache(classNamespace, objType);// 写入缓存
                }
                catch (System.Exception ex)
                {
                    string str = ex.Message;// 记录错误日志
                }
            }
            return objType;
        }
        #endregion

        public static I create_interface<I>(string AssemblyPath, string class_name)
        {
            string ClassNamespace = AssemblyPath + "." + class_name;
            object objType = CreateObjectNoCache(AssemblyPath, ClassNamespace);
            return (I)objType;
        }

        public static I create_idal<I>(string class_name)
        {
            string ClassNamespace = AssemblyPath + "." + class_name;
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (I)objType;
        }
        public static I create_idal<I>(string folder, string class_name)
        {
            string ClassNamespace = AssemblyPath + "." + folder + "." + class_name;
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (I)objType;
        }
    }
}
