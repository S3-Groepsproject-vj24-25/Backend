using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class TestQrCodeService : QrCodeService
    {
        private readonly byte[] _returnValue;
        private readonly bool _shouldThrow;

        public TestQrCodeService(byte[] returnValue = null, bool shouldThrow = false)
        {
            _returnValue = returnValue ?? new byte[] { 1, 2, 3, 4, 5 };
            _shouldThrow = shouldThrow;
        }

        public override byte[] GenerateQrCode(string content, int pixelsPerModule = 20)
        {
            if (_shouldThrow)
            {
                throw new InvalidOperationException("Test exception");
            }

            LastCalledContent = content;
            LastCalledPixelsPerModule = pixelsPerModule;
            return _returnValue;
        }

        // Properties to track method calls
        public string LastCalledContent { get; private set; }
        public int LastCalledPixelsPerModule { get; private set; }
    }
}

