using ArchivosLibrary;
using ArchivosLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArchivosApi.Controllers
{
    [ApiController]
    [Route("api/archivos")]
    public class ArchivosController : ControllerBase
    {
        private readonly Methods _archivos;

        public ArchivosController()
        {
            _archivos = new Methods();
        }

        [HttpGet]
        [Route("")]
        public dynamic Get()
        {
            try
            {
                var request = _archivos.Get();

                if (request == null)
                {
                    return NotFound();
                }

                return request;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error {ex.Message}");
            }
        }


        [HttpGet]
        [Route("{id}")]
        public dynamic GetById(int id)
        {
            try
            {
                var request = _archivos.GetById(id);

                if (request == null)
                {
                    return NotFound();
                }

                return request;
            } catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error {ex.Message}");
            }
        }

        [HttpPost]
        [Route("")]
        public dynamic Create([FromBody] Archivo archivo, [FromQuery] int usuario_id)
        {
            try
            {
                var request = _archivos.Create(archivo, usuario_id);

                if (request == null)
                {
                    return BadRequest();
                }

                return request;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error {ex.Message}");
            }
        }

        [HttpPatch]
        [Route("{id}")]
        public dynamic Update(int id, [FromBody] Archivo archivo)
        {
            try
            {
                var request = _archivos.Update(id, archivo);

                if (request == null)
                {
                    return NotFound();
                }

                return request;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public dynamic Delete(int id, [FromQuery] int usuario_id)
        {
            try
            {
                var request = _archivos.Delete(id, usuario_id);

                if (request == null)
                {
                    return NotFound();
                }

                return request;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error {ex.Message}");
            }
        }

        [HttpPost]
        [Route("restore/{id}")]
        public dynamic Restore(int id, [FromQuery] int usuario_id)
        {
            try
            {
                var request = _archivos.Restore(id, usuario_id);

                if (request == null)
                {
                    return NotFound();
                }

                return request;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error {ex.Message}");
            }
        }
    }
}
