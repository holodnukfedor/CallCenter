using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace CallCenterDAL.Entities
{
    public class User
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string Patronymic { get; set; }

        [Required]
        [MinLength(11)]
        [MaxLength(14)]
        public string Phone { get; set; }

        public virtual ICollection<UserInPhoneCall> UserInPhoneCalls { get; set; }
    }
}
