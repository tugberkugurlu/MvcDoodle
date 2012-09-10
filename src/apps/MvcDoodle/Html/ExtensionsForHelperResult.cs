using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.WebPages;

namespace MvcDoodle.Html {

    public static class ExtensionsForHelperResult {

        public static HelperResult Each<TItem>(
            this IEnumerable<TItem> items,
            Func<IndexedItem<TItem>, HelperResult> template) {

            return new HelperResult(
                writer => {

                    for (int i = 0; i < items.Count(); i++) {

                        template(new IndexedItem<TItem>(i, items.ElementAt(i))).WriteTo(writer);
                    }

                });
        }
    }
}
