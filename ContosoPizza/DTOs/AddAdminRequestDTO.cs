namespace ContosoPizza.DTOs
{
    public class AddAdminRequestDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
