using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MvcDoodle {

    //http://blogs.msdn.com/b/marcinon/archive/2011/08/16/optimizing-mvc-view-lookup-performance.aspx
    public class TwoLevelViewCache : IViewLocationCache {

        private readonly static object s_key = new object();
        private readonly IViewLocationCache _cache;

        public TwoLevelViewCache(IViewLocationCache cache) {

            _cache = cache;
        }

        public string GetViewLocation(HttpContextBase httpContext, string key) {

            var dictionary = getRequestCache(httpContext);

            string location;
            if (!dictionary.TryGetValue(key, out location)) {

                location = _cache.GetViewLocation(httpContext, key);
                dictionary[key] = location;
            }

            return location;
        }

        public void InsertViewLocation(HttpContextBase httpContext, string key, string virtualPath) {

            _cache.InsertViewLocation(httpContext, key, virtualPath);
        }

        private static IDictionary<string, string> getRequestCache(HttpContextBase httpContext) {

            var dictionary = httpContext.Items[s_key] as IDictionary<string, string>;
            if (dictionary == null) {

                dictionary = new Dictionary<string, string>();
                httpContext.Items[s_key] = dictionary;
            }

            return dictionary;
        }
    }
}