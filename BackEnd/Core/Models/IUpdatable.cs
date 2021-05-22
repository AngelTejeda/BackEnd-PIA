namespace Core.Models
{
    // Esta interface se utiliza para los modelos de la Base de Datos que se utilizan
    // para actualizar un registro.
    public interface IUpdatable<T> where T: class
    {
        // Coloca la información del Modelo al objeto de la clase de la Base de Datos que recibe.
        public void ModifyDataBaseObject(T dbObject);
    }
}
