﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NVisionIT.AutomatedTellerMachine.Service.Models
{
    public enum TransactionType
    {
        Debit = 1,
        Credit,
        Check,
        Default
    }
}
