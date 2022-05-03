namespace WebClient.Helpers.ShoppingCart
{
    public class Cart
    {
        public Cart()
        {
            items = new List<Item>();
        }

        public List<Item>? items { get; set; }
    }
    public class Item
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int amount { get; set; }
    }
}


