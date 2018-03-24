using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapely.Core.Db
{
    public static class ShapelyService
    {
        public static IEnumerable<RectangleShape> GetAllRectangles()
        {
            return new List<RectangleShape>();
        }

        public static RectangleShape GetRectangle(Guid id)
        {
            return null;
        }

        public static void InsertRectangle(RectangleShape shape)
        {

        }

        public static void UpdateRectangle(RectangleShape shape)
        {

        }

        public static void DeleteRectangle(RectangleShape shape)
        {

        }
    }
}
