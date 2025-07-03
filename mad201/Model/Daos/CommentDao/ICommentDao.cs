using Es.Udc.DotNet.ModelUtil.Dao;
using Model.Daos.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Daos.CommentDao
{
    public interface ICommentDao : IGenericDao<Comment, Int64>
    {

        PagedResult<Comment> FindByRestaurantId(long restaurantId, int pageNumber, int pageSize);
    }
}
