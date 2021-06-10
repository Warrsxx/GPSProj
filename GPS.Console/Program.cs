using GPS.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GPS.Domain;
using GPS.Repository;

namespace GPSConsole
{
    class Program
    {
        static void Main(string[] args)
        {


            try
            {
                var contextOptions = new DbContextOptionsBuilder<GpsDBContext>()
                                    .UseSqlServer("Data Source=DESKTOP-S4PRAAC\\MSSQLLOCAL;Initial Catalog=GPSPrjDB;User ID=WebApp;Password=123456;Integrated Security=SSPI;Trusted_Connection=false;")
                                    .Options;

                using var context = new GpsDBContext(contextOptions);
                SeedDatabase.SeedEmpresas(context);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }


            Console.WriteLine("Hello World!");
            RunAsync().Wait();
            Console.ReadKey();

        }
        static async Task RunAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri("https://www.receitaws.com.br/v1/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("cnpj/27865757000102");
                if (response.IsSuccessStatusCode)
                {  //GET
                    Empresa Empresa = await response.Content.ReadAsAsync<Empresa>();
                    Console.WriteLine("{0}\t{1}\t{2}", Empresa.nome, Empresa.fantasia, Empresa.logradouro);
                    Console.WriteLine("Empresa acessado e exibido.  Tecle algo para incluir um novo produto.");
                    Console.ReadKey();
                }
            }

        }
    }
}
