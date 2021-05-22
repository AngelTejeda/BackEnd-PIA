using Core.DataAccess;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.BackEnd
{
    public class CustomerSC : BaseSC
    {
        // Devuelve el número de clientes en la Base de Datos.
        public int CountCustomers()
        {
            return GetAllCustomers().Count();
        }

        // Devuelve el cliente con el ID especificado, o null si no existe.
        public Customer GetCustomerById(string id)
        {
            return GetAllCustomers().FirstOrDefault(customer => customer.CustomerId == id);
        }

        // Devuelve el número de la última página dado un número de elementos por página.
        public int CalculateLastPage(int elementsPerPage)
        {
            int totalElements = CountCustomers();
            return BaseSC.CalculateLastPage(totalElements, elementsPerPage);
        }

        // Devuelve los clientes contenidos en una página especificada.
        public IQueryable<Customer> GetPage(int elementsPerPage, int page)
        {
            return BaseSC.GetPage(GetAllCustomers(), elementsPerPage, page);
        }

        // Devuelve un IQueryable con todos los clientes de la tabla.
        public IQueryable<Customer> GetAllCustomers()
        {
            return dbContext.Customers.AsQueryable();
        }

        // Agrega un registro a la tabla de clientes con con la información que recibe del modelo newCustomer.
        // Devuelve el id con el que se registró al cliente.
        public string AddNewCustomer(IAddible<Customer> newCustomer)
        {
            if (newCustomer == null)
                throw new ArgumentNullException(nameof(newCustomer));

            // Genera un objeto Customer (el cual se puede agregar a la Base de Datos) con la información del modelo.
            Customer dataBaseCustomer = newCustomer.GetDataBaseObject();

            // Agrega el registro y guarda los cambios.
            dbContext.Customers.Add(dataBaseCustomer);
            dbContext.SaveChanges();

            return dataBaseCustomer.CustomerId;
        }

        // Actualiza un registro de la Base de Datos. El registro modificado es el que se indica en el ID.
        // La información que se modifica es la que contiene el modelo modifiedCustomer.
        public void UpdateCustomer(string id, IUpdatable<Customer> modifiedCustomer)
        {
            if (modifiedCustomer == null)
                throw new ArgumentNullException(nameof(modifiedCustomer));

            // Obtiene el cliente especificado de la Base de Datos.
            Customer dataBaseCustomer = GetCustomerById(id);    

            if (dataBaseCustomer == null)
                throw new KeyNotFoundException();

            // Modifica el cliente obtenido con la información que contiene el modelo.
            modifiedCustomer.ModifyDataBaseObject(dataBaseCustomer);

            // Guarda los cambios.
            dbContext.SaveChanges();
        }

        // Elimina el registro de un cliente especificado en el ID.
        public void DeleteCustomer(string id)
        {
            // Obtiene el cliente especificado de la Base de Datos.
            Customer dataBaseCustomer = GetCustomerById(id);

            if (dataBaseCustomer == null)
                throw new KeyNotFoundException();

            // Elimina el registro y guarda los cambios.
            dbContext.Customers.Remove(dataBaseCustomer);
            dbContext.SaveChanges();
        }
    }
}