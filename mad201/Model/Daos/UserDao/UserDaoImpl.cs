
using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Data.Entity;
using System.Linq;

namespace Model.Daos.UserDao
{
    public class UserDaoImpl :
        GenericDaoEntityFramework<User, Int64>, IUserDao
    {
        #region Public Constructors

        /// <summary>
        /// Public Constructor
        /// </summary>
        public UserDaoImpl()
        {
        }

        #endregion Public Constructors

        #region IUserDao Members. Specific Operations

        /// <summary>
        /// Finds a User by his loginName
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public User FindByLoginName(string loginName)
        {
            User user = null;

            DbSet<User> users= Context.Set<User>();

            var result =
                (from u in users
                 where u.login == loginName
                 select u);

            user = result.FirstOrDefault();



            if (user == null)
                throw new InstanceNotFoundException(loginName,
                    typeof(User).FullName);

            return user;
        }

        #endregion IUserProfileDao Members
    }
}
