using Microsoft.AspNetCore.Razor.TagHelpers;
using ProjeCoreOrnekOzellikler.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeCoreOrnekOzellikler.TagHelpers
{
    [HtmlTargetElement("category-List")]
    public class CategoryListTagHelper:TagHelper
    {

        private readonly DataContext _context;

        public CategoryListTagHelper(DataContext dataContext)
        {
            _context = dataContext;
        }

        StringBuilder stringBuilder = new StringBuilder();



        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            //Ana Kategorileri Listeliyoruz.. parentId==0 olanlar anakategoriler
            var anaKategoriler = _context.Categories.Where(x => x.ParentId == 0);

            output.TagName = "div class='tree context-menu-one'";

         
            foreach (var item in anaKategoriler)
            {
                
                stringBuilder.AppendLine("<ul >");

                stringBuilder.AppendFormat("<li data-id='{0}'>{1}", item.Id, item.CategoryName);

              


                AltKategoriBul(item.Id);
                stringBuilder.AppendLine("</li>");
                stringBuilder.AppendLine("</ul>");

            }


            output.Content.SetHtmlContent(stringBuilder.ToString());

        }


        void AltKategoriBul(int id)
        {
            //Parametre olarak gelen parentId ye ait alt kategorileri buluyoruz
            var altKategoriler = _context.Categories.Where(x => x.ParentId == id).ToList();

            //eger parametre olarak gelen ana kategoriye ait alt kategoriler var ise if in icine girer
           
            if (altKategoriler.Any(x=>x.ParentId==id))
            {
                stringBuilder.AppendLine("<ul>");
                foreach (var item in altKategoriler)
                {

                    stringBuilder.AppendFormat("<li data-id='{0}'>{1}", item.Id, item.CategoryName);

                    if (_context.Categories.Any(x=>x.ParentId==item.Id))
                    {
                        AltKategoriBul(item.Id);                     
                    }
                    stringBuilder.AppendLine("</li>");                  
                }
                stringBuilder.AppendLine("</ul>");

            }


        }





    }
}
