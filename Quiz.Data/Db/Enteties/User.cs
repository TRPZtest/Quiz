using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Data.Db.Enteties
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        public string  Login { get; set; }
        public string Password { get; set; }
    }
}
