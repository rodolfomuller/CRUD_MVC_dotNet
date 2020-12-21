using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CRUD.Models
{
    interface ICidadeDAL
    {
        List<Cidade> BuscarTodasCidades();
        public Cidade BuscarCidadeId(int id);
        void InserirCidade( Cidade cidade);
        void AlterarCidade(Cidade cidade);
        void DeletarCidade(int id);
    }
}
