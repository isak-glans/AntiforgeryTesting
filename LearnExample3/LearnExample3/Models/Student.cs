using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnExample3.Models
{
    public class Student : Person
    {
        public int Id { get; set; }
        public string SchoolName { get; set; }
    }
}
