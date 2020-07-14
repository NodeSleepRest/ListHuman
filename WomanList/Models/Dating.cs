using System;
using System.Collections.Generic;

namespace WomanList.Models
{
    public partial class Dating
    {
        public int Id { get; set; }
        public int? 女性id { get; set; }
        public string 場所 { get; set; }
        public int? 費用 { get; set; }
        public string 時間帯 { get; set; }
        public string 備考 { get; set; }

        public virtual Woman 女性 { get; set; }
    }
}
