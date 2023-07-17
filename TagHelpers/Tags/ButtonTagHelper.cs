using CustomTagHelpers.Base_Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomTagHelpers.Tags
{
    [HtmlTargetElement("custom-button")]
    public class ButtonTagHelper : ControlTagHelper
    {

        public ButtonTagHelper(IHttpContextAccessor accessor) : base(accessor)
        {
            
        }

        public override string GenerateControls()
        {
            return "<button type='submit'>Login</button>";
        }
    }
}
