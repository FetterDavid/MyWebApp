using MyApp.DataAccess.Repository.IRepository;
using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        MyAppDbContext _db;

        public CoverTypeRepository(MyAppDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(CoverType coverType)
        {
            dbSet.Update(coverType);
        }
    }
}
