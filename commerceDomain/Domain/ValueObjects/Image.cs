using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commerceDomain.Domain
{
    public class Image : ValueObject<Image>
    {

        public Image(string imageUrl, string title)
        {
            this.imageUrl = imageUrl;
            this.title = title;
            this.imageExtension = imageUrl.Split('.').Last();
        }
        public string imageUrl { get; private set; }
        public string title { get; private set; }
        public string imageExtension { get; private set; }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            return new List<Object> { imageUrl };
        }
    }
}
