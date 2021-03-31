using System;
using System.IO;
using UnityEngine;

namespace GLTFast.Schema
{

    [System.Serializable]
    public static class LoadConfig
    {
        public static bool forceMipGeneration;

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
            //Debug.Log("texture cdn: " + textureCDN.Append(textureFetchMode.ToString(), texUrl).AbsoluteUri);
            return textureCDN.Append(textureFetchMode.ToString(), texUrl).AbsoluteUri;
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
