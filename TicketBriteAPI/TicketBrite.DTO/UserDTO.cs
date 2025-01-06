namespace TicketBrite.DTO
{
    public class UserDTO
    {
        public Guid userID { get; set; }
        public Guid roleID { get; set; }
        public string roleName { get; set; }
        public string userName { get; set; }
        public string userEmail { get; set; }
        public Guid? organizationID { get; set; }
    }
}
