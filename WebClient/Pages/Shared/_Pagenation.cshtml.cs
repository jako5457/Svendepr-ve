using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.Helpers.Pagenation;

namespace WebClient.Pages.Shared
{
    public class _PagenationModel : PageModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentPage">int value with currentpage query value</param>
        /// <param name="amount"></param>
        public _PagenationModel(int? currentPage, int amount)
        {
            CurrentPage = currentPage;
            if (CurrentPage != null)
            {
                Pager = new Pager(amount, CurrentPage.Value);
            }
            else
            {
                Pager = new Pager(amount);
            }
        }

        public Pager Pager { get; set; }

        public int? CurrentPage { get; set; }
        public string CurrentPageString { get; set; }

    }
}
