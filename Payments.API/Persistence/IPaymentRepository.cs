﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payments.API.Persistence
{
    public interface IPaymentRepository
    {
        Task SavePaymentRecordAsync(PaymentRecord record);

        Task<bool> IsExternalIDExistAsync(string externalID);

        Task<PaymentRecord> GetPaymentRecordAsync(string externalID);
    }
}
