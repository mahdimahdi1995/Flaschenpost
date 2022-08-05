namespace Flaschenpost.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string Name { get; set; }
        public List<Article> Articles { get; set; }
    }
}
