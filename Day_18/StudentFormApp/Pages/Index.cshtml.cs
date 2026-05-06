using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StudentApp.Pages
{
    public class IndexModel : PageModel
    {
        // Two-way binding property
        [BindProperty]
        public string StudentName { get; set; }

        public string Message { get; set; }

        // Handles GET request
        public void OnGet()
        {
        }

        // Handles POST request
        public void OnPost()
        {
            if (!string.IsNullOrEmpty(StudentName))
            {
                Message = $"Welcome, {StudentName}! Your registration is successful.";
            }
            else
            {
                Message = "Please enter your name.";
            }
        }
    }
}