using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using msac_tpa_new.Entities;

namespace msac_tpa_new.App_Code
{
    public static class SportmanHelper
    {
        public static string GetSportmanFullName(this IHtmlHelper html, Sportman sportman)
        {
            return $"{sportman.Surname} {sportman.Name} {sportman.LastName}";
        }
    }
}
