using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Enums;

namespace DataAccessLayer.Entities
{
    public abstract class User
    {
        public int Id { get; set; }
        public Sex Sex { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BDay { get; set; }
        public int Telephone { get; set; }
        public string Address { get; set; }
        public string ImgPath { get; set; }
    }
}
