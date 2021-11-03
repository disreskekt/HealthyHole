using Business.Services.Interfaces;
using Domain.Models.Dto;
using HealthyHole.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HrdController : ControllerBase
    {
        private readonly IHrdService _hrdService;

        public HrdController(IHrdService hrdService)
        {
            _hrdService = hrdService;
        }

        [HttpPost]
        [Route("AddEmployee")]
        public IActionResult AddEmployee([FromBody] AddEmployeeDto addEmployeeDto)
        {
            try
            {
                var employee = _hrdService.AddEmployee(addEmployeeDto);

                return Ok(employee);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("EditEmployee")]
        public IActionResult EditEmployee([FromBody] EditEmployeeDto editEmployeeDto)
        {
            try
            {
                var employee = _hrdService.EditEmployee(editEmployeeDto);

                return Ok(employee);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("DeleteEmployee/{Id}")]
        public IActionResult DeleteEmployee(int Id)
        {
            try
            {
                _hrdService.DeleteEmployee(Id);

                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetAllEmployees/{position?}")]
        public IActionResult GetAllEmployees(Position? position = null)
        {
            try
            {
                var employees = _hrdService.GetAllEmployees(position);

                return Ok(employees);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetAllPositions")]
        public IActionResult GetAllPositions()
        {
            try
            {
                var positions = _hrdService.GetAllPositions();

                return Ok(positions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetEmployeeById/{employeeId}")]
        public IActionResult GetEmployeeById(int employeeId)
        {
            try
            {
                var employee = _hrdService.GetEmployeeById(employeeId);

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetStatistics")]
        public IActionResult GetStatistics()
        {
            try
            {
                var statistics = _hrdService.GetStatistics();

                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
