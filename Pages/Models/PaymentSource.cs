﻿// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pages.Models
{
    public enum PaymentSource
    {
        Undefined,
        Financing,
        [Description("Credit/Debit Card")]
        CreditCart,
        [Description("Google Pay")]
        GooglePay,
        PayPal
    }
}
