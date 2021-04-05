

using GLTFast.Schema;
using System;
using System.IO;
using UnityEngine;

public class ExtendedGltf
{
    public static bool forceMipGeneration => LoadConfig.forceMipGeneration;

    public string currentFileUniqueID; 


    public void AllocateTextureResourceFetchURI(Image img, Uri baseUri)
    {
        if (string.IsNullOrEmpty(img.uri))
            return;
        img.uri = LoadConfig.ConstructTextureURL(img.uri, currentFileUniqueID);

        if (LoadConfig.GetTextureFetchMode() == TextureType.ktx)
            img.mimeType = LoadConfig.KtxMimeType;
    }



}
