using System;
using System.Threading.Tasks;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using GPS.Domain;



namespace GPS.Repository
{
    public class GPSRepository : IGPSRepository
    {
        private readonly GpsDBContext _context;

        public GPSRepository(GpsDBContext context)
        {

            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public async Task<bool> SaveChancesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public ICollection<Empresa> GetAllEmpresasAsync()
        {
            IQueryable<Empresa> query = _context.Empresas
           .Include(p => p.atividade_principais)
           .Include(s => s.atividades_secundarias)
           .Include(b => b.billing).AsNoTracking()
           .Include(q => q.qsas);           
            
            query = query.AsNoTracking()
            .OrderByDescending(c => c.cnpj);
            return  query.ToList();
        }

        public async Task<Empresa[]> GetAllEmpresasAsyncById(int id)
        {
            throw new NotImplementedException();
        }

        public Empresa GetEmpresasByCnpj(string cnpj) 
        {

            IQueryable<Empresa> query = _context.Empresas
             .Include(p => p.atividade_principais)
             .Include(s => s.atividades_secundarias)
             .Include(b => b.billing).AsNoTracking()
             .Include(q => q.qsas)
             .Where(c => c.cnpj == cnpj);

            query = query.AsNoTracking()
            .OrderByDescending(c => c.cnpj);
            return query.FirstOrDefault();

        }

        public bool ExisteEmpresaByCnpj(string cnpj)
        {

            IQueryable<Empresa> query = _context.Empresas
             //.Include(p => p.atividade_principais)
             //.Include(s => s.atividades_secundarias)
             //.Include(b => b.billing).AsNoTracking()
             //.Include(q => q.qsas)
             .Where(c => c.cnpj == cnpj);

            query = query.AsNoTracking()
            .OrderByDescending(c => c.cnpj);

            Empresa empresa = query.FirstOrDefault();

            if (empresa != null)
            {
                return true;
            }
            else {
                return false;
            }

           // return query.FirstOrDefault();

        }
        //public Empresa GetEmpresasWService(string cnpj) 
        //{

        //    return null;

        //}

    }
}
