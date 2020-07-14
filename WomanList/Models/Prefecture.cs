using System;
using System.Collections.Generic;

namespace WomanList.Models
{
    public partial class Prefecture
    {
        public Prefecture()
        {
            Woman出身地Navigation = new HashSet<Woman>();
            Woman居住地Navigation = new HashSet<Woman>();
        }

        public int Id { get; set; }
        public string 名称 { get; set; }

        public virtual ICollection<Woman> Woman出身地Navigation { get; set; }
        public virtual ICollection<Woman> Woman居住地Navigation { get; set; }
    }
}
