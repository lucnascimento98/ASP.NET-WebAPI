namespace ContosoPizza.Models
{
    public class RoleClaim
    {
        public int Id { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public int Claim { get; set; }
    }
}
