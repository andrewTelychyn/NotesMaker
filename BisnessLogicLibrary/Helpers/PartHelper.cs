using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisnessLogicLibrary
{
    public class PartHelper
    {
        public Part part;

        public PartHelper(Part one)
        {
            part = one;
        }

        public string Build()
        {
            string outcome = "";

            if (!String.IsNullOrEmpty(part.Title))
            {
                outcome += part.Title + "\n";
            }
            foreach (Note n in part.Notes)
            {
                outcome += "\u2014 " + n.Content + "\n";
            }
            return outcome += "\n";
        }
    }
}
