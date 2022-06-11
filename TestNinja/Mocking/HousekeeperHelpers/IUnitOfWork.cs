using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.Mocking.HousekeeperHelpers
{
    public interface IUnitOfWork
    {
        IQueryable<T> Query<T>();
    }
}
