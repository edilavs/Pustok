#pragma checksum "C:\Users\99470\Desktop\ASP-PustokSite\Pustok_AdminPanel\Views\Account\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8ef0210ba743b8f5b76dbb9bdb9839241b6509d5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_Index), @"mvc.1.0.view", @"/Views/Account/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\99470\Desktop\ASP-PustokSite\Pustok_AdminPanel\Views\_ViewImports.cshtml"
using Pustok.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\99470\Desktop\ASP-PustokSite\Pustok_AdminPanel\Views\_ViewImports.cshtml"
using Pustok.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\99470\Desktop\ASP-PustokSite\Pustok_AdminPanel\Views\_ViewImports.cshtml"
using Pustok.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8ef0210ba743b8f5b76dbb9bdb9839241b6509d5", @"/Views/Account/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8a8109e974c018dc83074964af3500260aae020d", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AccountIndexViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<section class=""breadcrumb-section"">
    <h2 class=""sr-only"">Site Breadcrumb</h2>
    <div class=""container"">
        <div class=""breadcrumb-contents"">
            <nav aria-label=""breadcrumb"">
                <ol class=""breadcrumb"">
                    <li class=""breadcrumb-item""><a href=""index.html"">Home</a></li>
                    <li class=""breadcrumb-item active"">Login</li>
                </ol>
            </nav>
        </div>
    </div>
</section>
<!--=============================================
=            Login Register page content         =
=============================================-->
<main class=""page-section inner-page-sec-padding-bottom"">
    <div class=""container"">
        <div class=""row"">
            <div class=""col-sm-12 col-md-12 col-xs-12 col-lg-6 mb--30 mb-lg--0"">
                <!-- Login Form s-->
                ");
#nullable restore
#line 23 "C:\Users\99470\Desktop\ASP-PustokSite\Pustok_AdminPanel\Views\Account\Index.cshtml"
           Write(await Html.PartialAsync("_MemberRegisterForm", Model.RegisterVM));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n            <div class=\"col-sm-12 col-md-12 col-lg-6 col-xs-12\">\r\n                ");
#nullable restore
#line 26 "C:\Users\99470\Desktop\ASP-PustokSite\Pustok_AdminPanel\Views\Account\Index.cshtml"
           Write(await Html.PartialAsync("_MemberLoginForm", Model.LoginVM));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n</main>\r\n\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AccountIndexViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
