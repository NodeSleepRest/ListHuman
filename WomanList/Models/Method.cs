using System;
using System.Collections.Generic;

namespace WomanList.Models
{
    public partial class Method
    {
        public Method()
        {
            Woman = new HashSet<Woman>();
        }

        public int Id { get; set; }
        public string 名称 { get; set; }
        public string 備考 { get; set; }

        public virtual ICollection<Woman> Woman { get; set; }
    }
}
