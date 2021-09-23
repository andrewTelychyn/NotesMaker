using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostMaker
{
    public class AuthorBookModel
    {
        public int Id { get; set; }

        public string AuthorName { get; set; }

        public string BookName { get; set; }

        public DateTime LastUse { get; set; }
    }
}
