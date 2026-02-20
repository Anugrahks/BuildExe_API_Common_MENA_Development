using Microsoft.EntityFrameworkCore;
using System;

namespace BuildExeHR.Models
{
    [Keyless]
    public class DefaultDate
    {
        public DateTime? FromDate { get; set; }

    }
}
