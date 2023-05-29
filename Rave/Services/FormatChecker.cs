using System;
using System.Collections.Generic;
using System.Text;

namespace Rave.Services
{
    using System;
    using System.Text.RegularExpressions;

    public class FormatChecker
    {
        public bool CheckPhoneNumber(string phoneNumber)
        {
            // Comprueba que el número de teléfono tenga el formato adecuado
            // Puedes personalizar la expresión regular según tus requisitos
            string pattern = "^\\d+$";
            if (phoneNumber != null)
            {
                return Regex.IsMatch(phoneNumber, pattern);
            } else { return false; }
            
        }

        public bool CheckEmail(string email)
        {
            // Comprueba que el email tenga el formato adecuado
            // Puedes personalizar la expresión regular según tus requisitos
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (email != null)
            {
                return Regex.IsMatch(email, pattern);
            }
            else {  return false; }
            
        }

        public bool CheckPassword(string password)
        {
            // Comprueba que la contraseña cumpla con los requisitos
            // Puedes personalizar las reglas de validación según tus necesidades
            if (password.Length < 8)
            {
                return false;
            }

            // Comprueba si contiene al menos una letra mayúscula, una letra minúscula y un dígito
            if (!Regex.IsMatch(password, "[A-Z]") || !Regex.IsMatch(password, "[a-z]") || !Regex.IsMatch(password, "[0-9]"))
            {
                return false;
            }
            return true;
        }

        public bool CheckText(string text)
        {
            // Comprueba que el texto no contenga caracteres prohibidos
            // Puedes personalizar la expresión regular según tus requisitos
            string pattern = @"^[a-zA-Z0-9\s\.,]+$";
            if(text != null)
            {
                return Regex.IsMatch(text, pattern);
            }
            else { return false; }
            
        }
    }
}
