using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcDoodle.Html {

    public class IndexedItem<TModel> {

        public IndexedItem(int index, TModel item) {

            Index = index;
            Item = item;
        }

        public int Index { get; private set; }
        public TModel Item { get; private set; }
    }
}
