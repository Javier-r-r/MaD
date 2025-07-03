using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace Model.Daos.PropertyDao
{
    /// <summary>
    /// Implementación de la interfaz <see cref="IPropertyDao"/> utilizando Entity Framework.
    /// Proporciona acceso a la base de datos para la entidad <see cref="Property"/>.
    /// </summary>
    public class PropertyDaoImpl : GenericDaoEntityFramework<Property, long>, IPropertyDao
    {
        /// <summary>
        /// Busca una propiedad en la base de datos por su nombre.
        /// </summary>
        /// <param name="name">Nombre de la propiedad a buscar.</param>
        /// <returns>La propiedad encontrada.</returns>
        /// <exception cref="InstanceNotFoundException">Lanzado cuando la propiedad con el nombre especificado no se encuentra en la base de datos.</exception>
        public Property FindByName(string name)
        {
            Property property = null;
            DbSet<Property> properties = Context.Set<Property>();

            var result = (from u in properties where u.name == name select u);

            property = result.FirstOrDefault();

            // Si no se encuentra la propiedad, se lanza una excepción
            if (property == null)
                throw new InstanceNotFoundException(name, typeof(Property).FullName);

            return property;
        }

        public List<Property> FindDistinctProperties()
        {
            DbSet<Property> properties = Context.Set<Property>();

            var distinctProps = properties
                .GroupBy(p => p.name)
                .Select(g => g.FirstOrDefault())
                .ToList();

            return distinctProps;
        }

        public List<ProductProperty> FindProductProperties(long productId)
        {
            DbSet<ProductProperty> properties = Context.Set<ProductProperty>();


            var productProperties = properties
                .Where(p => p.Product.Id == productId)
                .ToList();

            return productProperties;
        }
    }


}
