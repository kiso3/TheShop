using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheShop.Model.Domin;

namespace TheShop.Repository
{
    public interface IArticlelRepository
    {
        Article GetById(int id);
        void Save(Article article);

    }
}
