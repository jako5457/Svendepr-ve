namespace WebClient.Helpers.Navigation
{
    public static class Charnavigation
    {
        public static string ReplaceComma(this string mystring)
        {
            return mystring.Replace(",", ".");
        }
    }
}
