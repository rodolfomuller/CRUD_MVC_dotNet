using System;
using System.Collections.Generic;
using Npgsql;
using NpgsqlTypes;

namespace CRUD.Models
{
    public class CidadeDAL:ICidadeDAL
    {
        //alterar conection string
        string connectionString = @"Host=localhost;Port=5445;Username=postgres;Password=;Database=db_crud;ApplicationName=CRUD;SSL Mode=Disable;Pooling=false;Connection Idle Lifetime=1;CommandTimeout=10;";
        public List<Cidade> BuscarTodasCidades()
        {
            List<Cidade> lstcidade = new List<Cidade>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = @"SELECT  cidade.id, ibge, cidade.id_uf , cidade.nome , longitude, latitude, cidade.id_regiao_uf, uf.nome as uf_nome, regiao_uf.nome as regiao_nome
                                    FROM cidade 
                                    INNER JOIN uf on uf.id = cidade.id_uf 
                                    INNER JOIN regiao_uf on regiao_uf.id = cidade.id_regiao_uf
                                    ORDER BY uf.nome, cidade.nome";                
                con.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Cidade cidade = new Cidade();
                        cidade.Id = (int)(dr["id"]);
                        cidade.Ibge = (int)(dr["ibge"]);
                        cidade.Nome = (string)(dr["nome"]);
                        cidade.Iduf = (int)(dr["id_uf"]);
                        cidade.Longitude = (string)(dr["longitude"]);
                        cidade.Latitude = (string)(dr["latitude"]);
                        cidade.Idregiaouf = (int)(dr["id_regiao_uf"]);
                        cidade.UFNome = (string)(dr["uf_nome"]);
                        cidade.RegiaoNome = (string)(dr["regiao_nome"]);
                        lstcidade.Add(cidade);
                    }
                }
                con.Close();
            }
            return lstcidade;
        }
        public void InserirCidade(Cidade cidade)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                string SQL = @"INSERT INTO public.cidade(ibge, id_uf, nome, longitude, latitude, id_regiao_uf)
                                        VALUES (@ibge, @id_uf, @nome, @longitude, @latitude, @id_regiao_uf)";

                NpgsqlCommand cmd = con.CreateCommand();                

                cmd.CommandText = SQL;

                cmd.Parameters.Add(new NpgsqlParameter("ibge", NpgsqlDbType.Integer) { Value = cidade.Ibge });
                cmd.Parameters.Add(new NpgsqlParameter("id_uf", NpgsqlDbType.Integer) { Value = cidade.Iduf });
                cmd.Parameters.Add(new NpgsqlParameter("nome", NpgsqlDbType.Varchar, 200) { Value = cidade.Nome });
                cmd.Parameters.Add(new NpgsqlParameter("longitude", NpgsqlDbType.Varchar, 20) { Value = cidade.Longitude });
                cmd.Parameters.Add(new NpgsqlParameter("latitude", NpgsqlDbType.Varchar, 20) { Value = cidade.Latitude });
                cmd.Parameters.Add(new NpgsqlParameter("id_regiao_uf", NpgsqlDbType.Integer) { Value = cidade.Idregiaouf });
               
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();               
            }
        }
        public void AlterarCidade(Cidade cidade)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                string SQL = @"UPDATE cidade SET ibge = @ibge, id_uf = @id_uf, nome = @nome, longitude = @longitude, latitude = @latitude, id_regiao_uf = @id_regiao_uf
                                WHERE id = @id";
                 
                NpgsqlCommand cmd = con.CreateCommand();

                cmd.CommandText = SQL;

                cmd.Parameters.Add(new NpgsqlParameter("ibge", NpgsqlDbType.Integer) { Value = cidade.Ibge });
                cmd.Parameters.Add(new NpgsqlParameter("id_uf", NpgsqlDbType.Integer) { Value = cidade.Iduf });
                cmd.Parameters.Add(new NpgsqlParameter("nome", NpgsqlDbType.Varchar, 200) { Value = cidade.Nome });
                cmd.Parameters.Add(new NpgsqlParameter("longitude", NpgsqlDbType.Varchar, 20) { Value = cidade.Longitude });
                cmd.Parameters.Add(new NpgsqlParameter("latitude", NpgsqlDbType.Varchar, 20) { Value = cidade.Latitude });
                cmd.Parameters.Add(new NpgsqlParameter("id_regiao_uf", NpgsqlDbType.Integer) { Value = cidade.Idregiaouf });
                cmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer) { Value = cidade.Id });

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
      
        public void DeletarCidade(int id)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                string SQL = @"DELETE FROM cidade WHERE id = @id";

                NpgsqlCommand cmd = con.CreateCommand();
                cmd.CommandText = SQL;
                cmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer) { Value = id });

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }         
        }


        public Cidade BuscarCidadeId(int id)
        {
           
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                var cmd = con.CreateCommand();
                cmd.CommandText = @"SELECT  cidade.id, ibge, cidade.id_uf , cidade.nome , longitude, latitude, cidade.id_regiao_uf, uf.nome as uf_nome, regiao_uf.nome as regiao_nome
                                    FROM cidade 
                                    INNER JOIN uf on uf.id = cidade.id_uf 
                                    INNER JOIN regiao_uf on regiao_uf.id = cidade.id_regiao_uf
                                    WHERE cidade.id = @id";

                cmd.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer) { Value = id });
                con.Open();

                Cidade cidade = new Cidade();
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {                       
                        cidade.Id = (int)(dr["id"]);
                        cidade.Ibge = (int)(dr["ibge"]);
                        cidade.Nome = (string)(dr["nome"]);
                        cidade.Iduf = (int)(dr["id_uf"]);
                        cidade.Longitude = (string)(dr["longitude"]);
                        cidade.Latitude = (string)(dr["latitude"]);
                        cidade.Idregiaouf = (int)(dr["id_regiao_uf"]);
                        cidade.UFNome = (string)(dr["uf_nome"]);
                        cidade.RegiaoNome = (string)(dr["regiao_nome"]);                      
                    }
                }
                con.Close();
                return cidade;
            }            
        }
        /// <summary>
        /// Buscar cidades utilizando filtros abaixo, poderia ser feito com Linq
        /// </summary>
        /// <param name="idUF"></param>
        /// <param name="idRegiaoUF"></param>
        /// <param name="nomeCidade"></param>
        /// <returns></returns>
        public List<Cidade> BuscarCidadesFiltro(int idUF, int idRegiaoUF, string nomeCidade)
        {
            List<Cidade> lstcidade = new List<Cidade>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                var cmd = con.CreateCommand();
                string clausulaWHERE = string.Empty;
                if (idUF != 0 || idRegiaoUF != 0 ||  !string.IsNullOrEmpty(nomeCidade))
                {
                    if (idUF != 0)
                    {
                        clausulaWHERE += " WHERE cidade.id_uf = @id_uf "; // ILIKE NO POSTGRESQL IGNORA CASE SENSITIVE
                        cmd.Parameters.Add(new NpgsqlParameter("id_uf", NpgsqlDbType.Integer) { Value = idUF });

                        if (idRegiaoUF != 0)
                        {
                            clausulaWHERE += " AND cidade.id_regiao_uf = @id_regiao_uf";
                            cmd.Parameters.Add(new NpgsqlParameter("id_regiao_uf", NpgsqlDbType.Integer) { Value = idRegiaoUF });
                        }
                    }

                    if (!string.IsNullOrEmpty(nomeCidade))
                    {
                        if (clausulaWHERE != string.Empty)
                            clausulaWHERE += " AND cidade.nome ILIKE '%@nomeCidade%' ";
                        else
                            clausulaWHERE = " WHERE cidade.nome ILIKE @nomeCidade ";

                        cmd.Parameters.Add(new NpgsqlParameter("nomeCidade", NpgsqlDbType.Varchar) { Value = "%" + nomeCidade + "%" });

                    }
                }

                cmd.CommandText = $@"SELECT  cidade.id, ibge, cidade.id_uf , cidade.nome , longitude, latitude, cidade.id_regiao_uf, uf.nome as uf_nome, regiao_uf.nome as regiao_nome
                                    FROM cidade 
                                    INNER JOIN uf on uf.id = cidade.id_uf 
                                    INNER JOIN regiao_uf on regiao_uf.id = cidade.id_regiao_uf
                                    {clausulaWHERE}
                                    ORDER BY uf.nome, cidade.nome";               

                con.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Cidade cidade = new Cidade();
                        cidade.Id = (int)(dr["id"]);
                        cidade.Ibge = (int)(dr["ibge"]);
                        cidade.Nome = (string)(dr["nome"]);
                        cidade.Iduf = (int)(dr["id_uf"]);
                        cidade.Longitude = (string)(dr["longitude"]);
                        cidade.Latitude = (string)(dr["latitude"]);
                        cidade.Idregiaouf = (int)(dr["id_regiao_uf"]);
                        cidade.UFNome = (string)(dr["uf_nome"]);
                        cidade.RegiaoNome = (string)(dr["regiao_nome"]);
                        lstcidade.Add(cidade);
                    }
                }
                con.Close();
            }
            return lstcidade;
        }

        public IEnumerable<Relatorio> BuscarQtdeCidades(int tipoRelatorio)
        {
            List<Relatorio> lstqtde = new List<Relatorio>();
            using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
            {
                var cmd = con.CreateCommand();
                if (tipoRelatorio == 1)
                    cmd.CommandText = @"SELECT COUNT(cidade.id)::int as qtd,  regiao_uf.nome 
                                        FROM cidade 
                                        INNER JOIN regiao_uf on regiao_uf.id = cidade.id_regiao_uf
                                        GROUP BY regiao_uf.nome 
                                        ORDER BY qtd DESC";
                else
                    cmd.CommandText = @"SELECT COUNT(cidade.id)::int as qtd, uf.nome 
                                        FROM cidade 
                                        INNER JOIN uf on uf.id = cidade.id_uf
                                        GROUP BY uf.nome 
                                        ORDER BY qtd DESC"; 
                    
                con.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Relatorio relQtde = new Relatorio();
                        relQtde.Qtde = (int)(dr["qtd"]);
                        relQtde.Nome = (string)(dr["nome"]);                       
                        lstqtde.Add(relQtde);
                    }
                }
                con.Close();
            }
            return lstqtde;
        }
    }
}
