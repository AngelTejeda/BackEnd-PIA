namespace Core.Models
{
    // Esta interface se utiliza para los modelos de la Base de Datos que se utilizan
    // para mostrar información al usuario.
    public interface IReadable<T> where T: class
    {
        // Copia al modelo la información del objeto de la Base de Datos que recibe como parámetro.
        public void CopyInfoFromDataBaseObject(T dbObject);
    }
}
