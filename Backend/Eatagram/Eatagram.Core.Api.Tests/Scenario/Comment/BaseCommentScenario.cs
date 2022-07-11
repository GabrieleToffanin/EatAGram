using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eatagram.Core.Api.Tests.Scenario.Comment
{
    public class BaseCommentScenario : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {                               //Url                      Content           Recipe Id
            yield return new object[] { "api/Comment/PostComment", "Ottimi i fighi", 3 };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
