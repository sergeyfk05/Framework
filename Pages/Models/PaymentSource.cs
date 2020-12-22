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
