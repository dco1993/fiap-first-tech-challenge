using Domain.Entity;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repository
{
    public class RegiaoRepository : AppRepository<Regiao>, IRegiaoRepository
    {
        public RegiaoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
