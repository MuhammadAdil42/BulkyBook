using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        T GetFirstOrDefault(Expression <Func<T, bool>> filter); //Generic implementation of Get First or Default function
         IEnumerable<T> GetAll(); // Generic Implementation of finding all the rows from a DB

        void Add(T entity); //Generic Implementation of adding a new row into a DB
        void Remove (T entity); //Generic Implementation of removing a row from DB
        void RemoveRange (IEnumerable<T> entities); //Generic Implementation of removing multiple rows from DB
    }
}
