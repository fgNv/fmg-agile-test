using System;
using System.Linq;

namespace AgileContentTest
{
    public class GreatestSiblingNumberFinder
    {
        public int Find(int input)
        {
            var inputCharacters = input.ToString()
                                       .OrderByDescending(c => c)
                                       .ToArray();
            var result = Int32.Parse(new string(inputCharacters));
            return result <= 100000000 ? result : -1;
        }
    }
}
