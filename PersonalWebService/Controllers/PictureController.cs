using PersonalWebService.Helper;
using PersonalWebService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PersonalWebService.Controllers
{
    [RoutePrefix("api/Scrawl")]
    [ModelValidationFilter]
    [BasicAuthentication]
    public class PictureController : ApiController
    {
        BLL.Picture_BLL pictureBll = new BLL.Picture_BLL();
        [HttpPost]
        [Route("Add")]
        public async Task<ReturnStatus_Model> Add([FromBody]Picture_Model picture)
        {
            return await Task.Run(() =>
            {
                return pictureBll.AddPicture(picture);
            });
        }

        [HttpPost]
        [Route("GetList")]
        public async Task<List<Picture_Model>> GetArticleList([FromBody]PictureCondition_Model pictureCondition)
        {
            return await Task.Run(() =>
            {
                return pictureBll.GetPictureList(pictureCondition);
            });
        }

        [HttpPost]
        [Route("Edit")]
        public async Task<ReturnStatus_Model> Edit([FromBody]Picture_Model picture)
        {
            return await Task.Run(() => { return pictureBll.EditPicture(picture); });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<ReturnStatus_Model> Delete([FromBody]string[] pictureIdList)
        {
            return await Task.Run(() => { return pictureBll.DeletePicture(pictureIdList); });
        }
    }
}
