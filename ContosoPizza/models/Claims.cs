namespace ContosoPizza.models
{
    public class Claims
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<RoleClaims> RolaClaims { get; set; }
    }
}
