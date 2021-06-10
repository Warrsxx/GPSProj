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
    public class qsa
    {
        [Key]
        public int id { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Empresa Empresa { get; set; }
        public string nome { get; set; }
        public string qual { get; set; }
        public string pais_origem { get; set; }
        public string nome_rep_legal { get; set; }
        public string qual_rep_legal { get; set; }
    }
}
