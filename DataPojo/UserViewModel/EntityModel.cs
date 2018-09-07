using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataPojo.UserViewModel
{
    public abstract class EntityModel
    {
        public string Id { get; set; }

        public EntityModel()
        {
            Id = Guid.NewGuid().ToString();
        }

    }
}
