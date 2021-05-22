using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Core.DataAccess;
using Core.Models;

namespace Core.BackEnd
{
    public class BaseSC
    {
        protected NorthwindContext dbContext = new();
        
        // Este es un método genérico que materializa un IQueryable de alguna clase (DbClass) de las tablas de la Base de Datos
        // en una lista del modelo indicado. El modelo debe implementar la interface IReadable con la misma clase que el IQueryable.
        public static List<Model> MaterializeIQueryable<DbClass, Model>(IQueryable<DbClass> dataBaseIQueryable)
            where Model : IReadable<DbClass>, new()
            where DbClass : class
        {
            List<Model> modelList = new();  // Lista de de los objetos materializados.

            dataBaseIQueryable = dataBaseIQueryable.AsNoTracking();

            foreach (DbClass dataBaseObject in dataBaseIQueryable)
            {
                
                Model model = new();                                // Modelo vacío.
                model.CopyInfoFromDataBaseObject(dataBaseObject);   // Se copia la información del objeto al modelo.

                modelList.Add(model);                               // Se añade el modelo a la lista.
            };

            return modelList;
        }

        // Calcula cuál es el número de la última página dado un total de elementos y el número de elementos por página.
        protected static int CalculateLastPage(int totalElements, int elementsPerPage)
        {
            return Convert.ToInt32(Math.Ceiling((double)totalElements / elementsPerPage));
        }

        // Regresa un IQueryable con un número determinado de elementos.
        // Estos elementos dependen del número de página que se indique.
        protected static IQueryable<T> GetPage<T>(IQueryable<T> elementsQuery, int elementsPerPage, int page)
        {
            if (elementsPerPage <= 0)
                throw new ArgumentOutOfRangeException(nameof(elementsPerPage));

            if (page <= 0)
                throw new ArgumentOutOfRangeException(nameof(page));

            return elementsQuery
                .Skip((page - 1) * elementsPerPage)
                .Take(elementsPerPage);
        }
    }
}
