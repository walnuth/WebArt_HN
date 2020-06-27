using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Signo.Business.Models;


namespace Signo.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Adicionar(TEntity entity);
        // método void não retona nada

        Task<TEntity> ObterPorId(Guid id); 
        //recebe um GUID e retorna TASK DO TIPO Entity. Quando retorna alguma coisa, retonra a ENTIDADE ou uma LISTA da ENTIDADE;

        Task<List<TEntity>> ObterTodos();

        Task Atualizar(TEntity entity);

        Task Remover(Guid id);

        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        // retorna uma lista de TEntity, numa expressão que é uma FUNÇÃO, e compara uma TEntity retornando bool, Função LAMBDA

        Task<int> SaveChanges();

    }
}