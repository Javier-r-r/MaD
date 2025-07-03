using Es.Udc.DotNet.ModelUtil.Dao;
using Model.Daos.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Daos.CommentDao
{
    public class CommentDaoImpl : GenericDaoEntityFramework<Comment, Int64>, ICommentDao
    {
        public PagedResult<Comment> FindByRestaurantId(long restaurantId, int pageNumber, int pageSize)
        {
            DbSet<Comment> allComments = Context.Set<Comment>();

            var query = (from c in allComments where c.Restaurant.Id == restaurantId select c);

            return query.ToPagedList(pageNumber, pageSize);
        }
    }
}
