using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        CategoryRepository Category { get; }
        CoverTypeRepository CoverType { get; }
        ProductRepository Product { get; }

        void Save();
    }
}
