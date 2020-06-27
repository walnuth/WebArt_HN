using System;
using System.Threading.Tasks;
using Signo.Business.Models;

namespace Signo.Business.Interfaces
{
    public interface IIntegranteRepository : IRepository<Integrante>
    {
        //Task<Integrante> ObeterFornecedorEndereco(Guid id);
        //Task<Integrante> ObeterFornecedorProdutosEndereco(Guid id);
    }
}
