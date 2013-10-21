using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarWars.Helpers
{
    public class RandomGenerator
    {
        // Mix in the odd weirdness factor
        public int RandomInteger(int max, int min = 0)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

    }
}