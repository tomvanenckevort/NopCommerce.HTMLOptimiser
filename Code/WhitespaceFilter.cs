using Nop.Core.Infrastructure;
using Nop.Services.Logging;
using System.IO;
using System.Linq;
using System.Text;
using WebMarkupMin.Core.Minifiers;

namespace Nop.Plugin.Misc.HtmlOptimiser.Code
{
    internal class WhitespaceFilter : Stream
    {
        private static HtmlMinifier htmlMinifier = new HtmlMinifier();

        private MemoryStream cacheStream = new MemoryStream();

        private readonly ILogger logger = EngineContext.Current.Resolve<ILogger>();

        private readonly Stream _stream;

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override long Length
        {
            get { return 0; }
        }

        public override long Position { get; set; }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _stream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            // write content in separate stream to cache until ready to flush
            cacheStream.Write(buffer, 0, count);
        }

        public override void Flush()
        {
            // get cache stream content and convert to string
            var streamBuffer = cacheStream.ToArray();

            string content = Encoding.UTF8.GetString(streamBuffer);

            // minify the content
            var result = htmlMinifier.Minify(content);

            if (result.Errors.Count == 0)
            {
                content = result.MinifiedContent;
            }
            else
            {
                // minification failed, log the errors
                var errors = result.Errors.Select(e => string.Format("{0} - {1}", e.Message, e.SourceFragment));

                logger.Error(string.Format("HTML minification failed. Error(s): {0}", string.Join("; ", errors)));
            }

            // write the minified content to the original response stream
            byte[] output = Encoding.UTF8.GetBytes(content);

            _stream.Write(output, 0, output.GetLength(0));

            // clear cache stream
            cacheStream.SetLength(0);

            _stream.Flush();
        }

        public override void Close()
        {
            _stream.Close();
        }

        public WhitespaceFilter(Stream stream)
        {
            _stream = stream;
        }
    }
}
