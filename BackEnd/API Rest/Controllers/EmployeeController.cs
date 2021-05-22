using Core.ActionFilters;
using Core.BackEnd;
using Core.DataAccess;
using Core.Models;
using Core.ValidationAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace API_Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        // GET api/<EmployeeController>/{id}
        [HttpGet("{id}")]
        [Employee_IdExistsActionFilter]
        public IActionResult Get(int id)
        {
            // Obtiene el empleado de la Base de Datos.
            Employee dbEmployee = new EmployeeSC().GetEmployeeById(id);
            // Genera un modelo con la información del empleado obtenido.
            EmployeePersonalInfoDTO employee = new(dbEmployee);

            return Ok(employee);
        }

        // GET: api/<EmployeeController>/pages/{page}
        [HttpGet]
        [Route("pages/{requestedPage}")]
        public IActionResult GetPage([PositiveInteger] int requestedPage)
        {
            const int elementsPerPage = 10;

            // Cálculo de las Páginas.
            int lastPage = new EmployeeSC().CalculateLastPage(elementsPerPage);
            Pagination<EmployeePersonalInfoDTO> response = new(requestedPage, lastPage);

            if (lastPage == 0)
                return Ok(response);

            // Selección de la página solicitada.
            IQueryable<Employee> dbEmployees = new EmployeeSC()
                .GetPage(elementsPerPage, (int)response.CurrentPage);

            // Materialización de los registros.
            List<EmployeePersonalInfoDTO> employees = BaseSC
                .MaterializeIQueryable<Employee, EmployeePersonalInfoDTO>(dbEmployees);

            // Los registros se añaden a la respuesta.
            response.ResponseList = employees;

            return Ok(response);
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public IActionResult GetAll()
        {
            // Se obtienen todos los registros.
            IQueryable<Employee> dbEmployees = new EmployeeSC().GetAllEmployees();

            // Se materialzian.
            List<EmployeePersonalInfoDTO> employees = BaseSC
                .MaterializeIQueryable<Employee, EmployeePersonalInfoDTO>(dbEmployees);

            return Ok(employees);
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public IActionResult Post([FromBody] EmployeePersonalInfoPostDTO newEmployee)
        {
            // Se añade un registro y se obtiene el id con el que se registró.
            int id = new EmployeeSC().AddNewEmployee(newEmployee);

            return Created("GET " + Request.Path.Value + "/" + id, new { Id = id });
        }

        // PUT api/<EmployeeController>/{id}
        [HttpPut("{id}")]
        [Employee_IdExistsActionFilter]
        public IActionResult Put(int id, [FromBody] EmployeePersonalInfoPutDTO modifiedEmployee)
        {
            // Se actualiza un registro.
            new EmployeeSC().UpdateEmployee(id, modifiedEmployee);

            return NoContent();
        }

        // DELETE api/<EmployeeController>/{id}
        [HttpDelete("{id}")]
        [Employee_IdExistsActionFilter]
        public IActionResult Delete(int id)
        {
            // Se elimina un registro.
            new EmployeeSC().DeleteEmployee(id);

            return NoContent();
        }
    }
}
