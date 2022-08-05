namespace Flaschenpost.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public double Price { get; set; }
        public string Unit { get; set; }
        public string PricePerUnitText { get; set; }
        public string Image { get; set; }
    }
}
