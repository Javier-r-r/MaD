using Es.Udc.DotNet.ModelUtil.IoC;
using Model;
using Model.Daos.Util;
using Model.Services.ClientService;
using Model.Services.RestaurantService;
using Model.Services.UserService;
using Model.Services.UserService.DTOs;
using Model.Services.CartService.DTOs;
using Model.Services.CartService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Web.HTTP.Util;
using Web.HTTP.View.ApplicationObjects;

namespace Web.HTTP.Session
{
    public class SessionManager
    {
        public static readonly String LOCALE_SESSION_ATTRIBUTE = "locale";

        public static readonly String USER_SESSION_ATTRIBUTE =
               "userSession";

        public static readonly String CART_SESSION_ATTRIBUTE = "sessionCart";


        private static IUserService userService;
        private static IClientService clientService;
        private static IRestaurantService restaurantService;
        private static ICartService cartService;

        public IUserService UserService
        {
            set { userService = value; }
        }

        public IClientService ClientService
        {
            set { clientService = value; }
        }
        public IRestaurantService RestaurantService
        {
            set { restaurantService = value; }
        }

        public ICartService CartService
        {
            set { cartService = value; }
        }


        static SessionManager()
        {
            IIoCManager iocManager =
                (IIoCManager)HttpContext.Current.Application["managerIoC"];

            userService = iocManager.Resolve<IUserService>();
            clientService = iocManager.Resolve<IClientService>();
            restaurantService = iocManager.Resolve<IRestaurantService>();
            cartService = iocManager.Resolve<ICartService>();
        }

        public static Cart GetCart(HttpContext context)
        {
            if (context.Session[CART_SESSION_ATTRIBUTE] == null)
            {
                context.Session[CART_SESSION_ATTRIBUTE] = new Cart();
            }

            return (Cart)context.Session[CART_SESSION_ATTRIBUTE];
        }

        public static void ClearCart(HttpContext context)
        {
            var cart = GetCart(context);
            if (cart != null)
            {
                cart.cart.Clear();

            }
        }

                /// <summary>
                /// Registers the user.
                /// </summary>
                /// <param name="context">Http Context includes request, response, etc.</param>
                /// <param name="loginName">Username</param>
                /// <param name="clearPassword">Password in clear text</param>
                /// <param name="userProfileDetails">The user profile details.</param>
                /// <exception cref="DuplicateInstanceException"/>
                public static void RegisterUser(HttpContext context,
            String loginName, String clearPassword,
            UserSummaryDto userDto, String surname)
        {

            long usrId = clientService.RegisterClient(
                loginName,
                clearPassword,
                userDto.name,
                userDto.address,
                userDto.email,
                surname,
                userDto.language,
                userDto.country);

            /* Insert necessary objects in the session. */
            UserSession userSession = new UserSession();
            userSession.UserProfileId = usrId;
            userSession.FirstName = userDto.name;
            userSession.Role = userDto.role;

            Locale locale = new Locale(userDto.language,
                userDto.country);

            UpdateSessionForAuthenticatedUser(context, userSession, locale);

            FormsAuthentication.SetAuthCookie(loginName, false);
        }

        /// <summary>
        /// Registers the restaurant.
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="loginName">Username</param>
        /// <param name="clearPassword">Password in clear text</param>
        /// <param name="userProfileDetails">The user profile details.</param>
        /// <exception cref="DuplicateInstanceException"/>
        public static void RegisterRestaurant(HttpContext context,
            String loginName, String clearPassword,
            UserSummaryDto userDto, String schedule, String restaurantType)
        {

            long usrId = restaurantService.RegisterRestaurant(
                loginName,
                clearPassword,
                userDto.name,
                userDto.address,
                userDto.email,
                schedule,
                restaurantType,
                userDto.language,
                userDto.country);

            /* Insert necessary objects in the session. */
            UserSession userSession = new UserSession();
            userSession.UserProfileId = usrId;
            userSession.FirstName = userDto.name;
            userSession.Role = userDto.role;

            Locale locale = new Locale(userDto.language,
                userDto.country);

            UpdateSessionForAuthenticatedUser(context, userSession, locale);

            FormsAuthentication.SetAuthCookie(loginName, false);
        }

