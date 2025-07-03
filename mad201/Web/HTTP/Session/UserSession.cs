using System;
using Model.Services.CartService.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.HTTP.Session
{
    public class UserSession
    {
        private long userProfileId;
        private String firstName;
        private String role;
        

        public UserSession()
        {
        }


        public long UserProfileId
        {
            get { return userProfileId; }
            set { userProfileId = value; }
        }

        public String FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public String Role
        {
            get { return role; }
            set { role = value; }
        }

    }
}