using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace MvcDoodle {

    public class RemoveWhitespacesAttribute : FilterAttribute, IResultFilter {

        public void OnResultExecuted(ResultExecutedContext filterContext) {

            var response = filterContext.HttpContext.Response;

            //Temp fix. I am not sure what causes this but ContentType is coming as text/html
            if (filterContext.HttpContext.Request.RawUrl != "/sitemap.xml") {

                if (response.ContentType == "text/html" && response.Filter != null) {
                    response.Filter = new HelperClass(response.Filter);
                }
            }
        }

        public void OnResultExecuting(ResultExecutingContext filterContext) {
        }

        private class HelperClass : Stream {

            private System.IO.Stream Base;

            public HelperClass(Stream ResponseStream) {

                if (ResponseStream == null)
                    throw new ArgumentNullException("ResponseStream");

                this.Base = ResponseStream;
            }

            StringBuilder s = new StringBuilder();

            public override void Write(byte[] buffer, int offset, int count) {

                string HTML = Encoding.UTF8.GetString(buffer, offset, count);

                //Thanks to Qtax
                //http://stackoverflow.com/questions/8762993/remove-white-space-from-entire-html-but-inside-pre-with-regular-expressions
                Regex reg = new Regex(@"(?<=\s)\s+(?![^<>]*</pre>)");
                HTML = reg.Replace(HTML, string.Empty);

                buffer = System.Text.Encoding.UTF8.GetBytes(HTML);
                this.Base.Write(buffer, 0, buffer.Length);
            }

            #region Other Members

            public override int Read(byte[] buffer, int offset, int count) {

                throw new NotSupportedException();
            }

            public override bool CanRead { get { return false; } }

            public override bool CanSeek { get { return false; } }

            public override bool CanWrite { get { return true; } }

            public override long Length { get { throw new NotSupportedException(); } }

            public override long Position {

                get { throw new NotSupportedException(); }
                set { throw new NotSupportedException(); }
            }

            public override void Flush() {

                Base.Flush();
            }

            public override long Seek(long offset, SeekOrigin origin) {

                throw new NotSupportedException();
            }

            public override void SetLength(long value) {

                throw new NotSupportedException();
            }

            #endregion
        }
    }
}