using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BisnessLogicLibrary
{
    public class Note
    {
        public string Content { get; set; }

        public Note(string one)
        {
            Content = one;
        }

    }
}
