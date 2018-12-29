using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace my_family_mp4player.Models
{
    public class MenuModelTest
    {
        public List<MenuModel> GetParentMenu(string rootdic)
        {
            List<MenuModel> menuList = new List<MenuModel>();

            var di = new DirectoryInfo(rootdic);
            menuList = di.GetDirectories()
                .Select(t => new MenuModel()
                {
                    name = t.FileName(),
                    fullName = t.FullName
                }).ToList();

            menuList.AddRange(di.GetFiles()
                .Select(t => new MenuModel()
                {
                    isDirctory = false,
                    name = t.FileName(),
                    fullName = t.FullName
                }).ToList());

            GetMenuModelByParent(menuList);
            return menuList;
        }

        private void GetMenuModelByParent(List<MenuModel> menuList)
        {
            foreach (var item in menuList)
            {
                //如果不是目录，就是文件，则跳过
                if (!Directory.Exists(item.fullName)) continue;

                var di = new DirectoryInfo(item.fullName);
                DirectoryInfo[] allDic = di.GetDirectories();

                item.children = di.GetDirectories()
                    .Select(t => new MenuModel()
                    {
                        name = t.FileName(),
                        fullName = t.FullName
                    }).ToList();

                item.children.AddRange(di.GetFiles()
                    .Select(t => new MenuModel()
                    {
                        isDirctory = false,
                        name = t.FileName(),
                        fullName = t.FullName
                    }).ToList());

                GetMenuModelByParent(item.children);
            }
        }

        /// <summary>
        /// D:\gitrepos\xuxuzhaozhao\my-family-mp4player\wwwroot\video\爱情片 => 爱情片
        /// </summary>
        /// <param name="dicFullName"></param>
        /// <returns></returns>
        private string getRelationDicName(string dicFullName)
        {
            return dicFullName.Split('\\').LastOrDefault();
        }
    }

    public class MenuModel
    {
        public string name { get; set; }
        public string fullName { get; set; }
        public bool isDirctory { get; set; } = true;
        public string file { get; set; } = "video";
        public List<MenuModel> children { get; set; } = new List<MenuModel>();
    }

    public static class Extensions
    {
        public static string FileName(this FileSystemInfo fileInfo)
        {
            return Path.GetFileName(fileInfo.FullName);
        }
    }
}
