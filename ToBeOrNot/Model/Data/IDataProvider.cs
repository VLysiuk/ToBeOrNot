﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToBeOrNot.Model.Data
{
    public interface IDataProvider
    {
        void SetupDatabase();
        void CleanUpDatabase();
    }
}