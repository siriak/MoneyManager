using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class Credentials
    {
        public Credentials(string cardNumber, string merchantPassword, string merchantId)
        {
            CardNumber = cardNumber;
            MerchantPassword = merchantPassword;
            MerchantId = merchantId;
        }

        public string CardNumber { get; set; }
        public string MerchantPassword { get; set; }
        public string MerchantId { get; set; }
    }
}
