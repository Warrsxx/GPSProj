using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GPS.Domain;


namespace GPS.Repository
{
    public class SeedDatabase
    {

        public static void SeedEmpresas(GpsDBContext context)
        {
            if (!context.Empresas.Any())
            {
                var empresa = new Empresa
                {
                    atividade_principais = new List<atividade_principal>
                    { 
                        new atividade_principal {
                        text = "Atividades de televisão aberta",
                        code = "60.21-7-00"
                    }

                    },
                    data_situacao = "03/11/2005",
                    tipo= "MATRIZ",
                    nome= "GLOBO COMUNICACAO E PARTICIPACOES S/A",
                    uf= "RJ",
                    telefone= "(21) 2155-4551/ (21) 2155-4552",
                    atividades_secundarias = new List<atividades_secundarias>
                   {
                       new atividades_secundarias {
                           text= "Produção de filmes para publicidade",
                           code= "59.11-1-02"
                       },

                       new atividades_secundarias {
                           text="Atividades de produção cinematográfica, de vídeos e de programas de televisão não especificadas anteriormente",
                           code=  "59.11-1-99"
                       }
                   },
                    qsas = new List<qsa> { 
                        new qsa {
                            qual= "10-Diretor",
                            nome= "CARLOS HENRIQUE SCHRODER"
                        },
                        new qsa {
                            qual= "10-Diretor",
                            nome= "JORGE LUIZ DE BARROS NOBREGA"
                        },
                        new qsa {
                            qual= "10-Diretor",
                            nome= "MARCELO LUIS MENDES SOARES DA SILVA"
                        }
                    },
                    situacao= "ATIVA",
                    bairro= "JARDIM BOTANICO",
                    logradouro= "R LOPES QUINTAS",
                    numero= "303",
                    cep= "22.460-901",
                    municipio= "RIO DE JANEIRO",
                    //porte= "DEMAIS",
                    abertura= "31/01/1986",
                    natureza_juridica= "205-4 - Sociedade Anônima Fechada",
                    fantasia= "TV/REDE/CANAIS/G2C+GLOBO SOMLIVRE GLOBO.COM GLOBOPLAY",
                    cnpj= "27.865.757/0001-02",
                    //ultima_atualizacao= "2021-06-05T23=35=46.754Z",
                    status= "OK",
                    complemento= "",
                    email= "",
                    efr= "",
                    motivo_situacao= "",
                    situacao_especial= "",
                    data_situacao_especial= "",
                    capital_social= "6983568523.86",
                    //extra= { },

                    //billing= new billing {
                    //    free= true,
                    //    database= true
                    //}

            };
                //context.Empresas.AddRange(empresas);
                context.Empresas.Add(empresa);
                context.SaveChanges();


            };
        }
    }
}
