﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePortal.Domain.ApplicationEnums
{
    public enum Gender
    {
        Male = 0,
        Female = 1,
        Others = 2
    }

    public enum CallType
    {
        Incoming = 0,
        Outgoing = 1,
    }

    public enum Order
    {
        Asc = 0,
        Desc = 1,
    }
}
