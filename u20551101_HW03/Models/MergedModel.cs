using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u20551101_HW03.Models
{
	public class MergedModel()
	{

        public IEnumerable<u20551101_HW03.Models.authors> authors { get; set; }
        public IEnumerable<u20551101_HW03.Models.books> books { get; set; }
        public IEnumerable<u20551101_HW03.Models.borrows> borrows { get; set; }
        public IEnumerable<u20551101_HW03.Models.students> students { get; set; }
        public IEnumerable<u20551101_HW03.Models.types> types { get; set; }

     }
}
