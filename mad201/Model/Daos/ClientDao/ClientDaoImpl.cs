using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Daos.ClientDao
{
    /// <summary>
    /// Implementación de la interfaz IClientDao utilizando Entity Framework.
    /// Proporciona acceso a la base de datos para la entidad Client.
    /// </summary>
    public class ClientDaoImpl :
        GenericDaoEntityFramework<Client, Int64>, IClientDao
    {
        // Métodos específicos para la gestión de clientes pueden añadirse aquí

        public Client FindByClientName(string name)
        {

            DbSet<Client> clients = Context.Set<Client>();

            var result = (from c in clients
                          where c.name == name
                          select c);

            return result.FirstOrDefault();
        }
    }
}
