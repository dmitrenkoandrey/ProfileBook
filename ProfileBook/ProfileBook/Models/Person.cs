﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ProfileBook.Models
{
    [Table("Persons")]
    public class Person
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string NickName  { get; set; }
        public string Name { get; set; }
        public string ProfileImage { get; set; }
        public string Description { get; set; }
        public string RegDate  { get; set; }
    }
}
