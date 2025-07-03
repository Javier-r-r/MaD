using Es.Udc.DotNet.ModelUtil.Dao;
using Model.Daos.Util;
using System;
using System.Collections.Generic;

namespace Model.Daos.BankCardDao
{
    /// <summary>
    /// Interfaz de acceso a datos para la entidad Bankcard.
    /// Extiende IGenericDao para proporcionar operaciones específicas.
    /// </summary>
    public interface IBankCardDao : IGenericDao<Bankcard, Int64>
    {

        /// <summary>
        /// Obtiene una lista de tarjetas bancarias asociadas a un usuario específico.
        /// </summary>
        /// <param name="ownerLogin"></param>
        /// <returns>The list of bankCards</returns>
        PagedResult<Bankcard> FindByOwner(long clientId, int pageNumber, int pageSize);

        List<Bankcard> FindAllByClientId(long clientId);

    }
}
