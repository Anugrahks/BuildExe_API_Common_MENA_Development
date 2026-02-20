using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BuildExeServices.Models
{
    [Keyless]
    public class UsersChangePassword
    {

        public int UserId { get; set; }
        public string NewPassword { get; set; }

    }
}
