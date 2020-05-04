namespace CarSale.Pages
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System.Diagnostics;

    /// <summary>
    /// Defines the <see cref="ErrorModel" />.
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {

        /// Gets or sets the RequestId.

        public string RequestId { get; set; }


        /// Gets a value indicating whether ShowRequestId.

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);


        /// The OnGet.

        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}
