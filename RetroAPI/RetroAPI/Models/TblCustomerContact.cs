using System;
using System.Collections.Generic;

namespace RetroAPI.Models
{
    public partial class TblCustomerContact
    {
        public string UserName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public int? ContactNo { get; set; }

        public virtual TblRegisterInfo EmailAddressNavigation { get; set; } = null!;
    }
}
