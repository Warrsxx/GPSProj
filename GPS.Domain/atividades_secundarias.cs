using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GPS.Domain
{
    public class atividades_secundarias
    {
        [Key]
        public int id { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Empresa Empresa { get; set; }
        public string code { get; set; }
        public string text { get; set; }
    }
}
