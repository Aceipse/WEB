using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorisk1.Tests
{
    public static class ModelExtensions
    {
        public static IQueryable<T> Include<T>
                (this IQueryable<T> sequence, string path) where T : class
        {
            var dbQuery = sequence as DbQuery<T>;
            if (dbQuery != null)
            {
                return dbQuery.Include(path);
            }
            return sequence;
        }
    }
}
