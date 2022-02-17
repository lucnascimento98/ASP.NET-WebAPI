namespace ContosoPizza.models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<User> Users { get; set; }
        public virtual List<RoleClaims> RoleClaims { get; set; }
    }
}
