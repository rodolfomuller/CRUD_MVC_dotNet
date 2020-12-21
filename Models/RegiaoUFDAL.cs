using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUD.Models
{
    public class RegiaoUFDAL
    {
        

        //alterar conection string
        string connectionString = @"Host=localhost;Port=5445;Username=postgres;Password=;Database=db_crud;ApplicationName=CRUD;SSL Mode=Disable;Pooling=false;Connection Idle Lifetime=1;CommandTimeout=10;";

        public IEnumerable BuscarTodasRegioes()
        {
            List<RegiaoUF> lstRegiao = new List<RegiaoUF>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = @"SELECT  id, nome, id_uf FROM regiao_uf  ORDER BY nome ;";

                con.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        RegiaoUF regiaoUf = new RegiaoUF();
                        regiaoUf.Id = (int)(dr["id"]);
                        regiaoUf.Nome = (dr["nome"]).ToString();
                        regiaoUf.IdUF = (int)(dr["id_uf"]);
                        lstRegiao.Add(regiaoUf);
                    }
                }
                con.Close();
            }
            return lstRegiao;
        }

        internal List<RegiaoUF> BuscarRegioesUF(int idUf)
        {
            List<RegiaoUF> lstRegiao = new List<RegiaoUF>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = @"SELECT  id, nome, id_uf FROM regiao_uf where id_uf= @id_uf ORDER BY nome ;";
                cmd.Parameters.Add(new NpgsqlParameter("id_uf", NpgsqlDbType.Integer) { Value = idUf });

                con.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        RegiaoUF regiaoUf = new RegiaoUF();
                        regiaoUf.Id = (int)(dr["id"]);
                        regiaoUf.Nome = (dr["nome"]).ToString();
                        regiaoUf.IdUF = (int)(dr["id_uf"]);
                        lstRegiao.Add(regiaoUf);
                    }
                }
                con.Close();
            }
            return lstRegiao;

        }
    }
}
