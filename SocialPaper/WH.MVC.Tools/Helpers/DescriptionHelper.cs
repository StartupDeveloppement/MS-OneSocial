using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace System.Web.Mvc
{
    public static class DescriptionHelper
    {
        public static HtmlString DescriptionFor<TModel, TResult>(this HtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression, string labelText = null)
        {
            string propName = ExpressionHelper.GetExpressionText(expression);
            string unqualifiedPropName = propName.Split('.').Last(); // if there is a . in the name, take the rightmost part.
            ModelMetadata metadata = html.ViewData.ModelMetadata.Properties.First(p => p.PropertyName == propName);

            string htmlBuilder = string.Format("<br /><span for=\"{0}\" style=\"color: #969696; margin: 0 0 0 10px; font-size: 8pt;\">{1}</span>", propName, metadata.Description);

            return new HtmlString(htmlBuilder);
        }
    }
}
