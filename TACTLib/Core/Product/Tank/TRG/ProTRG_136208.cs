using static TACTLib.Core.Product.Tank.ManifestCryptoHandler;
using static TACTLib.Core.Product.Tank.ResourceGraph;

namespace TACTLib.Core.Product.Tank.TRG
{
    [ManifestCrypto(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProTRG_136208 : ITRGEncryptionProc
    {
        public byte[] Key(TRGHeader header, int length)
        {
            byte[] buffer = new byte[length];
            uint kidx, okidx;
            kidx = okidx = (uint)(length * header.m_buildVersion);
            for (uint i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx -= header.m_buildVersion & 511;
            }
            return buffer;
        }

        public byte[] IV(TRGHeader header, byte[] digest, int length)
        {
            byte[] buffer = new byte[length];
            uint kidx, okidx;
            kidx = okidx = Keytable[header.m_skinCount & 511];
            for (int i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx += okidx % 61;
                buffer[i] ^= digest[SignedMod(kidx - i, SHA1_DIGESTSIZE)];
            }
            return buffer;
        }

        private static readonly byte[] Keytable =
        {
            0x9F, 0x68, 0x29, 0x8B, 0x31, 0xE4, 0x35, 0x2E, 0x46, 0xE7, 0x47, 0x61, 0xA0, 0xF9, 0xB6, 0xFF, 
            0x8C, 0x54, 0x79, 0x9A, 0xC7, 0x25, 0x64, 0x87, 0x6C, 0x38, 0x9B, 0x54, 0x57, 0xC4, 0x0D, 0x07, 
            0x77, 0x3B, 0x62, 0xEE, 0x47, 0x69, 0x1D, 0x49, 0xE4, 0x67, 0xC0, 0x13, 0x04, 0x2A, 0x21, 0x8C, 
            0x4A, 0xE0, 0xD9, 0x2E, 0x30, 0xB6, 0xDE, 0xDD, 0xB9, 0x87, 0x13, 0xD0, 0x70, 0x66, 0xCB, 0xC0, 
            0x0B, 0xE9, 0x4C, 0x7C, 0xC5, 0xBD, 0x16, 0x8C, 0x1B, 0x1A, 0xC0, 0xDA, 0x49, 0xD4, 0x98, 0x7F, 
            0x05, 0x61, 0xD2, 0x1E, 0xAC, 0x9D, 0xF7, 0x42, 0x76, 0xDB, 0x8A, 0xBF, 0x48, 0xBD, 0xE3, 0xCD, 
            0x10, 0xD9, 0x3C, 0xCB, 0xBC, 0xFA, 0x10, 0xF2, 0xCD, 0x81, 0xC4, 0x7F, 0x8C, 0x94, 0x3C, 0xF7, 
            0x4E, 0xE2, 0xD8, 0xBB, 0xBA, 0x7E, 0x61, 0x2A, 0x93, 0xFC, 0x98, 0x3F, 0xB3, 0x76, 0xC2, 0x6A, 
            0xA2, 0x19, 0x85, 0x47, 0x47, 0xA4, 0xB9, 0x25, 0xE9, 0x51, 0x1D, 0x1A, 0x27, 0xED, 0x59, 0x65, 
            0xE7, 0x40, 0xF3, 0xA2, 0x9B, 0x5E, 0xEC, 0xC6, 0x6B, 0xC7, 0x5D, 0x78, 0x54, 0x90, 0x15, 0xE9, 
            0xDF, 0x29, 0x58, 0x2F, 0x63, 0x77, 0xF4, 0x15, 0xF4, 0xA2, 0x02, 0xA7, 0x91, 0x5D, 0xD1, 0x52, 
            0x5F, 0x58, 0x90, 0x99, 0x7C, 0x27, 0xD0, 0xAE, 0xB4, 0xE3, 0x7E, 0x98, 0x8F, 0x52, 0x13, 0xBB, 
            0xFE, 0x7A, 0x47, 0xAD, 0xE5, 0x0B, 0x8E, 0x27, 0x71, 0x3C, 0x49, 0x0C, 0xEC, 0xE6, 0x92, 0x1E, 
            0x6C, 0xC8, 0x3F, 0xCE, 0x03, 0xC8, 0xCE, 0x44, 0x89, 0x35, 0x1D, 0x18, 0xCB, 0x9F, 0x49, 0x18, 
            0x58, 0xAF, 0xAD, 0x69, 0x72, 0x8B, 0xAD, 0x6E, 0x45, 0x92, 0x58, 0x64, 0x6A, 0xAB, 0x94, 0x53, 
            0x06, 0x5C, 0x73, 0x35, 0x55, 0x83, 0x38, 0xDD, 0x7F, 0x9A, 0x2F, 0xA5, 0xC1, 0x8A, 0xA2, 0xAF, 
            0xA9, 0x28, 0x4F, 0x45, 0x4E, 0x89, 0x4E, 0x55, 0x58, 0xAF, 0xBD, 0x6C, 0x2D, 0x6C, 0xCD, 0xDC, 
            0xE4, 0x97, 0xFB, 0x31, 0xC9, 0xE7, 0xF8, 0x84, 0x59, 0xA6, 0x1F, 0x45, 0x6C, 0xED, 0x6B, 0x71, 
            0x50, 0x4B, 0x45, 0xA5, 0xD5, 0xCB, 0x9D, 0xC0, 0xDA, 0xF3, 0xC9, 0xDF, 0x2F, 0x3A, 0x4D, 0x75, 
            0xEC, 0x04, 0x54, 0x4D, 0x6C, 0xAC, 0xD3, 0x82, 0xA2, 0x82, 0xF5, 0x0E, 0x83, 0xD4, 0xDA, 0x30, 
            0x8E, 0xFF, 0x77, 0x98, 0xBC, 0xAF, 0xD3, 0xCB, 0x2F, 0xD2, 0xC9, 0xD7, 0x12, 0x98, 0x7E, 0x3F, 
            0xEC, 0x9C, 0x5B, 0xE9, 0x77, 0x8A, 0xEE, 0x16, 0x95, 0xCC, 0x75, 0xBB, 0x08, 0xF4, 0xBD, 0xEA, 
            0x62, 0x4C, 0x9F, 0x9C, 0xA6, 0xF7, 0x93, 0x79, 0x22, 0xAA, 0x0A, 0xAD, 0xAB, 0x75, 0x0A, 0x68, 
            0x44, 0x2A, 0x61, 0x5F, 0x85, 0xC1, 0x85, 0xC2, 0x0F, 0x45, 0x8D, 0x0F, 0xB6, 0x85, 0xEE, 0x59, 
            0x4A, 0x01, 0xFB, 0x83, 0x49, 0xEC, 0x17, 0xFB, 0x67, 0x58, 0x82, 0x41, 0xDB, 0xF1, 0x3A, 0xEA, 
            0x37, 0x95, 0x23, 0xF6, 0xD6, 0x62, 0x0F, 0x8B, 0xA3, 0xAC, 0xA4, 0x70, 0x3C, 0x2E, 0x2F, 0x85, 
            0x25, 0x93, 0xFD, 0x69, 0xC6, 0x32, 0xFC, 0xA5, 0xA3, 0x1E, 0x6B, 0x20, 0x2C, 0x3E, 0x43, 0x23, 
            0xDB, 0xE9, 0x86, 0x21, 0xF1, 0x97, 0x6A, 0x35, 0x63, 0xB2, 0xB1, 0x91, 0xCA, 0xEA, 0x13, 0x32, 
            0x7B, 0x56, 0x4C, 0xE8, 0xD8, 0x74, 0xDF, 0xD3, 0xB4, 0x47, 0x2D, 0xB8, 0xF3, 0xBC, 0x61, 0xF0, 
            0xF5, 0x45, 0xD9, 0xBF, 0x05, 0xFE, 0xA9, 0x24, 0xF7, 0x7A, 0xF1, 0xB9, 0xBF, 0x0B, 0x22, 0x2F, 
            0xCC, 0x7E, 0x39, 0xE6, 0x40, 0xD8, 0x31, 0xB5, 0x4E, 0x66, 0x60, 0x83, 0x42, 0x0C, 0xCD, 0xE2, 
            0xC1, 0xE4, 0x19, 0x5D, 0x40, 0x10, 0x62, 0xAF, 0xB1, 0x02, 0xBF, 0x9C, 0x41, 0x7E, 0xF3, 0x57
        };
    }
}