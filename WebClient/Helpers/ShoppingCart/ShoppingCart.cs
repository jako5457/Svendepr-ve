using WebClient.Helpers.Constants;


namespace WebClient.Helpers.ShoppingCart
{
    public class ShoppingCart
    {
        public Cart? mycart;
        public ShoppingCart(HttpContext httpContext)
        {
            HttpContext = httpContext;

            if (GetCartSession() == null)
            {
                mycart = new Cart();
                SetCartSession();
            }
            else
            {
                mycart = GetCartSession();
            }
        }
        private HttpContext HttpContext { get; set; }

        private Cart? GetCartSession()
        {
            return HttpContext.Session.Get<Cart>(CartConst.CartSession);
        }

        private void SetCartSession()
        {
            HttpContext.Session.Set<Cart>(CartConst.CartSession, mycart);
        }

        public void ClearCart()
        {
            HttpContext.Session.Remove(CartConst.CartSession);
        }

        public int GetCartItemsCount()
        {
            int counter = 0;

            foreach (Item item in mycart.items)
            {
                counter += item.amount;
            }

            return counter;
        }

        public void AddItem(Item item)
        {
            Item? myitem = mycart?.items?.Where(i => i.Id == item.Id).FirstOrDefault();
            if (myitem != null)
            {
                myitem.amount++;
            }
            else
            {
                mycart?.items?.Add(new Item { amount = 1, Id = item.Id, name = item.name });
            }

            SetCartSession();
        }

        public List<Item> ListItems()
        {
            if (mycart.items.Any())
            {
                return mycart.items;
            }

            return default; //null
        }

        public void RemoveItem(Item item)
        {
            mycart?.items?.Remove(item);

            SetCartSession();
        }
    }
}
