using Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HealthyHole.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KppController : ControllerBase
    {
        private readonly IKppService _kppService;
        public KppController(IKppService kppService)
        {
            _kppService = kppService;
        }

        [HttpGet]
        [Route("StartShift/{employeeId}/{startTime}")]
        public IActionResult StartShift(int employeeId, string startTime)
        {
            try
            {
                _kppService.StartShift(employeeId, startTime);

                return Ok();
            }
            catch (AccessViolationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("EndShift/{employeeId}/{endTime}")]
        public IActionResult EndShift(int employeeId, string endTime)
        {
            try
            {
                _kppService.EndShift(employeeId, endTime);

                return Ok();
            }
            catch (AccessViolationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FormatException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
