using HeirsHolding.Core.Interfaces.Database;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeirsHolding.Infrastructure.DataBase
{
    public class DataSource : IDataSource
    {
        private readonly IMemoryCache _distributedCache;
        public DataSource(IMemoryCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<List<T>> Get<T>(string key)
        {
            var resp = _distributedCache.Get(key);
            var res = new List<T>();
            if (resp != null)
            {
                res = JsonConvert.DeserializeObject<List<T>>(resp.ToString());
            }
            return res;
        }


        public async Task<List<T>> Set<T>(List<T> model,string key)
        {
            //get 
            var records = await Get<T>(key);

            if (records.Count != default)
            {
                records.AddRange(model);
            }
            else
            {
                records = model;
            }

            var payload = JsonConvert.SerializeObject(records);

            _distributedCache.Set(key, payload, new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(120)
            });
            return records;
        }
    }
}
