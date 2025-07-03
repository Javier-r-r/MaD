using System;
using System.Collections.Generic;

namespace Model.Services.ClientService.DTOs
{
    /// <summary>
    /// Representa un objeto de transferencia de datos (DTO) que contiene la información básica
    /// de un cliente, como el apellido, el idioma y el país.
    /// </summary>
    public class ClientDto
    {
        /// <summary>
        /// Apellido del cliente.
        /// </summary>
        public String surname { get; private set; }

        /// <summary>
        /// Idioma preferido del cliente.
        /// </summary>
        public String language { get; private set; }

        /// <summary>
        /// País del cliente.
        /// </summary>
        public String country { get; private set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ClientDto"/>.
        /// </summary>
        /// <param name="surname">Apellido del cliente.</param>
        /// <param name="language">Idioma preferido del cliente.</param>
        /// <param name="country">País del cliente.</param>
        public ClientDto(String surname, String language, String country)
        {
            this.surname = surname;
            this.language = language;
            this.country = country;
        }

        /// <summary>
        /// Compara el objeto actual con otro objeto para determinar si son iguales.
        /// </summary>
        /// <param name="obj">El objeto con el que comparar.</param>
        /// <returns>Devuelve <c>true</c> si los objetos son iguales, de lo contrario, devuelve <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            ClientDto target = (ClientDto)obj;

            return (this.surname == target.surname)
                  && (this.language == target.language)
                  && (this.country == target.country);
        }



        /// <summary>
        /// Devuelve una representación en cadena del objeto <see cref="ClientDto"/>.
        /// </summary>
        /// <returns>Cadena que representa al cliente con su apellido, idioma y país.</returns>
        public override String ToString()
        {
            String strClientDto;

            strClientDto =
                "surname = " + surname + " | " +
                "language = " + language + " | " +
                "country = " + country + " ]";

            return strClientDto;
        }

        public override int GetHashCode()
        {
            int hashCode = 516055274;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(surname);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(language);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(country);
            return hashCode;
        }
    }
}
