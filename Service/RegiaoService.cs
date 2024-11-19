using Domain.Entity;
using Domain.Repository;

namespace Service
{
    public class RegiaoService : IRegiaoRepository
    {
        private readonly IRegiaoRepository _regiaoRepository;

        public RegiaoService()
        {
        }

        public RegiaoService(IRegiaoRepository regiaoRepository)
        {
            _regiaoRepository = regiaoRepository;
        }

        public void Atualizar(Regiao entidade)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(Regiao entidade)
        {
            throw new NotImplementedException();
        }

        public void Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public Regiao ObterPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Regiao> ObterTodos()
        {
            return _regiaoRepository.ObterTodos();
        }
    }
}
