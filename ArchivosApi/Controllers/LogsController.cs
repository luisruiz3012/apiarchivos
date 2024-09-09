using Microsoft.AspNetCore.Mvc;
using UserLogsLibrary;
using UserLogsLibrary.Models;

namespace ArchivosApi.Controllers
{
    [ApiController]
    [Route("api/logs")]
    public class LogsController : ControllerBase
    {
        private readonly Methods _logs;
        public LogsController() { 
            _logs = new Methods();
        }

        [HttpGet]
        [Route("")]
        public dynamic Get()
        {
            try
            {
                var request = _logs.Get();

                if (request == null)
                {
                    return NotFound();
                }

                return request;

            } catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public dynamic GetById(int id)
        {
            try
            {
                var request = _logs.GetById(id);

                if (request == null)
                {
                    return NotFound();
                }

                return request;

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("")]
        public dynamic Create([FromBody] UserLog log)
        {
            try
            {
                var request = _logs.Create(log);

                if (request == null)
                {
                    return BadRequest();
                }

                return request;

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch]
        [Route("{id}")]
        public dynamic Update(int id, [FromBody] UserLog log)
        {
            try
            {
                var request = _logs.Update(id, log);

                if (request == null)
                {
                    return NotFound();
                }

                return request;

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public dynamic Delete(int id)
        {
            try
            {
                var request = _logs.Delete(id);

                if (request == null)
                {
                    return NotFound();
                }

                return request;

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
