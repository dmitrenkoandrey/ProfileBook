using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Models
{
    public interface IPersons
    {
        Guid Id { get; set; }
        string UserName { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string EMail { get; set; }
        string Phone { get; set; }
    }
}
