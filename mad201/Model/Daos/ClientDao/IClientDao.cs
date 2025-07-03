using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Daos.ClientDao
{
    /// <summary>
    /// Interfaz para la gestión de clientes en la base de datos.
    /// Extiende la funcionalidad de IGenericDao para la entidad Client.
    /// </summary>
    public interface IClientDao : IGenericDao<Client, Int64>
    {
        /// <summary>
        /// Finds  Client by name
        /// </summary>
        /// <param type="name">name</param>
        /// <returns>The client/returns>
        Client FindByClientName(String name);
    }
}