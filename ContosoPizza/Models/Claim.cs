namespace ContosoPizza.Models
{
    public class Claim
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<RoleClaim> RoleClaims { get; set; }
    }
}
