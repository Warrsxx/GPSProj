using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPS.Domain;
using GPS.Repository;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GPS.WebApp.Controllers
{
    public class EmpresaController : Controller
    {
        private readonly IGPSRepository _repo;

        string Baseurl = "https://www.receitaws.com.br/v1/";

        public EmpresaController(IGPSRepository repo)
        {
            _repo = repo;
        }

 
        public ActionResult Index()
        {
            return View();
        }

        public string GetEmpresas()
        {
            try
            {
                ICollection<Empresa> Empresas = _repo.GetAllEmpresasAsync();

                string output = JsonConvert.SerializeObject(Empresas);

                return output;

            }
            catch (System.Exception ex)
            {

                string msg = ex.Message.ToString();

                return null;
                //return this.StatusCode(StatusCodes.Status500InternalServerError, msg);
                //return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de Dados Falhou {ex.message}");

            }
        }

        public string GetEmpresasWService(string cnpj)
        {
            

            using (var client = new HttpClient())
            {
                Empresa empresaP = null;

                client.BaseAddress = new Uri(Baseurl);

                //HTTP GET
                var responseTask = client.GetAsync("cnpj/" + cnpj);
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Empresa>();
                    readTask.Wait();
                    empresaP = readTask.Result;
                }
                else
                {
                    empresaP = null;
                    ModelState.AddModelError(string.Empty, "Erro no servidor. Contate o Administrador.");
                }


                List<Empresa> list = new List<Empresa>();
                list.Add(empresaP);

                ICollection<Empresa> collection = list;
 

                string output = JsonConvert.SerializeObject(collection);
                return output;
 
            }

        }
        public string SaveEmpresas(string empresa)
        {
            try
            {
                ICollection<Empresa> Empresas = JsonConvert.DeserializeObject<ICollection<Empresa>>(empresa);

                string output = "";

                Empresa Emp = Empresas.ToArray()[0];

                Empresa empresaExiste = _repo.GetEmpresasByCnpj(Emp.cnpj);

                if (empresaExiste != null)
                {
                    Emp.Id = empresaExiste.Id;

                    _repo.Update(Emp);

                    _repo.SaveChancesAsync();

                    output = String.Format("Empresa: {0} \n Cnpj: {1} \n Atualizada com Sucesso!", Emp.nome, Emp.cnpj);

                    output = JsonConvert.SerializeObject(output);
                }
                else {

                    _repo.Add(Emp);

                    _repo.SaveChancesAsync();

                    output = String.Format("Empresa: {0} \n Cnpj: {1} \n Adicionada a Base com Sucesso!", Emp.nome, Emp.cnpj);

                    output = JsonConvert.SerializeObject(output);
                }

                return output;

            }
            catch (System.Exception ex)
            {

                string msg = ex.Message.ToString();

                return null;

            }
        }

        static async Task RunAsync(string cnpj)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri("https://www.receitaws.com.br/v1/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("cnpj/" + cnpj);
                if (response.IsSuccessStatusCode)
                {  //GET
                    Empresa Empresa = await response.Content.ReadAsAsync<Empresa>();
                }
            }
        }


         
    }
}
