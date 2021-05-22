using Core.DataAccess;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.BackEnd
{
    public class EmployeeSC : BaseSC
    {
        // Devuelve el número de empleados en la Base de Datos.
        public int CountEmployees()
        {
            return GetAllEmployees().Count();
        }

        // Devuelve el empleado con el ID especificado, o null si no existe.
        public Employee GetEmployeeById(int id)
        {
            return GetAllEmployees().FirstOrDefault(employee => employee.EmployeeId == id);
        }

        // Devuelve el número de la última página dado un número de elementos por página.
        public int CalculateLastPage(int elementsPerPage)
        {
            int totalElements = CountEmployees();
            return BaseSC.CalculateLastPage(totalElements, elementsPerPage);
        }

        // Devuelve los empleados contenidos en una página especificada.
        public IQueryable<Employee> GetPage(int elementsPerPage, int page)
        {
            return BaseSC.GetPage(GetAllEmployees(), elementsPerPage, page);
        }

        // Devuelve un IQueryable con todos los empleados de la tabla.
        public IQueryable<Employee> GetAllEmployees()
        {
            return dbContext.Employees.AsQueryable();
        }

        // Agrega un registro a la tabla de empleados con con la información que recibe del modelo newEmployee.
        // Devuelve el id con el que se registró al empleado.
        public int AddNewEmployee(IAddible<Employee> newEmployee)
        {
            if (newEmployee == null)
                throw new ArgumentNullException(nameof(newEmployee));

            // Genera un objeto Employee (el cual se puede agregar a la Base de Datos) con la información del modelo.
            Employee dataBaseEmployee = newEmployee.GetDataBaseObject();

            // Agrega el registro y guarda los cambios.
            dbContext.Employees.Add(dataBaseEmployee);
            dbContext.SaveChanges();

            return dataBaseEmployee.EmployeeId;
        }

        // Actualiza un registro de la Base de Datos. El registro modificado es el que se indica en el ID.
        // La información que se modifica es la que contiene el modelo modifiedEmployee.
        public void UpdateEmployee(int id, IUpdatable<Employee> modifiedEmployee)
        {
            if (modifiedEmployee == null)
                throw new ArgumentNullException(nameof(modifiedEmployee));

            // Obtiene el empleado especificado de la Base de Datos.
            Employee dataBaseEmployee = GetEmployeeById(id);

            if (dataBaseEmployee == null)
                throw new KeyNotFoundException();

            // Modifica el empleado obtenido con la información que contiene el modelo.
            modifiedEmployee.ModifyDataBaseObject(dataBaseEmployee);

            // Guarda los cambios.
            dbContext.SaveChanges();
        }

        // Elimina el registro de un empleado especificado en el ID.
        public void DeleteEmployee(int id)
        {
            // Obtiene el empleado especificado de la Base de Datos.
            Employee dataBaseEmployee = GetEmployeeById(id);

            if (dataBaseEmployee == null)
                throw new KeyNotFoundException();

            // Elimina el registro y guarda los cambios.
            dbContext.Employees.Remove(dataBaseEmployee);
            dbContext.SaveChanges();
        }
    }
}