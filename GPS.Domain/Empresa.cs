using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GPS.Domain
{
    public class Empresa
    {
        [Key]
        public int Id { get; set; }
        public string status { get; set; }
        public string message{ get; set; }
        public billing billing{ get; set; }
        public string cnpj{ get; set; }
        public string tipo{ get; set; }
        public string abertura{ get; set; }
        public string nome{ get; set; }
        public string fantasia{ get; set; }
        public ICollection<atividade_principal> atividade_principais { get; set; }
        public ICollection<atividades_secundarias> atividades_secundarias{ get; set; }
        public string natureza_juridica{ get; set; }
        public string logradouro{ get; set; }
        public string numero{ get; set; }
        public string complemento{ get; set; }
        public string cep{ get; set; }
        public string bairro{ get; set; }
        public string municipio{ get; set; }
        public string uf{ get; set; }
        public string email{ get; set; }
        public string telefone{ get; set; }
        public string efr{ get; set; }
        public string situacao{ get; set; }
        public string data_situacao{ get; set; }
        public string motivo_situacao{ get; set; }
        public string situacao_especial{ get; set; }
        public string data_situacao_especial{ get; set; }
        public string capital_social{ get; set; }
        public ICollection<qsa> qsas{ get; set; }

    }
}