        /// <summary>
        /// Login method. Authenticates an user in the current context.
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="loginName">Username</param>
        /// <param name="clearPassword">Password in clear text</param>
        /// <param name="rememberMyPassword">Remember password to the next logins</param>
        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        public static void Login(HttpContext context, String loginName,
           String clearPassword, Boolean rememberMyPassword)
        {
            /* Try to login, and if successful, update session with the necessary
             * objects for an authenticated user. */
            UserDto loginResult = DoLogin(context, loginName,
                clearPassword, false, rememberMyPassword);

            /* Add cookies if requested. */
            if (rememberMyPassword)
            {
                CookiesManager.LeaveCookies(context, loginName,
                    loginResult.password);
            }
        }

        /// <summary>
        /// Tries to log in with the corresponding method of
        /// <c>UserService</c>, and if successful, inserts in the
        /// session the necessary objects for an authenticated user.
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="loginName">Username</param>
        /// <param name="password">User Password</param>
        /// <param name="passwordIsEncrypted">Password is either encrypted or
        /// in clear text</param>
        /// <param name="rememberMyPassword">Remember password to the next
        /// logins</param>
        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        private static UserDto DoLogin(HttpContext context,
             String loginName, String password, Boolean passwordIsEncrypted,
             Boolean rememberMyPassword)
        {
            UserDto user =
                userService.Login(loginName, password,
                    passwordIsEncrypted);

            /* Insert necessary objects in the session. */

            UserSession userSession = new UserSession();
            userSession.UserProfileId = user.userId;
            userSession.FirstName = user.name;
            userSession.Role = user.role;

            Locale locale =
                new Locale(user.language, user.country);

            UpdateSessionForAuthenticatedUser(context, userSession, locale);

            return user;
        }

        /// <summary>
        /// Updates the session values for an previously authenticated user
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="userSession">The user data stored in session.</param>
        /// <param name="locale">The locale info.</param>
        private static void UpdateSessionForAuthenticatedUser(
            HttpContext context, UserSession userSession, Locale locale)
        {
            /* Insert objects in session. */
            context.Session.Add(USER_SESSION_ATTRIBUTE, userSession);
            context.Session.Add(LOCALE_SESSION_ATTRIBUTE, locale);
        }

        /// <summary>
        /// Determine if a user is authenticated
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <returns>
        /// 	<c>true</c> if is user authenticated
        ///     <c>false</c> otherwise
        /// </returns>
        public static Boolean IsUserAuthenticated(HttpContext context)
        {
            if (context.Session == null)
                return false;

            return (context.Session[USER_SESSION_ATTRIBUTE] != null);
        }

        public static Locale GetLocale(HttpContext context)
        {
            Locale locale =
                (Locale)context.Session[LOCALE_SESSION_ATTRIBUTE];

            return locale;
        }

