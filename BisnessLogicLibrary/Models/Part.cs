using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisnessLogicLibrary
{
    public class Part
    {
        public string Title { get; set; }

        public List<Note> Notes { get; set; }

        public Part(string one)
        {
            Title = one;
            Notes = new List<Note>();
        }

    }

    
}
