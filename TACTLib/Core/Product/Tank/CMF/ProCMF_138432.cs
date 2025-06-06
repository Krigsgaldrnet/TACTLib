using static TACTLib.Core.Product.Tank.ManifestCryptoHandler;
using static TACTLib.Core.Product.Tank.ContentManifestFile;

namespace TACTLib.Core.Product.Tank.CMF
{
    [ManifestCrypto(AutoDetectVersion = true, Product = TACTProduct.Overwatch)]
    public class ProCMF_138432 : ICMFEncryptionProc
    {
        public byte[] Key(CMFHeader header, int length)
        {
            byte[] buffer = new byte[length];
            uint kidx, okidx;
            kidx = okidx = Keytable[length + 256];
            for (uint i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx += 3;
            }
            return buffer;
        }

        public byte[] IV(CMFHeader header, byte[] digest, int length)
        {
            byte[] buffer = new byte[length];
            uint kidx, okidx;
            kidx = okidx = (uint)(length * header.m_buildVersion);
            for (int i = 0; i != length; ++i)
            {
                buffer[i] = Keytable[SignedMod(kidx, 512)];
                kidx += okidx % 29;
                buffer[i] ^= (byte)(digest[SignedMod(kidx + header.m_dataCount, SHA1_DIGESTSIZE)] + 1);
            }
            return buffer;
        }

