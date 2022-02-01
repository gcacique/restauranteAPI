using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace ExercicioSala03.Conexao
{
    public class SqlServer
    {

        public List<Entidades.Cliente> ListarCliente()
        {
            var clientes = new List<Entidades.Cliente>();

            clientes.Add(new Entidades.Cliente()
            {
                Identificador = 1,
                Nome = "João",
                Cpf = "12345678900"
            });
            return clientes;
        }

        private readonly SqlConnection _conexao;

        public SqlServer()
        {
            string stringConexao = File.ReadAllText(@"C:\Users\glcac\Documents\Curso RUMO\servidorSql.txt");
            _conexao = new SqlConnection(stringConexao);

        }
        public void InserirCliente(Entidades.Cliente cliente)
        {
            try
            {
                //abrir a conexao
                _conexao.Open();

                //Inserir dados na tabela cliente
                string query = @"INSERT INTO Cliente
                                 (Nome, cpf)
                              VALUES
                                (@Nome, @cpf)";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                    cmd.Parameters.AddWithValue("@Cpf", cliente.Cpf);

                    cmd.ExecuteNonQuery();
                }

            }
            finally
            {
                _conexao.Close();
            }
        }

        public void AtualizarCliente(Entidades.Cliente cliente)
        {
            try
            {
                _conexao.Open();

                string query = @"UPDATE Cliente
                                SET Nome = @Nome
                                WHERE Cpf = @Cpf";
                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                    cmd.Parameters.AddWithValue("@Cpf", cliente.Cpf);

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _conexao.Close();
            }

        }
        public bool VerificarExestenciaCliente(string cpf)
        {
            try
            {
                _conexao.Open();

                string query = @"SELECT Count(Cpf) AS total
                                from Cliente WHERE Cpf = @Cpf;";
                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Cpf", cpf);
                    return Convert.ToBoolean(cmd.ExecuteScalar());
                }
            }
            finally
            {
                _conexao.Close();
            }
        }
        public void DeletarCliente(Entidades.Cliente cliente)
        {
            try
            {
                _conexao.Open();

                string query = @"DELETE FROM Cliente
                                   WHERE Cpf = @Cpf";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Cpf", cliente.Cpf);
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _conexao.Close();
            }

        }

        public Entidades.Cliente SelecionarCliente(string cpf)
        {
            try
            {
                _conexao.Open();

                string query = @"SELECT * FROM Cliente
                                    WHERE Cpf = @Cpf";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@Cpf", cpf);
                    var rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        var cliente = new Entidades.Cliente();
                        cliente.Cpf = cpf;
                        cliente.Nome = rdr["Nome"].ToString();

                        return cliente;
                    }
                    else
                    {
                        throw new InvalidOperationException("Cpf" + cpf + "não encontrado!");
                    }
                }
            }
            finally
            {
                _conexao.Close();
            }
        }

        public List<Entidades.Cliente> ListarClientes()
        {
            var clientes = new List<Entidades.Cliente>();
            try
            {
                _conexao.Open();

                string query = @"SELECT * FROM Cliente";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    var rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        var cliente = new Entidades.Cliente();
                        cliente.Nome = rdr["Nome"].ToString();
                        cliente.Cpf = rdr["Cpf"].ToString();

                        clientes.Add(cliente);
                    }
                }
            }
            finally
            {
                _conexao.Close();
            }
            return clientes;
        }



    }

}


