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
    public class ProductController : ControllerBase
    {
        // GET api/<ProductController>/{id}
        [HttpGet("{id}")]
        [Product_IdExistsActionFilter]
        public IActionResult Get([PositiveInteger] int id)
        {
            // Obtiene el empleado de la Base de Datos.
            Product dbProduct = new ProductSC().GetProductById(id);
            // Genera un modelo con la información del empleado obtenido.
            ProductBasicInfoDTO product = new(dbProduct);

            return Ok(product);
        }

        // GET: api/<ProductController>/pages/{page}
        [HttpGet]
        [Route("pages/{requestedPage}")]
        public IActionResult GetPage([PositiveInteger] int requestedPage)
        {
            const int elementsPerPage = 10;

            // Cálculo de las Páginas.
            int lastPage = new ProductSC().CalculateLastPage(elementsPerPage);
            Pagination<ProductBasicInfoDTO> response = new(requestedPage, lastPage);

            if (lastPage == 0)
                return Ok(response);

            // Selección de la página solicitada.
            IQueryable<Product> dbProducts = new ProductSC()
                .GetPage(elementsPerPage, (int)response.CurrentPage);

            // Materialización de los registros.
            List<ProductBasicInfoDTO> products = BaseSC
                .MaterializeIQueryable<Product, ProductBasicInfoDTO>(dbProducts);

            // Los registros se añaden a la respuesta.
            response.ResponseList = products;

            return Ok(response);
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult GetAll()
        {
            // Se obtienen todos los registros.
            IQueryable<Product> dbProducts = new ProductSC().GetAllProducts();

            // Se materialzian.
            List<ProductBasicInfoDTO> products = BaseSC
                .MaterializeIQueryable<Product, ProductBasicInfoDTO>(dbProducts);

            return Ok(products);
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post([FromBody] ProductBasicInfoPostDTO newProduct)
        {
            // Se añade un registro y se obtiene el id con el que se registró.
            int id = new ProductSC().AddNewProduct(newProduct);

            return Created("GET " + Request.Path.Value + "/" + id, new { Id = id });
        }

        // PUT api/<ProductController>/{id}
        [HttpPut("{id}")]
        [Product_IdExistsActionFilter]
        public IActionResult Put([PositiveInteger] int id, [FromBody] ProductBasicInfoPutDTO modifiedProduct)
        {
            // Se actualiza un registro.
            new ProductSC().UpdateProduct(id, modifiedProduct);

            return NoContent();
        }

        // DELETE api/<ProductController>/{id}
        [HttpDelete("{id}")]
        [Product_IdExistsActionFilter]
        public IActionResult Delete([PositiveInteger] int id)
        {
            // Se elimina un registro.
            new ProductSC().DeleteProduct(id);

            return NoContent();
        }
    }
}
