using System.Threading;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    namespace Common.Domain
    {
        public interface IRepository<T> where T : class, IAggregateRoot
        {
            Task AddAsync(T entity, CancellationToken cancellationToken = default);
            void Delete(T entity);
        }
    }
}