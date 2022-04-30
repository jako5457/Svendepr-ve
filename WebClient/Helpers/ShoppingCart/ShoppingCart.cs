using WebClient.Helpers.Constants;


namespace WebClient.Helpers.ShoppingCart
{
    public class ShoppingCart : Cart
    {
        public ShoppingCart(HttpContext httpContext)
        {
            HttpContext = httpContext;
        }
        private HttpContext HttpContext { get; set; }

        public void AddItem(Item item)
        {
            Cart? mycart = HttpContext.Session.Get<Cart>(CartConst.CartSession);
            Item? myitem = mycart?.items?.Where(i => i.Id == item.Id).FirstOrDefault();
            if (myitem != null)
            {
                myitem.amount++;
            }
            else
            {
                mycart?.items?.Add(new Item { amount = 1, Id = item.Id, Name = item.Name });
            }

            HttpContext.Session.Set<Cart>(CartConst.CartSession, mycart);
        }

        public void RemoveItem(Item item)
        {

        }
    }
}
