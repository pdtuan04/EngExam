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
        private string _password;

        public string Password {
            get 
            { 
                return _password; 
            }
            set 
            {
                if (value != null && value.Length < 6) 
                    throw new Exception("Logic mat khau them sau");
                if (value == null) throw new Exception("null");
                _password = value;
            }
        }
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
