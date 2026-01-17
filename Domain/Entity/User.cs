using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class User
    {
        public required Guid Id { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public int? Age { 
            get 
            { 
                return _age; 
            }  
            set 
            { 
                if (value < 0) throw new InvalidAgeExceprion();
                _age = value ?? 1;
            } 
        }
        private int _age;
    }
}
