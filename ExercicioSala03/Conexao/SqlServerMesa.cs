using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace ExercicioSala03.Conexao
{
    public class SqlServerMesa
    {
        public List<Entidades.Mesa> ListarMesas()
        {
            var mesas = new List<Entidades.Mesa>();
            mesas.Add(new Entidades.Mesa()
            {
                Identificador = 1,
                QuantidadeCadeiras = 1,
            });
            return mesas;
        }

        private readonly SqlConnection _conexao;

        public SqlServerMesa()
        {
            string stringConexao = File.ReadAllText(@"C:\Users\glcac\Documents\Curso RUMO\servidorSql.txt");
            _conexao = new SqlConnection(stringConexao);
        }

        public void InserirMesa(Entidades.Mesa mesa)
        {
            try
            {
                _conexao.Open();

                string query = @"INSERT INTO Mesa
                                (QuantidadeCadeiras)
                                VALUES
                                 (@QuantidadeCadeiras);";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@QuantidadeCadeiras", mesa.QuantidadeCadeiras);
                    cmd.ExecuteNonQuery();
                }


            }
            finally
            {
                _conexao.Close();

            }

        }
        public void AtualizarMesa(Entidades.Mesa mesa)
        {
            try
            {
                _conexao.Open();
                string query = @"UPDATE Mesa
                                    SET QuantidadeCadeiras = @QuantidadeCadeiras
                                    WHERE Identificador = @Identificador";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@QuantidadeCadeiras", mesa.QuantidadeCadeiras);

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _conexao.Close();
            }
        }
        public bool VerificarExistenciaMesa(short identificador)
        {
            try
            {
                _conexao.Open();

                string query = @"SELECT COUNT(identificador) AS total 
                                 from Mesa WHERE Identificador = @identificador;";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@identificador", identificador);

                    return Convert.ToBoolean(cmd.ExecuteScalar());
                }
            }
            finally
            {
                _conexao.Close();
            }
        }
        public void DeletarMesa(Entidades.Mesa mesa)
        {
            try
            {
                _conexao.Open();

                string query = @"DELETE FROM Mesa
                                 WHERE Identificador = @identificador";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Cpf", mesa.Identificador);
                    cmd.ExecuteNonQuery();

                }
            }
            finally
            {
                _conexao.Close();
            }
        }
        public Entidades.Mesa SelecionarMesa(short identificador)
        {
            try
            {
                _conexao.Open();

                string query = @"Select * FROM Mesa
                                 WHERE Identificador = @identificador";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@identificador", identificador);
                    var rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        var mesa = new Entidades.Mesa();
                        mesa.Identificador = identificador;
                        mesa.QuantidadeCadeiras = Convert.ToInt16(rdr["QuantidadeCadeiras"]);

                        return mesa;

                    }
                    else
                    {
                        throw new InvalidOperationException("Identificador" + identificador + " não encontrado!");
                    }
                }
            }
            finally
            {
                _conexao.Close();
            }
        }

        public List<Entidades.Mesa> ListarMesa()
        {
            var mesas = new List<Entidades.Mesa>();
            try
            {
                _conexao.Open();

                string query = @"Select * FROM Mesa";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    var rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        var mesa = new Entidades.Mesa();
                        mesa.Identificador = Convert.ToInt32(rdr["Identificador"]);
                        mesa.QuantidadeCadeiras = Convert.ToInt16(rdr["QuantidadeCAdeiras"]);

                        mesas.Add(mesa);
                    }
                }
            }
            finally
            {
                _conexao.Close();
            }
            return mesas;
        }
    }
}
