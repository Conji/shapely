using Shapely.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapely.Core.Contracts
{
    public interface IRectangleRepository
    {
        IEnumerable<RectangleDetail> Find(int take, int skip, string filter, string orderBy);

        RectangleDetail FindById(Guid id);

        RectangleDetail Insert(RectangleDetail detail);

        bool Update(RectangleDetail detail);

        bool Delete(Guid id);
    }
}
