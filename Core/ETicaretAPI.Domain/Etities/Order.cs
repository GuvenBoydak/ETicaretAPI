using ETicaretAPI.Domain.Etities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Etities
{
    public  class Order:BaseEntity
    {
        public string Description { get; set; }

        public string Adress { get; set; }

        public Guid CustomerID { get; set; }

        public ICollection<Product> Products { get; set; }

        public Customer Customer { get; set; }
    }
}
