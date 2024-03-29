﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain
{
    public class Address : ValueObject<Address>
    {
        public string street { get; private set; }
        public string postCode { get; private set; }
        public string city { get; private set; }

        public Address(string street, string postCode, string city)
        {
            this.street = street;
            this.postCode = postCode;
            this.city = city;
        }
        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<Object> { street, postCode, city };
        }

        public string toString()
        {
            return street + ", " + city + ", " + postCode;
        }


    }
}
