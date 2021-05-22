using System.Collections.Generic;

namespace Core.Models
{
    // Este modelo se utiliza para paginar los registros de las Tablas de la Base de Datos.
    public class Pagination<T> where T : class
    {
        public int? PreviousPage { get; set; }
        public int? CurrentPage { get; set; }
        public int? NextPage { get; set; }
        public int LastPage { get; set; }
        public List<T> ResponseList { get; set; }


        public Pagination(int page, int lastPage)
        {
            ResponseList = new List<T>();
            LastPage = lastPage;

            // Si no hay registros.
            if (lastPage == 0)
            {
                PreviousPage = null;
                CurrentPage = null;
                NextPage = null;

                return;
            }

            // Si la página solicitada no existe devuele la última página.
            if (page > lastPage)
                page = lastPage;

            // Las páginas anterior y siguiente se calculan con base en la página actual.
            PreviousPage = page - 1;
            CurrentPage = page;
            NextPage = page + 1;

            // Si la página solicitada es la última, no hay página siguiente.
            if (page == lastPage)
                NextPage = null;

            // Si la página solicitada es la primera, no hay página anterior.
            if (page == 1)
                PreviousPage = null;
        }
    }
}
