using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Model.Daos.ClientDao;
using Model.Daos.CommentDao;
using Model.Daos.RestaurantDao;
using Model.Services.CommentService.DTOs;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services.CommentService
{
    public class CommentService : ICommentService
    {
        [Inject]
        public ICommentDao CommentDao { private get; set; }

        [Inject]
        public IClientDao ClientDao { private get; set; }

        [Inject]
        public IRestaurantDao RestaurantDao { private get; set; }

        [Transactional]
        public long addComment(string content, int rate, long clientId, long restaurantId)
        {

            Client client = ClientDao.Find(clientId);
            Restaurant restaurant = RestaurantDao.Find(restaurantId);
            var comment = new Comment();

            if (client == null)
                throw new InstanceNotFoundException(clientId, "Cliente");

            if (restaurant == null)
                throw new InstanceNotFoundException(restaurantId, "restaurant");

            comment.content = content;
            comment.rate = rate;
            comment.date = DateTime.Now;
            comment.Client = client;
            comment.Restaurant = restaurant;

            CommentDao.Create(comment);

            return comment.Id;
        }

        public void deleteComment(long commentId)
        {
            CommentDao.Remove(commentId);
        }

        public List<CommentDto> FindByRestaurantId(long restaurantId, int pageNumber, int pageSize)
        {
            if (restaurantId <= 0)
                throw new InstanceNotFoundException(restaurantId, "Restaurant");

            return ConvertToCommentDto(CommentDao.FindByRestaurantId(restaurantId, pageNumber, pageSize).Items);
        }

        private CommentDto FromComment(Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                content = comment.content,
                rate = comment.rate,
                date = comment.date,
                clientId = comment.Client.Id,
                restaurantId = comment.Restaurant.Id
            };
        }

        private List<CommentDto> ConvertToCommentDto(List<Comment> comments)
        {
            return comments.Select(c => FromComment(c)).ToList();
        }
    }
}
