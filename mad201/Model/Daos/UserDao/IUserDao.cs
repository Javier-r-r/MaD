using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Daos.UserDao
{
    public interface IUserDao :  IGenericDao<User, Int64>
    {
        /// <summary>
        /// Finds a User by loginName
        /// </summary>
        /// <param name="loginName">loginName</param>
        /// <returns>The UserProfile</returns>
        /// <exception cref="InstanceNotFoundException"/>
        User FindByLoginName(String loginName);
    }
}
