using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WomanList.Models
{
    public partial class Woman
    {
        public Woman()
        {
            Dating = new HashSet<Dating>();
        }

        public int Id { get; set; }
        public string 仮名 { get; set; }
        public string 本名 { get; set; }
        [Range(18, 100, ErrorMessage = "おいおい、その年齢は冗談だろう")]
        public int? 年齢 { get; set; }
        public int? 居住地 { get; set; }
        public int? 出身地 { get; set; }
        public string 職業 { get; set; }
        public int? 年収 { get; set; }
        [Range(1, 10, ErrorMessage = "1～10段階で評価してね！大きいほど良いという意味だよ！")]
        public int? 顔 { get; set; }
        [Range(1, 10, ErrorMessage = "1～10段階で評価してね！大きいほど良いという意味だよ！")]
        public int? 胸 { get; set; }
        [Range(1, 10, ErrorMessage = "1～10段階で評価してね！大きいほど良いという意味だよ！")]
        public int? 体型 { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy年MM月dd日}")]
        public DateTime? 出会った日 { get; set; }
        public int? 知り合った方法 { get; set; }
        public string 備考 { get; set; }

        public virtual Prefecture 出身地Navigation { get; set; }
        public virtual Prefecture 居住地Navigation { get; set; }
        public virtual Method 知り合った方法Navigation { get; set; }
        public virtual ICollection<Dating> Dating { get; set; }
    }
}