        /// <summary>
        /// Updates the user profile details.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="userProfileDetails">The user profile details.</param>
        public static void UpdateUserProfileDetails(HttpContext context,
            UserDto userProfileDetails)
        {
            /* Update user's profile details. */

            UserSession userSession =
                (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            UserSummaryDto userSummaryDto = new UserSummaryDto(
                userProfileDetails.login,
                userProfileDetails.name,
                userProfileDetails.address,
                userProfileDetails.email,
                userProfileDetails.language,
                userProfileDetails.country);

            userService.UpdateUserProfileDetails(userSession.UserProfileId,
                userSummaryDto);

            /* Update user's session objects. */

            Locale locale = new Locale(userProfileDetails.language,
                userProfileDetails.country);

            userSession.FirstName = userProfileDetails.name;

            UpdateSessionForAuthenticatedUser(context, userSession, locale);
        }

        /// <summary>
        /// Finds the user profile with the id stored in the session.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static UserSummaryDto FindUserProfileDetails(HttpContext context)
        {
            UserSession userSession =
                (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            UserSummaryDto userProfileDetails =
                userService.FindUserProfileDetails(userSession.UserProfileId);

            return userProfileDetails;
        }

        /// <summary>
        /// Gets the user info stored in the session.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static UserSession GetUserSession(HttpContext context)
        {
            if (IsUserAuthenticated(context))
                return (UserSession)context.Session[USER_SESSION_ATTRIBUTE];
            else
                return null;
        }

        /// <summary>
        /// Changes the user's password
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        /// <param name="oldClearPassword">The old password in clear text</param>
        /// <param name="newClearPassword">The new password in clear text</param>
        /// <exception cref="IncorrectPasswordException"/>
        public static void ChangePassword(HttpContext context,
               String oldClearPassword, String newClearPassword)
        {
            UserSession userSession =
                (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            userService.ChangePassword(userSession.UserProfileId,
                oldClearPassword, newClearPassword);

            /* Remove cookies. */
            CookiesManager.RemoveCookies(context);
        }

        /// <summary>
        /// Destroys the session, and removes the cookies if the user had
        /// selected "remember my password".
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        public static void Logout(HttpContext context)
        {
            /* Remove cookies. */
            CookiesManager.RemoveCookies(context);

            context.Session.Remove(CART_SESSION_ATTRIBUTE);


            /* Invalidate session. */
            context.Session.Abandon();

            /* Invalidate Authentication Ticket */
            FormsAuthentication.SignOut();
        }

        /// <sumary>
        /// Guarantees that the session will have the necessary objects if the
        /// user has been authenticated or had selected "remember my password"
        /// in the past.
        /// </sumary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        public static void TouchSession(HttpContext context)
        {
            /* Check if "UserSession" object is in the session. */
            UserSession userSession = null;

            if (context.Session != null)
            {
                userSession =
                    (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

                // If userSession object is in the session, nothing should be doing.
                if (userSession != null)
                {
                    return;
                }
            }

            /*
             * The user had not been authenticated or his/her session has
             * expired. We need to check if the user has selected "remember my
             * password" in the last login (login name and password will come
             * as cookies). If so, we reconstruct user's session objects.
             */
            UpdateSessionFromCookies(context);
        }

        /// <summary>
        /// Tries to login (inserting necessary objects in the session) by using
        /// cookies (if present).
        /// </summary>
        /// <param name="context">Http Context includes request, response, etc.</param>
        private static void UpdateSessionFromCookies(HttpContext context)
        {
            HttpRequest request = context.Request;
            if (request.Cookies == null)
            {
                return;
            }

            /*
             * Check if the login name and the encrypted password come as
             * cookies.
             */
            String loginName = CookiesManager.GetLoginName(context);
            String encryptedPassword = CookiesManager.GetEncryptedPassword(context);

            if ((loginName == null) || (encryptedPassword == null))
            {
                return;
            }

            /* If loginName and encryptedPassword have valid values (the user selected "remember
             * my password" option) try to login, and if successful, update session with the
             * necessary objects for an authenticated user.
             */
            try
            {
                DoLogin(context, loginName, encryptedPassword, true, true);

                /* Authentication Ticket. */
                FormsAuthentication.SetAuthCookie(loginName, true);
            }
            catch (Exception)
            { // Incorrect loginName or encryptedPassword
                return;
            }
        }

        // RestaurantService methods

        public static PagedResult<Restaurant> GetAllRestaurants(int page, int pageSize)
        {
            return restaurantService.listRestaurants(page, pageSize);
        }
        public static List<string> GetAllRestaurantsTypes()
        {
            return restaurantService.getRestaurantTypes();
        }

        public static PagedResult<Restaurant> GetRestaurantsFilterByType(string type, int page, int pageSize)
        {
            return restaurantService.listRestaurantsFilterByType(type, page, pageSize);
        }

        public static PagedResult<Product> GetAllRestaurantProducts(long restaurantId, int pageNumber, int pageSize)
        {
            return restaurantService.listRestaurantProducts(restaurantId, pageNumber, pageSize);
        }

        public static PagedResult<Product> GetRestaurantProductsFilterByCategoryAndKeywords(long categoryId, string keywords, long restaurantId, int pageNumber, int pageSize)
        {
            return restaurantService.listRestaurantProductsFilterByCategoryAndKeywords(categoryId, keywords, restaurantId, pageNumber, pageSize);
        }
        public static Restaurant FindRestaurant(HttpContext context)
        {
            UserSession userSession =
                (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            return restaurantService.FindRestaurantById(userSession.UserProfileId);
        }
        public static void UpdateRestaurantProfile(HttpContext context, long userId, string name, string email, string schedule, string type, string language, string country)
        {
            UserSession userSession =
                (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            restaurantService.UpdateRestaurantProfile(userId, name, email, schedule, type, language, country);

            Locale locale = new Locale(language,
                country);

            userSession.FirstName = name;

            UpdateSessionForAuthenticatedUser(context, userSession, locale);
        }

        public static Product FindProduct(long productId)
        {
            return restaurantService.FindProduct(productId);
        }

        public static void RegisterProduct(Product product , List<ProductProperty> properties, long restaurantId)
        {
            restaurantService.AddProduct(restaurantId, product.name, product.price, DateTime.Now, product.stock, product.Category.categoryName, properties);
        }

        public static void UpdateProduct(Product product, string categoryName, List<ProductProperty> properties)
        {
            restaurantService.UpdateProduct(product.Id, product.name, product.price, DateTime.Now, product.stock, categoryName, properties);
        }

        public static void DeleteProduct(long productId)
        {
            restaurantService.DeleteProduct(productId);
        }

        public static List<Category> GetAllCategories()
        {
            return restaurantService.FindAllCategories();
        }

        public static List<Property> GetAllSpecificProperties()
        {
            return restaurantService.FindAllSpecificProperties();
        }
        public static void RegisterCategory(String category)
        {
            restaurantService.AddProductCategory(category);
        }
        
        public static void RemoveProperty(long propertyId)
        {
            restaurantService.RemoveProperty(propertyId);
        }


        // ClientService methods

        public static Client FindClient(HttpContext context)
        {
            UserSession userSession =
                (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            return clientService.FindClientById(userSession.UserProfileId);
        }
               

        public static void UpdateClientProfile(HttpContext context, long userId, string name, string surname, string email, string language, string country)
        {
            UserSession userSession =
               (UserSession)context.Session[USER_SESSION_ATTRIBUTE];

            clientService.UpdateClientProfile(userId, name, surname, email, language, country);
            Locale locale = new Locale(language,
                country);

            userSession.FirstName = name;

            UpdateSessionForAuthenticatedUser(context, userSession, locale);

        }

        

        public static PagedResult<BankCardDetails> GetClientBankcards(long clientId, int pageNumber, int pageSize)
        {
            return clientService.FindBankCardsByClientId(clientId, pageNumber, pageSize);
        }

        public static List<BankCardDetails> GetAllClientBankcards(long clientId)
        {
            return clientService.FindAllBankCardsByClientId(clientId);
        }



        public static void AddBankcard(long clientId, string creditType, long bankCardNumber, string name, int cvv, DateTime expirationDate)
        {
            clientService.AddBankCard(clientId, creditType, bankCardNumber, name, cvv, expirationDate);
        }

        public static void RemoveBankCard(long bankcardId)
        {
            clientService.RemoveBankcard(bankcardId);
        }

        public static void SetDefaultBankcard(long bankcardId)
        {
            clientService.SetBankCardAsDefault(bankcardId);
        }

        public static Bankcard GetBankcardData(long bankcardId)
        {
           return clientService.FindBankcardById(bankcardId);
        }

        public static void UpdateBankcard(long bankcardId, string creditType, long bankCardNumber, string name, int cvv, DateTime expirationDate)
        {
            clientService.UpdateBankcard(bankcardId, creditType, bankCardNumber, name, cvv, expirationDate);
        }


        public static void Purchase(long clientId, string address, long bankard, List<CartLineDto> cartLines)
        {
            cartService.AddOrder(clientId, address, bankard, cartLines);
        }

        public static PagedResult<Order> GetClientOrders(long clientId, int pageNumber, int pageSize)
        {
            return cartService.ViewClientOrders(clientId, pageNumber, pageSize);
        }
    }
}
