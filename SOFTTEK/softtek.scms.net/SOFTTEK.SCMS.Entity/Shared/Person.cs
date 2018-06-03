using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFTTEK.SCMS.Entity.Shared
{
    public class Person
    {
        public int Identifier { get; set; }
        public string Identification { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { 
            get {
                return String.Join(" ", Name, MiddleName, LastName);
            } 
        }
        public DateTime BornIn { get; set; }
        public int Age { 
            get {
                return (new DateTime(1, 1, 1) + (DateTime.Now - BornIn)).Year;
            } 
        }
        public string Gender { get; set; }
        public string From { get; set; }
    }
}
