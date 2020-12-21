using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRUD.Models
{
    public class CidadeSimples:Cidade
    {
        public List<Cidade> CidadeModel { get; set; }
        public SelectList UFs { get; set; }
        public SelectList Regioes { get; set; }
        public int Iduf { get; set; }
        public int Idregiaouf { get; set; }


    }
   
}
