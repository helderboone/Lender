﻿using Lender.API.Models.Base;

namespace Lender.API.Models
{
    public class Address : Entity
    {
        public string Number { get; set; }

        public string Street { get; set; }

        public string Neighborhood { get; set; }

        public string City { get; set; }

        public long FriendId { get; set; }

        public Friend Friend { get; set; }
    }
}
