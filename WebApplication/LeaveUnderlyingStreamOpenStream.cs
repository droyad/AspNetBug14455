using System;
using System.IO;
using System.Threading.Tasks;

namespace WebApplication
{
    public class LeaveUnderlyingStreamOpenStream : Stream
    {
        private readonly BufferedStream innerStream;

        public LeaveUnderlyingStreamOpenStream(BufferedStream innerStream)
        {
            this.innerStream = innerStream;
        }

        public override void Flush()
        {
            innerStream.Flush();
        }

        public new Task FlushAsync()
        {
            return innerStream.FlushAsync();
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object? state)
            => innerStream.BeginRead(buffer, offset, count, callback, state);

        public override int EndRead(IAsyncResult asyncResult)
            => innerStream.EndRead(asyncResult);

        public override int Read(byte[] buffer, int offset, int count)
            => innerStream.Read(buffer, offset, count);

        public override long Seek(long offset, SeekOrigin origin)
            => innerStream.Seek(offset, origin);

        public override void SetLength(long value)
        {
            innerStream.SetLength(value);
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object? state)
            => innerStream.BeginWrite(buffer, offset, count, callback, state);

        public override void EndWrite(IAsyncResult asyncResult)
        {
            innerStream.EndWrite(asyncResult);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            innerStream.Write(buffer, offset, count);
        }


        public override bool CanRead => innerStream.CanRead;

        public override bool CanSeek => innerStream.CanSeek;

        public override bool CanWrite => innerStream.CanWrite;

        public override long Length => innerStream.Length;

        public override long Position
        {
            get => innerStream.Position;
            set => innerStream.Position = value;
        }
    }
}