using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeCoreOrnekOzellikler.TagHelpers
{
    [HtmlTargetElement("product-image")]
    public class ImageTagHelper : TagHelper
    {

        public int Width { get; set; }

        public int Height { get; set; }

        public string AltTag { get; set; }

        public string Src { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            output.TagMode = TagMode.StartTagOnly;

            output.Attributes.SetAttribute("src", Src);
            output.Attributes.SetAttribute("width", Width);
            output.Attributes.SetAttribute("height", Height);
            output.Attributes.SetAttribute("alt", AltTag);
        }

    }
}
