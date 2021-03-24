#if !UNITY_IOS
//#define USE_KTX
#endif

using System;
using System.IO;

namespace GLTFast.Schema
{

    [System.Serializable]
    public class LoadConfig
    {
        public static Uri textureCDN = new Uri("https://storage.googleapis.com/spacejoy-main/hpGltf/604f3533825f470023137595/");
        public static bool enableTextureCache;
        public static TextureType textureFetchMode = TextureType.raw;
        public static string FileCache = "";


        public static string ConstructTextureURL(string filePath)
        {
            return textureCDN.Append(textureFetchMode.ToString(),
#if USE_KTX
            Path.GetFileNameWithoutExtension(filePath) + ".ktx2").AbsoluteUri;
#else
            Path.GetFileName(filePath)).AbsoluteUri;
#endif
        }
    }

    public enum TextureType
    {
        raw,
#if USE_KTX
        ktx,
#endif
        x2,
        x4
    }
}
