using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Model.Daos.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Daos.BankCardDao
{
    /// <summary>
    /// Implementación de la interfaz IBankCardDao utilizando Entity Framework.
    /// Proporciona acceso a la base de datos para la entidad Bankcard.
    /// </summary>
    public class BankCardDaoImpl :
        GenericDaoEntityFramework<Bankcard, Int64>, IBankCardDao
    {

        public PagedResult<Bankcard> FindByOwner(long clientId, int pageNumber, int pageSize)
        {
            DbSet<Bankcard> allBankCards = Context.Set<Bankcard>();

            // Ordenar por un campo (por ejemplo, número de tarjeta o ID de la tarjeta)
            var query = from c in allBankCards
                        where c.Client.Id == clientId
                        orderby c.number  // Asegúrate de ordenar los resultados
                        select c;

            int totalItems = query.Count(); // Total de tarjetas bancarias
            var items = query.Skip((pageNumber - 1) * pageSize)  // Paginación
                             .Take(pageSize)
                             .ToList(); // Obtener la página actual de tarjetas

            // Devolver el resultado paginado
            return new PagedResult<Bankcard>(items, totalItems, pageNumber, pageSize);
        }


        public List<Bankcard> FindAllByClientId(long clientId)
        {
            DbSet<Bankcard> allBankCards = Context.Set<Bankcard>();

            var query = from c in allBankCards
                        where c.Client.Id == clientId
                        orderby c.number
                        select c;

            return query.ToList();
        }

    }
}
