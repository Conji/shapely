using Shapely.Core.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapely.Core
{
    public class RectangleShape : BusinessBase<RectangleShape>
    {
        #region Private Fields

        private string m_Color;

        #endregion

        #region Public Properties

        public string Color
        {
            get { return m_Color; }
            set { SetValue(value, ref m_Color); }
        }

        #endregion

        protected override void DataDelete()
        {
            ShapelyService.DeleteRectangle(this);
        }

        protected override void DataInsert()
        {
            ShapelyService.InsertRectangle(this);
        }

        protected override RectangleShape DataSelect(Guid id)
        {
            return ShapelyService.GetRectangle(id);
        }

        protected override void DataUpdate()
        {
            ShapelyService.UpdateRectangle(this);
        }

        protected override IEnumerable<RectangleShape> FillStore()
        {
            return ShapelyService.GetAllRectangles();
        }
    }
}
