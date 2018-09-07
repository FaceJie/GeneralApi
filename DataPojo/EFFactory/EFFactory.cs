using DataPojo.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataPojo
{
    public class EFFactory
    {
        private static OpenAuthDBEntities context;
        public static DbContext GetContext()
        {
            if (context == null)
            {
                context = new OpenAuthDBEntities();
            }
            return context;

        }
    }
}
