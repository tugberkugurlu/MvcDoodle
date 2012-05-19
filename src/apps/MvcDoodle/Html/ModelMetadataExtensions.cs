using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

namespace MvcDoodle.Html {

    public static class ModelMetadataExtensions {

        public static string WatermarkTextFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression) { 

            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.Watermark;
        }

        public static string DescriptionTextFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression) {

            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return metadata.Description;
        }
    }
}