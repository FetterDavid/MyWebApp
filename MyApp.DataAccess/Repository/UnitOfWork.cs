using MyApp.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyAppDbContext _db;
        public CategoryRepository Category { get; private set; }
        public CoverTypeRepository CoverType { get; private set; }
        public ProductRepository Product { get; private set; }

        public UnitOfWork(MyAppDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            CoverType = new CoverTypeRepository(_db);
            Product = new ProductRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
