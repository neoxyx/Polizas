using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using ProyectoPolizas.Models;
using ProyectoPolizas.Repositories;
using System;
using Microsoft.Extensions.Logging;

namespace ProyectoPolizas.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PolizasController : ControllerBase
    {
        private readonly ILogger<PolizasController> _logger;
        private readonly PolizaRepository _repository;

        public PolizasController(PolizaRepository repository, ILogger<PolizasController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("BuscarPorPlaca/{placa}")]
        public ActionResult<List<Poliza>> BuscarPorPlaca(string placa)
        {
            _logger.LogInformation("Se ha accedido a MiEndpoint.");
            try
            {
                // Lógica de tu endpoint
                var polizas = _repository.BuscarPorPlaca(placa);

                if (polizas.Count == 0)
                {
                    return NotFound($"No se encontraron pólizas con la placa {placa}");
                }
                _logger.LogInformation("Operación exitosa.");
                return Ok(polizas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en MiEndpoint.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpGet("BuscarPorNumeroPoliza/{numeroPoliza}")]
        public ActionResult<List<Poliza>> BuscarPorNumeroPoliza(int numeroPoliza)
        {
            var polizas = _repository.BuscarPorNumeroPoliza(numeroPoliza);

            if (polizas.Count == 0)
            {
                return NotFound($"No se encontraron pólizas con el número de póliza {numeroPoliza}");
            }

            return Ok(polizas);
        }

        [HttpPost]
        public IActionResult CrearPoliza(Poliza poliza)
        {
            _repository.InsertarPoliza(poliza);
            return Ok();
        }

        internal object CreatePoliza(Poliza validPoliza)
        {
            throw new NotImplementedException();
        }
    }
}
