using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheShop.Model.Domin;

namespace TheShop.Domain
{
    public interface ISupplire
    {
        bool ArticleInInventory(int id);
        Article GetArticle(int id);
    }
}
