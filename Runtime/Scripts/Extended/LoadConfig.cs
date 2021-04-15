using System;

namespace GLTFast.Schema
{

    [Serializable]
    public static class LoadConfig
    {
        public const string KtxMimeType = "image/ktx2";
        public static bool forceMipGeneration;
        public static bool enableTextureOverWeb;
        public static bool enableAOTextures;

        static TextureType textureFetchMode = TextureType.raw;

        public static TextureType GetTextureFetchMode()
        {
            return textureFetchMode;
        }

        public static void SetLoadMode(TextureType textureFetchMode, bool forceMipGeneration, bool enableTextureOverWeb, bool enableAOTextures)
        {
            LoadConfig.textureFetchMode = textureFetchMode;
            LoadConfig.forceMipGeneration = forceMipGeneration;
            LoadConfig.enableTextureOverWeb = enableTextureOverWeb;
            LoadConfig.enableAOTextures = enableAOTextures;
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
