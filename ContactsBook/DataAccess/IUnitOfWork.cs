﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsBook.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
