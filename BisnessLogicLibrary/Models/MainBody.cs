using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisnessLogicLibrary
{
    public class MainBody
    {
        public string Name { get; set; }

        public string Author { get; set; }

        public string Part { get; set; }

        public MainBody(string one, string two, string three)
        {
            Name = one;
            Author = two;
            Part = three;
        }
    }
}
