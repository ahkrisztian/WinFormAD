using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindiwsFormAdModels.UserModels
{
    public class UserAD
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }

        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public DateTime whenCreated { get; set; }
        public DateTime whenUpdated { get; set; }


        public override string ToString()
        {
            return $"Name: {FirstName} {LastName}\n" +
                $"Email: {Email}\n" +
                $"Display Name: {DisplayName}\n" +
                $"Address: {Address}\n" +
                $"Phonenumber: {PhoneNumber}\n" +
                $"Account created at: {whenCreated}";
        }
    }
}
