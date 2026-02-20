namespace BuildExeBasic.Models
{
    public class SendMailResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            if (IsSuccess)
            {
                return "{\"message\":\"Email sent successfully\"}";
            }
            else
            {
                return $"Failed to send email. Error: {ErrorMessage}";
            }
        }
    }
}
