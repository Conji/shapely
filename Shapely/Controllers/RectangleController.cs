using Shapely.Core.Contracts;
using Shapely.Core.Models;
using Shapely.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shapely.Controllers
{
    public class RectangleController : ApiController
    {
        IRectangleRepository m_Repository = new RectangleRepository();

        // GET api/values
        [HttpGet]
        public IEnumerable<RectangleDetail> Get(int take = 0, int skip = 0, string filter = "", string order = "")
        {
            return m_Repository.Find(take, skip, filter, order);
        }

        // GET api/values/5
        [HttpGet]
        public RectangleDetail Get(Guid id)
        {
            return m_Repository.FindById(id);
        }

        // POST api/values
        [HttpPost]
        public RectangleDetail Post([FromBody]RectangleDetail value)
        {
            return m_Repository.Insert(value);
        }

        // PUT api/values/5
        [HttpPut]
        public IHttpActionResult Put([FromBody]RectangleDetail value)
        {
            var result = m_Repository.Update(value);
            if (result) return Ok();
            else return NotFound();
        }

        // DELETE api/values/5
        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            var result = m_Repository.Delete(id);
            if (result) return Ok();
            else return NotFound();
        }
    }
}
