namespace Core.Models
{
    // Esta interface se utiliza para los modelos de la Base de Datos que se utilizan
    // para dar de alta un registro.
    public interface IAddible<T> where T: class
    {
        // Genera un objeto de la Base de Datos con la información del Modelo.
        public abstract T GetDataBaseObject();
    }
}
