using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeirsHolding.Core.Interfaces.Database
{
    public interface IDataSource
    {
        Task<List<T>> Set<T>(List<T> model, string key);
        Task<List<T>> Get<T>(string key);
    }
}
