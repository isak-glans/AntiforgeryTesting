using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnExample3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearnExample3.Pages
{
    public class NewStudentModel : PageModel
    {
        [BindProperty]
        public Student Input { get; set; }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            var firstname = Input.FirstName;
        }
    }
}