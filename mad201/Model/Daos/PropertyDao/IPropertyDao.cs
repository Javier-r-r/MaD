using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Daos.PropertyDao
{
    /// <summary>
    /// Interfaz para la gestión de propiedades en la base de datos.
    /// Extiende la funcionalidad de <see cref="IGenericDao{TEntity, TKey}"/> para la entidad <see cref="Property"/>.
    /// </summary>
    public interface IPropertyDao : IGenericDao<Property, long>
    {
        /// <summary>
        /// Busca una propiedad en la base de datos por su nombre.
        /// </summary>
        /// <param name="name">Nombre de la propiedad a buscar.</param>
        /// <returns>La propiedad encontrada.</returns>
        Property FindByName(string name);

        /// <summary>
        /// Busca todas las propiedades especificas diferentes
        /// </summary>
        /// <returns>La lista de propiedades.</returns>
        List<Property> FindDistinctProperties();

        /// <summary>
        /// Busca todas las propiedades especificas de un producto
        /// </summary>
        /// <returns>La lista de propiedades.</returns>
        List<ProductProperty> FindProductProperties(long productId);
    }
}
