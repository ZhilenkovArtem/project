﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthentificationService
{
    public interface ILogger
    {
        public void WriteEvent(string eventMessage);
        public void WriteError(string errorMessage);
    }
}
