using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab10OAiP.Models
{
        public class Student
        {
            public int Id { get; set; }

            public string FirstName { get; set; } = "";

            public string LastName { get; set; } = "";

            public DateTime BirthDate { get; set; }

            public string Email { get; set; } = "";

            public string PasswordHash { get; set; } = "";

            public string PhoneNumber { get; set; } = "";

            public string Faculty { get; set; } = "";

            public string Group { get; set; } = "";
            public int Course { get; set; }
            public DateTime EnrollmentDate { get; set; }
        }
}
