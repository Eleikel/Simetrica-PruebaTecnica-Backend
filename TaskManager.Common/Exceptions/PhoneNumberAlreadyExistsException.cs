using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Common.Exceptions
{
    public class PhoneNumberAlreadyExistsException : ExceptionBase
    {
        public PhoneNumberAlreadyExistsException() : base(HttpStatusCode.Conflict)
        {
        }

        public PhoneNumberAlreadyExistsException(string phoneNumber) : base(HttpStatusCode.Conflict, $"El N° tel: '{phoneNumber}' ya existe.")
        {
        }
    }
}