        private static readonly byte[] Keytable =
        {
            0x09, 0xCB, 0x0A, 0x37, 0x7E, 0xE1, 0x88, 0x6F, 0x29, 0x49, 0x31, 0x4D, 0x79, 0x6B, 0x17, 0xA3, 
            0x43, 0xD5, 0x72, 0x51, 0x18, 0xC8, 0x5C, 0xAE, 0xF2, 0xE1, 0x93, 0x3F, 0x7A, 0x8D, 0xA7, 0xD6, 
            0xD6, 0x23, 0x97, 0x97, 0xDA, 0x10, 0x02, 0x6D, 0xD8, 0xE0, 0x49, 0x4D, 0xB1, 0x7C, 0x7A, 0xB2, 
            0xF9, 0xC2, 0x23, 0xF8, 0xD8, 0xB4, 0x57, 0x6D, 0xE5, 0xBD, 0xE0, 0x5F, 0x76, 0xF1, 0x2E, 0xC2, 
            0xC5, 0xE2, 0x42, 0x5B, 0xC6, 0x4A, 0xF0, 0xF5, 0x97, 0x37, 0x08, 0x14, 0x29, 0xD2, 0x82, 0x90, 
            0x05, 0xBF, 0xEC, 0x84, 0x61, 0x8C, 0xE0, 0x73, 0x4F, 0xE3, 0xA6, 0x2A, 0x1D, 0xEC, 0x6C, 0xD9, 
            0x6D, 0x47, 0xD0, 0x93, 0xC4, 0xBB, 0x1B, 0xEA, 0x5A, 0x00, 0x93, 0x6A, 0x98, 0xD7, 0x65, 0x9D, 
            0x6D, 0x05, 0x79, 0x60, 0x6B, 0xFB, 0x58, 0x04, 0xF6, 0x59, 0xFC, 0xAB, 0x41, 0x24, 0x9C, 0x52, 
            0x12, 0xEF, 0xB4, 0x22, 0x92, 0xD2, 0x82, 0x8F, 0xA6, 0x87, 0xB7, 0xE3, 0xE0, 0x1B, 0x02, 0x30, 
            0x8C, 0x5E, 0x85, 0x1C, 0x7B, 0x10, 0x38, 0x1E, 0x2C, 0x8E, 0x20, 0xE9, 0x8F, 0x4C, 0x2B, 0xFE, 
            0x6D, 0x3A, 0x6A, 0xB1, 0x76, 0x3C, 0xD5, 0xDD, 0x13, 0x8D, 0x37, 0x66, 0xD8, 0x43, 0x35, 0x15, 
            0xE3, 0x6D, 0x44, 0x80, 0x04, 0x1C, 0xC4, 0x19, 0xBE, 0xEB, 0x56, 0xEE, 0xD3, 0x11, 0x97, 0xDB, 
            0xF6, 0xB4, 0x98, 0xC3, 0x98, 0xD3, 0xBD, 0xEE, 0x64, 0x68, 0xE0, 0xD8, 0x05, 0xD8, 0xC5, 0x4C, 
            0x6C, 0x80, 0x81, 0xB1, 0xFF, 0x70, 0x88, 0xD6, 0xAA, 0x8E, 0x46, 0xAF, 0x92, 0xE2, 0xC0, 0xCF, 
            0xD9, 0x07, 0x09, 0x43, 0x4A, 0xCD, 0xCF, 0xBE, 0xC2, 0x70, 0x95, 0x82, 0xE0, 0xB4, 0xB4, 0x5B, 
            0xD1, 0xC9, 0x93, 0x2C, 0x6C, 0x65, 0x9B, 0xC1, 0x96, 0xF3, 0x38, 0x9B, 0x80, 0x1F, 0x98, 0x7C, 
            0x06, 0xE4, 0x5C, 0xBA, 0x26, 0xF8, 0x06, 0xC8, 0x2D, 0x25, 0xAD, 0xA0, 0x71, 0xE1, 0xDD, 0xA0, 
            0xC9, 0x7F, 0x8A, 0x2E, 0x34, 0x2C, 0x54, 0x08, 0x8E, 0x6A, 0xB7, 0xED, 0x19, 0xB5, 0xA0, 0xCB, 
            0xBB, 0x0A, 0x1F, 0xA1, 0x49, 0x90, 0x72, 0x09, 0x31, 0xB7, 0x44, 0xD7, 0x0B, 0x58, 0x26, 0x65, 
            0x13, 0x6B, 0xF4, 0x06, 0x40, 0x40, 0x9A, 0x89, 0x40, 0x08, 0xCB, 0xAA, 0x01, 0x92, 0x2F, 0x9C, 
            0x58, 0x0E, 0x81, 0x6A, 0xC8, 0x33, 0x7A, 0xF3, 0x8C, 0x83, 0x00, 0x17, 0x1A, 0x90, 0xF3, 0xB1, 
            0x79, 0xC7, 0xAC, 0x5B, 0x3A, 0x9B, 0x16, 0xCA, 0xBC, 0x3E, 0x37, 0x1F, 0x18, 0x7B, 0xF0, 0x78, 
            0xEC, 0xC1, 0x8D, 0xC4, 0x95, 0x96, 0x72, 0xC1, 0x52, 0xCC, 0xA7, 0x66, 0xAB, 0x79, 0x06, 0xBA, 
            0x46, 0x65, 0x00, 0xAA, 0x79, 0x9F, 0xF3, 0x96, 0xDF, 0x38, 0xDF, 0xAD, 0x08, 0x34, 0xD7, 0x7B, 
            0x0E, 0x92, 0xEB, 0x43, 0x55, 0xF5, 0x61, 0xCC, 0xAB, 0x2F, 0x23, 0x2A, 0x09, 0x73, 0x8A, 0xFD, 
            0xC3, 0x11, 0xC7, 0xD2, 0x83, 0x40, 0x2D, 0x80, 0xA5, 0x6C, 0x9F, 0xDB, 0x55, 0xEE, 0xBF, 0x3C, 
            0x80, 0x32, 0x88, 0x6A, 0x01, 0xA6, 0x96, 0x8F, 0x55, 0x5C, 0x7D, 0xA5, 0x67, 0xB9, 0xD5, 0xE8, 
            0x83, 0x0A, 0x88, 0xC7, 0x8C, 0x2D, 0x6C, 0x95, 0x87, 0x27, 0xD7, 0x6B, 0x07, 0x61, 0xD4, 0x54, 
            0x2A, 0xFC, 0xEC, 0x17, 0xAA, 0xB0, 0xD6, 0x50, 0xB7, 0xAC, 0x68, 0x38, 0x66, 0xAF, 0xCF, 0x2E, 
            0x5F, 0x60, 0x9E, 0x6A, 0x27, 0xDB, 0xA6, 0x98, 0x4B, 0xBD, 0xAF, 0x46, 0x30, 0x0E, 0x1A, 0x73, 
            0xD6, 0xDD, 0xAE, 0x02, 0xED, 0x1E, 0x59, 0x6D, 0x3A, 0x60, 0xFE, 0xF7, 0x49, 0xD3, 0x0E, 0x0E, 
            0xE6, 0x73, 0x5D, 0xF9, 0xAB, 0xFB, 0x10, 0x41, 0x8E, 0xD9, 0x75, 0x35, 0x3D, 0x60, 0xA3, 0x36
        };
    }
}