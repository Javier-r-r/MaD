using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services.UserService.DTOs
{
    public class UserDto
    {
        public long userId { get; private set; }
        public String login { get; private set; }
        public String password { get; private set; }
        public String name { get; private set; }
        public String address { get; private set; }
        public String email { get; private set; }
        public String language { get; private set; }
        public String country { get; private set; }
        public String role { get; set; }

        public UserDto(long userId, String login, String password, String name, String address, String email, String language, String country)
        {
            this.userId = userId;
            this.login = login;
            this.password = password;
            this.name = name;
            this.address = address;
            this.email = email;
            this.language = language;
            this.country = country;
        }

        public override bool Equals(object obj)
        {

            UserDto target = (UserDto)obj;

            return (this.userId == target.userId)
                  && (this.login == target.login)
                  && (this.password == target.password)
                  && (this.name == target.name)
                  && (this.address == target.address)
                  && (this.email == target.email)
                  && (this.language == target.language)
                  && (this.country == target.country)
                  && (this.role == target.role);
        }

        public override int GetHashCode()
        {
            return this.userId.GetHashCode();
        }

        public override String ToString()
        {
            String strUserDto;

            strUserDto =
                "[ userId = " + userId + " | " +
                "login = " + login + " | " +
                "password = " + password + " | " +
                "name = " + name + " | " +
                "email = " + email + " | " +
                "address = " + address + " | " +
                "language = " + language + " | " +
                "country = " + country + " | " +
                "role = " + role + " ]";

            return strUserDto;
        }
    }
}
