using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Common.Exceptions
{
    public class UserAlreadyExistsException : ExceptionBase
    {
        public UserAlreadyExistsException() : base(HttpStatusCode.Conflict)
        {
        }

        public UserAlreadyExistsException(string username) : base(HttpStatusCode.Conflict, $"El Usuario '{username}' ya existe.")
        {
        }
    }
}
