namespace STARC.Domain.Models
{
    public class HashPassword
    {
        public string Password { get; set; }
        public byte[] Salt { get; set; }
    }
}
