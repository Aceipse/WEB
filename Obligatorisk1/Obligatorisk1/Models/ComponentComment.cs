namespace Obligatorisk1.Models
{
    public class ComponentComment
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Value { get; set; }
    }
}