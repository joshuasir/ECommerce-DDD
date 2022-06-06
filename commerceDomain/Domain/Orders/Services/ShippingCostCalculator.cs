using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain.Orders.Services
{
    public class ShippingCostCalculator
    {

        private IEnumerable<int> _weightBand;
        private readonly int _boxWeightInKg;

        public ShippingCostCalculator(IEnumerable<int> weightBand, int boxWeightInKg)
        {
            _weightBand = weightBand;
            _boxWeightInKg = boxWeightInKg;
        }

        public Currency CalculateCostToShip()
        {

            return new Currency(0);
        }
    }
}
