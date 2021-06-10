using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GPS.Domain
{
    public class billing
    {
        [Key]
        public int id { get; set; }
        
        [ForeignKey("EmpresaId")]
        [JsonIgnore]
        [IgnoreDataMember] 
        public virtual Empresa Empresa { get; set; }
        public Boolean free { get; set; }
        public Boolean database { get; set; }
    }
}
