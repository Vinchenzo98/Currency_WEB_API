namespace Currency.API.Models.DTO
{
    public class BlockUserDTO
    {
        public int AccountID { get; set; }
        public int AdminID { get; set; }
        public DateTime BlockDate { get; set; }

        public DateTime UnblockDate { get; set; }

        public int UserID { get; set; }
    }
}