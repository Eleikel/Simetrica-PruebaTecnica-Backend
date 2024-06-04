using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Common.Exceptions
{
   public class EmailIsNotValid : ExceptionBase
    {
        public EmailIsNotValid() : base(HttpStatusCode.BadRequest)
        {

        }
        public EmailIsNotValid(string email) : base(HttpStatusCode.BadRequest, $"El correo {email} no es válido.")
        {
        }
    }
}
