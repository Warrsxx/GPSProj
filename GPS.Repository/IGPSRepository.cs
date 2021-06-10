using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GPS.Domain;


namespace GPS.Repository
{
    public interface IGPSRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChancesAsync();
        ICollection<Empresa> GetAllEmpresasAsync();
        Empresa GetEmpresasByCnpj(string cnpj);
        bool ExisteEmpresaByCnpj(string cnpj);
        //Empresa GetEmpresasWService(string cnpj);
        Task<Empresa[]> GetAllEmpresasAsyncById(int id);
    }
}
