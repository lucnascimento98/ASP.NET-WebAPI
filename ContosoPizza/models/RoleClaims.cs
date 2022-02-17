namespace ContosoPizza.models
{
    public class RoleClaims
    {
        public int Id { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public int ClaimsId { get; set; }
        public virtual Claims Claim { get; set; } 
    }
}
