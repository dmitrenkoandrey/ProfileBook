using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string NickName  { get; set; }
        public string Name { get; set; }
        public string ProfileImage { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public DateTime RegDate  { get; set; }
        public string Email { get; set; }
    }
}
