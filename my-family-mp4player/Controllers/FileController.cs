using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_family_mp4player.Models;

namespace my_family_mp4player.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpGet("GetMenus")]
        public object GetMenus()
        {
            string mp4RootDic = $@"{Environment.CurrentDirectory}\wwwroot\video\";

            MenuModelTest test = new MenuModelTest();

            return test.GetParentMenu(mp4RootDic);
        }
    }
}