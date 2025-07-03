using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Services.UserService.DTOs
{
    public class UserSummaryDto
    {

        public String login { get; private set; }
        public String name { get; private set; }
        public String address { get; private set; }
        public String email { get; private set; }
        public String language { get; private set; }
        public String country { get; private set; }
        public String role { get;  set; }
        public UserSummaryDto(String login, String name, String address, String email, String language, String country)
        {
            this.login = login;
            this.name = name;
            this.address = address;
            this.email = email;
            this.language = language;
            this.country = country;
        }
        public UserSummaryDto(String login, String name, String address, String email, String language, String country, String role)
        {
            this.login = login;
            this.name = name;
            this.address = address;
            this.email = email;
            this.language = language;
            this.country = country;
            this.role = role;
        }

        public override bool Equals(object obj)
        {

            UserSummaryDto target = (UserSummaryDto)obj;

            return (this.login == target.login)
                  && (this.name == target.name)
                  && (this.address == target.address)
                  && (this.email == target.email)
                  && (this.language == target.language)
                  && (this.country == target.country);
        }

        public override int GetHashCode()
        {
            return this.login.GetHashCode();
        }

        public override String ToString()
        {
            String strUserSummaryDto;

            strUserSummaryDto =
                "[ login = " + login + " | " +
                "name = " + name + " | " +
                "email = " + email + " | " +
                "address = " + address + " | " +
                "language = " + language + " | " +
                "country = " + country + " ]";

            return strUserSummaryDto;
        }
    }
}
