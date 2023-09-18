using System.Collections.Generic;

namespace VivesBlog_Models
{
    public class ArticleModel
    {
        public Article Article { get; set; }
        public IList<Person> Authors { get; set; }
    }
}
