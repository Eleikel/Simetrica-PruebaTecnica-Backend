﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Common.Exceptions
{
    public class NotFoundException : ExceptionBase
    {
        public NotFoundException() : base(HttpStatusCode.NotFound)
        {

        }
        public NotFoundException(string message) : base(HttpStatusCode.NotFound, $"{message} no se encuentra o no existe.")
        {
        }
    }
}
