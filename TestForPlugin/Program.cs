using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestForPlugin
{
    class Program
    {
        static void Main(string[] args)
        {
            string retString = "CDATA[nbf+isCUcgz1DgfSC7cr7U2aBGvxCZLvpotAz7UTaWI3c+sgdT9YkE9N9jnR8b0pf+q+PPJkmY34hKB8nb7LQ+PPThA4kjg08Y+if+j8TY+AXpNxkwb1ttez6Qe8OG8GP/1SosmYYwLmxu993KNW/ZhW23N7opai9Z9QmX8/r3Q69anZcQpxfe3w4NiUW7Cr]]></";
            string regString = "CDATA\\[(?<value>(.|\n)*?)\\]";
            Match match = Regex.Match(retString, regString);
        }
    }
}
