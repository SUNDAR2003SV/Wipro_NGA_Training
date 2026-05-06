using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UserFormApp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Email { get; set; }

        public string Message { get; set; }

        // 🔹 GET - page load
        public void OnGet()
        {
            Message = "Enter your details";
        }

        // 🔹 POST - form submit
        public void OnPost()
        {
            Message = $"Received: {Username} - {Email}";
        }
    }
}