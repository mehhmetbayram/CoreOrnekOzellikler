using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjeCoreOrnekOzellikler.TagHelpers
{
    [HtmlTargetElement("product-paging")]
    public class ProductPagingTagHelper : TagHelper
    {
        [HtmlAttributeName("current-page")]
        public int CurrentPage { get; set; }
        [HtmlAttributeName("current-category")]
        public int CurrentCategory { get; set; }
        [HtmlAttributeName("page-count")]
        public int PageCount { get; set; }
        [HtmlAttributeName("page-size")]
        public int PageSize { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {


            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<nav aria-label='...'>");
            stringBuilder.Append("<ul class='pagination'>");

            stringBuilder.AppendFormat("<li class='{0}'>", CurrentPage == 1 ? "page-item disabled" : "page-item");
            stringBuilder.AppendFormat("<a class='page-link' href='/Sayfalama/Index?currentPage={0}&category={1}'>Previous</a>", CurrentPage - 1, CurrentCategory);
            stringBuilder.Append("</li>");


            stringBuilder.AppendFormat("<li class='{0}'>", CurrentPage == CurrentPage ? "page-item active" : "page-item");

            stringBuilder.AppendFormat("" +
                "<form aasp-action=Index asp-controller=Sayfalama> " +
                "<input type = 'hidden' value = {0} name = category id =category />" +
                "<input type = 'number' value = {1} name = currentPage id = currentPage class='form-control-sm'/>" +
                "<input type = 'submit' value =Git class='btn btn-sm btn-primary'/></form>", CurrentCategory, CurrentPage);

            //stringBuilder.AppendFormat("<a class='page-link' href='/Sayfalama/Index?currentPage={0}&category={1}'>{2}</a>", CurrentPage, CurrentCategory, CurrentPage);

            stringBuilder.Append("</li>");


            //for (int i = 1; i<=PageCount; i++)
            //{

            //        stringBuilder.AppendFormat("<li class='{0}'>", i == CurrentPage ? "page-item active" : "page-item");

            //        stringBuilder.AppendFormat("<a class='page-link' href='/Sayfalama/Index?currentPage={0}&category={1}'>{2}</a>", i, CurrentCategory, i);

            //        stringBuilder.Append("</li>");


            //}

            stringBuilder.AppendFormat("<li class='{0}'>", CurrentPage == PageCount ? "page-item disabled" : "page-item");

            stringBuilder.AppendFormat("<a class='page-link' href='/Sayfalama/Index?currentPage={0}&category={1}'>Next</a>", CurrentPage + 1, CurrentCategory);

            stringBuilder.Append("</li>");
            stringBuilder.Append("</ul>");
            stringBuilder.Append("</nav>");

            output.Content.SetHtmlContent(stringBuilder.ToString());
        }
    }
}
