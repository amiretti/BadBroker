using System.Collections.Generic;
using System.Threading.Tasks;

namespace BadBroker.Application.Validations
{
    public interface IValidations<in T>
    {
        Task<ICollection<string>> Validate(T entity);
    }
}