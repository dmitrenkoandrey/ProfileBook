using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Models
{
    public interface IUserLogin
    {
        string UserName { get; set; }
        string Password { get; set; }
    }
}
