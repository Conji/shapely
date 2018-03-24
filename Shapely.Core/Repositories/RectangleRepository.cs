using Shapely.Core.Contracts;
using Shapely.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace Shapely.Core.Repositories
{
    public class RectangleRepository : IRectangleRepository
    {
        public IEnumerable<RectangleDetail> Find(int take, int skip, string filter, string orderBy)
        {
            var store = RectangleShape.Store.Values.ToArray();
            if (take == 0) take = store.Length;
            if (string.IsNullOrEmpty(filter)) filter = "True";
            if (string.IsNullOrEmpty(orderBy)) orderBy = "Id Asc";

            return store.Where(filter).OrderBy(orderBy).Skip(skip).Take(take).Select(ToJson);
        }

        public RectangleDetail FindById(Guid id)
        {
            return ToJson(RectangleShape.GetById(id));
        }

        public RectangleDetail Insert(RectangleDetail detail)
        {
            var shape = FromJson(detail);
            shape.Save();
            return ToJson(shape);
        }

        public bool Update(RectangleDetail detail)
        {
            try
            {
                var shape = FromJson(detail);
                shape.Save();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                var shape = RectangleShape.GetById(id);
                shape.MarkForDeletion();
                shape.Save();
                return true;
            } 
            catch (NullReferenceException ex)
            {
                return false;
            }
        }

        static RectangleDetail ToJson(RectangleShape value)
        {
            return new RectangleDetail
            {
                Id = value.Id,
                Color = value.Color
            };
        }

        static RectangleShape FromJson(RectangleDetail detail)
        {
            var shape = RectangleShape.GetById(detail.Id) ?? new RectangleShape();
            shape.Id = detail.Id;
            shape.Color = detail.Color;
            return shape;
        }
    }
}
