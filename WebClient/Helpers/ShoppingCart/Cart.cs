namespace WebClient.Helpers.ShoppingCart
{
    public class Cart
    {
        public Cart()
        {
            items = new List<Item>();
        }

        public ICollection<Item>? items { get; set; }

        public int CalTotalItems()
        {
            if (items.Any())
            {
                return items.Count();
            }
            return 0;
        }
    }
    public class Item
    {
        public string? Id { get; set; }
        public int amount { get; set; }
    }
}


