using System.Collections.Generic;

namespace Catalog.API.Contracts.Views
{
    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public IDictionary<string, string> Properties { get; set; }
    }
}
