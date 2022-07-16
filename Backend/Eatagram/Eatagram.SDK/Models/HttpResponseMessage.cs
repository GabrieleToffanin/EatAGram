using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.SDK.Models
{
    public class HttpResponseMessage<TResponse> where TResponse : class
    {
        public HttpResponseMessage OriginalContent { get; set; }

        public TResponse Content { get; set; }

        public HttpResponseMessage(HttpResponseMessage originalContent, TResponse content)
        {
            OriginalContent = originalContent;
            Content = content;
        }

        public HttpResponseMessage(HttpResponseMessage originalContent)
        {
            OriginalContent = originalContent;
        }
    }
}
