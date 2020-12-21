using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Models
{
    public class UFDAL
    {
        //alterar conection string
        string connectionString = @"Host=localhost;Port=5445;Username=postgres;Password=;Database=db_crud;ApplicationName=CRUD;SSL Mode=Disable;Pooling=false;Connection Idle Lifetime=1;CommandTimeout=10;";
        public IEnumerable<UF> BuscarTodasUF()
        {
            List<UF> lstUF = new List<UF>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = @"SELECT  id, nome FROM UF  ORDER BY uf.nome ;";

                con.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        UF uf = new UF();
                        uf.Id = (int)(dr["id"]);
                        uf.Nome = (dr["nome"]).ToString();
                        lstUF.Add(uf);
                    }
                }
                con.Close();
            }
            return lstUF;
        }
    }
}
