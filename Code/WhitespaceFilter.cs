using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Nop.Plugin.Misc.HtmlOptimiser.Code
{
    internal class WhitespaceFilter : Stream
    {
        private static readonly Regex REPLACE_LINE_BREAKS = 
            new Regex(@"(^\s+)|(\n)", RegexOptions.Multiline | RegexOptions.Compiled);

        private static readonly Regex REPLACE_BETWEEN_TAGS =
            new Regex(@">\s+<", RegexOptions.Multiline | RegexOptions.Compiled);

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

        public override void Flush()
        {
            _stream.Flush();
        }

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

        public override void Close()
        {
            _stream.Close();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            byte[] data = new byte[count];

            Buffer.BlockCopy(buffer, offset, data, 0, count);

            string content = Encoding.Default.GetString(buffer);

            content = REPLACE_LINE_BREAKS.Replace(content, string.Empty);
            content = REPLACE_BETWEEN_TAGS.Replace(content, "><");

            byte[] output = Encoding.Default.GetBytes(content);

            _stream.Write(output, 0, output.GetLength(0));
        }

        public WhitespaceFilter(Stream stream)
        {
            _stream = stream;
        }
    }
}
