#region Dateikopf
// Autor:       Maher Al Abbasi       
// Datum:      04.02.2021
#endregion

namespace WebUI.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
