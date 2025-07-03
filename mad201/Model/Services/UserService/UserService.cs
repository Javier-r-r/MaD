using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Model.Daos.ClientDao;
using Model.Daos.RestaurantDao;
using Model.Daos.UserDao;
using Model.Services.UserService.DTOs;
using Model.Services.UserService.Exceptions;
using Model.Services.UserService.Util;
using Ninject;
using System;

namespace Model.Services.UserService
{
    public class UserService : IUserService
    {
        [Inject]
        public IUserDao UserDao { private get; set; }
        [Inject]
        public IClientDao clientDao { private get; set; }
        [Inject]
        public IRestaurantDao restaurantDao { private get; set; }

        #region IUserService Members

        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void ChangePassword(long userId, string oldClearPassword,
            string newClearPassword)
        {
            User user = UserDao.Find(userId);
            String storedPassword = user.password;

            if (!PasswordEncrypter.IsClearPasswordCorrect(oldClearPassword,
                 storedPassword))
            {
                throw new IncorrectPasswordException(user.login);
            }

            user.password =
            PasswordEncrypter.Crypt(newClearPassword);

            UserDao.Update(user);
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public UserSummaryDto FindUserProfileDetails(long userProfileId)
        {
            User user = UserDao.Find(userProfileId);

            UserSummaryDto userSummaryDto =
                 new UserSummaryDto(user.login,
                     user.name, user.address,
                     user.email, user.language, user.country);

            return userSummaryDto;
        }

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncorrectPasswordException"/>
        [Transactional]
        public UserDto Login(string login, string password, bool passwordIsEncrypted)
        {
            User user =
                UserDao.FindByLoginName(login);

            String storedPassword = user.password;

            if (passwordIsEncrypted)
            {
                if (!password.Equals(storedPassword))
                {
                    throw new IncorrectPasswordException(login);
                }
            }
            else
            {
                if (!PasswordEncrypter.IsClearPasswordCorrect(password,
                        storedPassword))
                {
                    throw new IncorrectPasswordException(login);
                }
            }

            UserDto userDto = new UserDto(user.Id, user.login,
                storedPassword, user.name, user.address, user.email, user.language, user.country);

            if (clientDao.FindByClientName(user.name) != null)
            {
                userDto.role = "Client";    
            }

            if (restaurantDao.FindByRestaurantName(user.name) != null)
            {
                userDto.role = "Restaurant";
            }

            if (clientDao.FindByClientName(user.name) != null)
            {
                userDto.role = "Client";
            }

            return userDto;
        }

        
        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        public long RegisterUser(string login, string clearPassword,
            UserSummaryDto userSummaryDto)
        {
            try
            {
                UserDao.FindByLoginName(login);

                throw new DuplicateInstanceException(login,
                    typeof(User).FullName);
            }
            catch (InstanceNotFoundException)
            {
                String encryptedPassword = PasswordEncrypter.Crypt(clearPassword);

                User user = new User();

                user.login = login;
                user.password = encryptedPassword;
                user.login = userSummaryDto.login;
                user.name = userSummaryDto.name;
                user.address = userSummaryDto.address;
                user.email = userSummaryDto.email;
                user.language = userSummaryDto.language;
                user.country = userSummaryDto.country;

                UserDao.Create(user);

                return user.Id;
            }
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void UpdateUserProfileDetails(long userId,
            UserSummaryDto userSummaryDto)
        {
            User user =
                UserDao.Find(userId);

            user.login = userSummaryDto.login;
            user.name = userSummaryDto.name;
            user.address = userSummaryDto.address;
            user.email = userSummaryDto.email;
            user.language = userSummaryDto.language;
            user.country = userSummaryDto.country;
            UserDao.Update(user);
        }

        public bool UserExists(string login)
        {

            try
            {
                User user = UserDao.FindByLoginName(login);
            }
            catch (InstanceNotFoundException)
            {
                return false;
            }

            return true;
        }

        #endregion IUserService Members
    }
}
