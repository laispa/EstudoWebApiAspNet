using EstudoWebAPI.Database;
using EstudoWebAPI.Helpers;
using EstudoWebAPI.Repository.Interface;
using EstudoWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstudoWebAPI.Repository
{
    public class PalavrasRepository : IPalavraRepository
    {
        private readonly MimicContext _banco;
        public PalavrasRepository(MimicContext banco) //construtor recebe banco
        {
            _banco = banco;  // fazendo injeção de depedência
        }
        
        public List<Palavra> ObterPalavras(PalavraUrlQuery query)
        {
            var item = _banco.Palavras.AsNoTracking().AsQueryable(); //converte em query para facilitar o filtro - Converte um IEnumerable em um IQueryable .
            if (query.Data.HasValue)
            {
                item = item.Where(a => a.Criado > query.Data.Value || a.Atualizado > query.Data.Value); // filtrar os registros criados após a query.Data

            }

            if (query.PagNumero.HasValue)
            {
                var quantidadeTotalRegistros = item.Count();
                item = item.Skip((query.PagNumero.Value - 1) * query.NRegistroPag.Value).Take(query.NRegistroPag.Value);
                var paginacao = new Paginacao();
                paginacao.NumeroPagina = query.PagNumero.Value;
                paginacao.RegistroPorPagina = query.NRegistroPag.Value;
                paginacao.TotalRegistros = quantidadeTotalRegistros;
                paginacao.TotalPaginas = (int)Math.Ceiling((double)quantidadeTotalRegistros / query.NRegistroPag.Value);//arredonda pra cima 2,1 vira 3
               
            }
            /*return new JsonResult (_banco.Palavras);  OU */
            return item.ToList(); // automaticamente converte para o json
        }

        public Palavra Obter(int id)
        {
           return _banco.Palavras.AsNoTracking().FirstOrDefault(a => a.Id == id);
        }


        public void Cadastrar(Palavra palavra)
        {
            _banco.Palavras.Add(palavra);
            _banco.SaveChanges();
        }

        public void Atualizar(Palavra palavra)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int id)
        {
            var palavra = Obter(id);
            _banco.Palavras.Update(palavra);
            _banco.SaveChanges();

        }
    }
}
