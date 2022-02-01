using ExercicioSala03.Conexao;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ExercicioSala03.Controllers
{

    [ApiController]
    public class RestauranteController : ControllerBase
    {
        private readonly SqlServer _sql;
        private readonly SqlServerMesa _sqlMesa;

        public RestauranteController()
        {
            _sql = new SqlServer();
            _sqlMesa = new SqlServerMesa();
        }

        [HttpPost("v1/Cliente")]
        public IActionResult InserirCliente(Entidades.Cliente cliente)
        {
            try
            {
                if (cliente.sexo != "M" && cliente.sexo != "F")
                    throw new InvalidOperationException("Erro, sexo não identificado!");

                _sql.InserirCliente(cliente);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
            return StatusCode(201);
        }

        [HttpPut("v1/Cliente")]
        public IActionResult atualizarCliente(Entidades.Cliente cliente)
        {
            
            try
            {
                if (!Services.Utils.ValidaCpf.IsCpf(cliente.Cpf))
                throw new InvalidOperationException("Cpf inválido.");

                                 
                _sql.AtualizarCliente(cliente);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
            return StatusCode(201);
        

        }
        [HttpDelete("v1/Cliente")]
        public void DeletarCliente(Entidades.Cliente cliente)
        {
            _sql.DeletarCliente(cliente);
        }

        [HttpGet("v1/Cliente")]
        public List<Entidades.Cliente> ListarClientes()
        {
            return _sql.ListarClientes();
        }

        [HttpGet("v1/Cliente/{cpf}")]
        public Entidades.Cliente selecionarCliente(string cpf)
        {
            return _sql.SelecionarCliente(cpf);
        }



        [HttpPost("v1/Mesa")]
        public void inserirMesa(Entidades.Mesa mesa)
        {
            _sqlMesa.InserirMesa(mesa);
        }

        [HttpPut("v1/Mesa")]
        public void atualizarMesa(Entidades.Mesa mesa)
        {
            _sqlMesa.AtualizarMesa(mesa);
        }

        [HttpDelete("v1/Mesa")]
        public void DeletarMesa(Entidades.Mesa mesa)
        {
            _sqlMesa.DeletarMesa(mesa);
        }

        [HttpGet("v1/Mesa")]
        public List<Entidades.Mesa> ListarMesa()
        {
            return _sqlMesa.ListarMesa();
        }

        [HttpGet("v1/Mesa/{identificador}")]
        public Entidades.Mesa selecionarMesa(short identificador)
        {
            return _sqlMesa.SelecionarMesa(identificador);
        }

            }
        }
