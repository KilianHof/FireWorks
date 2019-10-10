using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebApplication1
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;


        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }


        public void OnGet()
        {
        }


        protected void btnBeforeOk_ServerClick(object sender, EventArgs e)
        {
            FireWorks.FireWorksMain program = new FireWorks.FireWorksMain();
            program.Run();
        }

        protected void Start(object Source, EventArgs e)
        {
            
        }

    }
}
