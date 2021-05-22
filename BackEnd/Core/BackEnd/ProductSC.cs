using Core.DataAccess;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.BackEnd
{
    public class ProductSC : BaseSC
    {
        // Devuelve el número de productos en la Base de Datos.
        public int CountProducts()
        {
            return GetAllProducts().Count();
        }

        // Devuelve el producto con el ID especificado, o null si no existe.
        public Product GetProductById(int id)
        {
            return GetAllProducts().FirstOrDefault(product => product.ProductId == id);
        }

        // Devuelve el número de la última página dado un número de elementos por página.
        public int CalculateLastPage(int elementsPerPage)
        {
            int totalElements = CountProducts();
            return BaseSC.CalculateLastPage(totalElements, elementsPerPage);
        }

        // Devuelve los productos contenidos en una página especificada.
        public IQueryable<Product> GetPage(int elementsPerPage, int page)
        {
            return BaseSC.GetPage(GetAllProducts(), elementsPerPage, page);
        }

        // Devuelve un IQueryable con todos los productos de la tabla.
        public IQueryable<Product> GetAllProducts()
        {
            return dbContext.Products.AsQueryable();
        }

        // Agrega un registro a la tabla de productos con con la información que recibe del modelo newProduct.
        // Devuelve el id con el que se registró al producto.
        public int AddNewProduct(IAddible<Product> newProduct)
        {
            if (newProduct == null)
                throw new ArgumentNullException(nameof(newProduct));

            // Genera un objeto Product (el cual se puede agregar a la Base de Datos) con la información del modelo.
            Product dataBaseProduct = newProduct.GetDataBaseObject();

            // Agrega el registro y guarda los cambios.
            dbContext.Products.Add(dataBaseProduct);
            dbContext.SaveChanges();

            return dataBaseProduct.ProductId;
        }

        // Actualiza un registro de la Base de Datos. El registro modificado es el que se indica en el ID.
        // La información que se modifica es la que contiene el modelo modifiedProduct.
        public void UpdateProduct(int id, IUpdatable<Product> modifiedProduct)
        {
            if (modifiedProduct == null)
                throw new ArgumentNullException(nameof(modifiedProduct));

            // Obtiene el producto especificado de la Base de Datos.
            Product dataBaseProduct = GetProductById(id);

            if (dataBaseProduct == null)
                throw new KeyNotFoundException();

            // Modifica el producto obtenido con la información que contiene el modelo.
            modifiedProduct.ModifyDataBaseObject(dataBaseProduct);

            // Guarda los cambios.
            dbContext.SaveChanges();
        }

        // Elimina el registro de un producto especificado en el ID.
        public void DeleteProduct(int id)
        {
            // Obtiene el producto especificado de la Base de Datos.
            Product dataBaseProduct = GetProductById(id);

            if (dataBaseProduct == null)
                throw new KeyNotFoundException();

            // Elimina el registro y guarda los cambios.
            dbContext.Products.Remove(dataBaseProduct);
            dbContext.SaveChanges();
        }
    }
}