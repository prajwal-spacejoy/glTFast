using System;
using System.IO;

namespace GLTFast.Schema
{

    [System.Serializable]
    public static class LoadConfig
    {
        static Uri textureCDN;
        static TextureType textureFetchMode = TextureType.raw;


        public static string ConstructTextureURL(string filePath)
        {
            string texUrl;
            switch (textureFetchMode)
            {
                case TextureType.ktx:
                    texUrl = Path.GetFileNameWithoutExtension(filePath) + ".ktx2";
                    break;
                default:
                    texUrl = Path.GetFileName(filePath);
                    break;
            }

            return textureCDN.Append(textureFetchMode.ToString(), texUrl).AbsoluteUri;
        }

        public static void SetLoadMode(Uri textureCdn, TextureType textureFetchMode)
        {
            textureCDN = textureCdn;
            LoadConfig.textureFetchMode = textureFetchMode;
        }

    }

    public enum TextureType
    {
        raw,
        ktx,
        x2,
        x4
    }
}
