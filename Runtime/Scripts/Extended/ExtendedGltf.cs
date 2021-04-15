using GLTFast.Schema;
using System.IO;

public class ExtendedGltf
{
    public static bool forceMipGeneration => LoadConfig.forceMipGeneration;
    public static bool enableAOTextures => LoadConfig.enableAOTextures;

    public string currentFileUniqueID;
    public string textureCdnURL;

    public ExtendedGltf(string currentFileUniqueID, string textureCdnURL)
    {
        this.currentFileUniqueID = currentFileUniqueID;
        this.textureCdnURL = textureCdnURL;
    }

    public void AllocateTextureResourceFetchURI(Image img)
    {
        if (string.IsNullOrEmpty(img.uri) || !LoadConfig.enableTextureOverWeb)
            return;

        var textureFetchMode = LoadConfig.GetTextureFetchMode();
        img.uri = ConstructTextureURL(img.uri, currentFileUniqueID, textureFetchMode);

        if (textureFetchMode == TextureType.ktx)
            img.mimeType = LoadConfig.KtxMimeType;
    }


    public string ConstructTextureURL(string filePath, string uniqueID, TextureType textureFetchMode)
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
        //Debug.Log("texture cdn: " + string.Join("/", textureCdnURL, uniqueID, textureFetchMode.ToString(), texUrl));

        return string.Join("/", textureCdnURL, uniqueID, textureFetchMode.ToString(), texUrl);
    }


}
