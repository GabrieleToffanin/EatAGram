using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.SDK.Models
{
    public class HttpResponseMessage<TResponse> where TResponse : class
    {
        private HttpResponseMessage OriginalContent { get; }

        public TResponse? Content { get; }

        public HttpResponseMessage(HttpResponseMessage originalContent, TResponse? content)
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
