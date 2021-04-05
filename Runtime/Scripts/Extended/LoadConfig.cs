using System;
using System.IO;
using UnityEngine;

namespace GLTFast.Schema
{

    [System.Serializable]
    public static class LoadConfig
    {
        public const string KtxMimeType = "image/ktx2";
        public static bool forceMipGeneration;

        static Uri textureCDN;
        static TextureType textureFetchMode = TextureType.raw;


        public static string ConstructTextureURL(string filePath, string uniqueID)
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
            //Debug.Log("texture cdn: " + textureCDN.Append(uniqueID, textureFetchMode.ToString(), texUrl).AbsoluteUri);
            return textureCDN.Append(uniqueID, textureFetchMode.ToString(), texUrl).AbsoluteUri;
        }

        public static TextureType GetTextureFetchMode()
        {
            return textureFetchMode;
        }

        public static void SetLoadMode(Uri textureCdn, TextureType textureFetchMode, bool forceMipGeneration)
        {
            LoadConfig.textureCDN = textureCdn;
            LoadConfig.textureFetchMode = textureFetchMode;
            LoadConfig.forceMipGeneration = forceMipGeneration;
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
