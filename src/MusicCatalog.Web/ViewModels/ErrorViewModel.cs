namespace MusicCatalog.Web.ViewModels
{
    /// <summary>
    /// Error view model
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Request id
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Is request id showed
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
