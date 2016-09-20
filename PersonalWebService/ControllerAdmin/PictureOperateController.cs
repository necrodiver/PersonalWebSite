using PersonalWebService.BLL;
using PersonalWebService.Helper;
using PersonalWebService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PersonalWebService.ControllerAdmin
{
    [RoutePrefix("api/PictureOperate")]
    [ModelValidationFilter]
    [BasicAuthentication]
    public class PictureOperateController : AdminBaseController
    {
        private static Picture_BLL pictureBll = new Picture_BLL();

        [HttpPost]
        [Route("GetList")]
        public async Task<List<Picture_Model>> GetPictureList([FromBody]PictureCondition_Model pictureCondition)
        {
            return await Task.Run(() =>
            {
                return pictureBll.GetPictureList(pictureCondition);
            });
        }
        [HttpPost]
        [Route("Edit")]
        public async Task<ReturnStatus_Model> EditPicture([FromBody]Picture_Model picture)
        {
            return await Task.Run(() =>
            {
                return pictureBll.EditPicture(picture);
            });
        }
        [HttpPost]
        [Route("Delete")]
        public async Task<ReturnStatus_Model> DeletePictureList(string[] pictureIds)
        {
            return await Task.Run(() =>
            {
                return pictureBll.DeletePicture(pictureIds);
            });
        }
    }
}
