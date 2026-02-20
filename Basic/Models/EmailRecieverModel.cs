using Microsoft.EntityFrameworkCore;

namespace BuildExeBasic.Models
{
    [Keyless]
    public class EmailRecieverModel
    {
        public string EmailId { get; set; }
    }
}
