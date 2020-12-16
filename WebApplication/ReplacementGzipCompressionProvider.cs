using System;
using System.IO;
using System.IO.Compression;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Options;

namespace WebApplication
{
    public class ReplacementGzipCompressionProvider : ICompressionProvider
    {
        public ReplacementGzipCompressionProvider(IOptions<GzipCompressionProviderOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            Options = options.Value;
        }

        private GzipCompressionProviderOptions Options { get; }

        /// <inheritdoc />
        public string EncodingName { get; } = "gzip";

        /// <inheritdoc />
        public bool SupportsFlush => true;

        /// <inheritdoc />
        public Stream CreateStream(Stream outputStream)
        {
            var bufferSize = int.TryParse(Environment.GetEnvironmentVariable("BUFFER_SIZE"), out var b) ? b : 100000;
            Console.WriteLine($"Compressing with buffer size {bufferSize}");
            var buffered = new LeaveUnderlyingStreamOpenStream(new BufferedStream(outputStream, bufferSize));
            return new GZipStream(buffered, Options.Level, leaveOpen: false); // HACK: leaveOpen was true in the original implementation, but the buffer needs to be disposed so that it flushes, so LeaveUnderlyingStreamOpenStream is used as a replacement
        }
    }
}