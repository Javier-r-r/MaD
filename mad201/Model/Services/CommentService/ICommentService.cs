using Es.Udc.DotNet.ModelUtil.Transactions;
using Model.Daos.CommentDao;
using Model.Services.CommentService.DTOs;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services.CommentService
{
    public interface ICommentService
    {
        [Inject]
        ICommentDao CommentDao { set; }

        [Transactional]
        long addComment(string content, int rate, long clientId, long restaurantId) ;

        [Transactional]
        void deleteComment(long commentId);

        [Transactional]
        List<CommentDto> FindByRestaurantId(long restaurantId, int pageNumber, int pageSize);
        
    }
}
