using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public interface IRepository<T> where T: class,new()
    {
        public Task<long> CuentaTotal();
        public Task<long> Cuenta(Expression<Func<T, bool>> selector);
        public Task<T> ObtenerEntidad<TID>(TID id);
        public Task<IList<T>> ObtenerEntidades();
        public Task<IList<T>> ObtenerEntidades(Expression<Func<T, bool>> selector);
        public Task<T?> PrimeroOValorPredeterminado(Expression<Func<T, bool>> selector);
        public Task<T> Agregar(T entidad);
        public Task<T> Modificar<TID>(TID id,T entidad);
        public Task<bool> Eliminar<TID>(TID id);
        public Task<bool> Eliminar(T entidad);
        public Task<IList<T>> ObtenerEntidades<TProperty, TSecondProperty>(Expression<Func<T, bool>> selector, Expression<Func<T, TProperty>> includeClause, Expression<Func<T, TSecondProperty>> secondIncludeClause);
    }
}
