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
    public class CustomerController : ControllerBase
    {
        // GET api/<CustomerController>/{id}
        [HttpGet("{id}")]
        [Customer_IdExistsActionFilter]
        public IActionResult Get(string id)
        {
            // Obtiene el empleado de la Base de Datos.
            Customer dbCustomer = new CustomerSC().GetCustomerById(id);
            // Genera un modelo con la información del empleado obtenido.
            CustomerContactInfoDTO customer = new(dbCustomer);

            return Ok(customer);
        }

        // GET: api/<CustomerController>/pages/{page}
        [HttpGet]
        [Route("pages/{requestedPage}")]
        public IActionResult GetPage([PositiveInteger] int requestedPage)
        {
            const int elementsPerPage = 10;

            // Cálculo de las Páginas.
            int lastPage = new CustomerSC().CalculateLastPage(elementsPerPage);
            Pagination<CustomerContactInfoDTO> response = new(requestedPage, lastPage);

            if (lastPage == 0)
                return Ok(response);

            // Selección de la página solicitada.
            IQueryable<Customer> dbCustomers = new CustomerSC()
                .GetPage(elementsPerPage, (int)response.CurrentPage);

            // Materialización de los registros.
            List<CustomerContactInfoDTO> customers = BaseSC
                .MaterializeIQueryable<Customer, CustomerContactInfoDTO>(dbCustomers);

            // Los registros se añaden a la respuesta.
            response.ResponseList = customers;

            return Ok(response);
        }

        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult GetAll()
        {
            // Se obtienen todos los registros.
            IQueryable<Customer> dbCustomers = new CustomerSC().GetAllCustomers();

            // Se materialzian.
            List<CustomerContactInfoDTO> customers = BaseSC
                .MaterializeIQueryable<Customer, CustomerContactInfoDTO>(dbCustomers);

            return Ok(customers);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult Post([FromBody] CustomerContactInfoPostDTO newCustomer)
        {
            // Se añade un registro y se obtiene el id con el que se registró.
            string id = new CustomerSC().AddNewCustomer(newCustomer);

            return Created("GET " + Request.Path.Value + "/" + id, new { Id = id });
        }

        // PUT api/<CustomerController>/{id}
        [HttpPut("{id}")]
        [Customer_IdExistsActionFilter]
        public IActionResult Put(string id, [FromBody] CustomerContactInfoPutDTO modifiedCustomer)
        {
            // Se actualiza un registro.
            new CustomerSC().UpdateCustomer(id, modifiedCustomer);

            return NoContent();
        }

        // DELETE api/<CustomerController>/{id}
        [HttpDelete("{id}")]
        [Customer_IdExistsActionFilter]
        public IActionResult Delete(string id)
        {
            // Se elimina un registro.
            new CustomerSC().DeleteCustomer(id);

            return NoContent();
        }
    }
}
