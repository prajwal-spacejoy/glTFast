

using GLTFast.Schema;
using System;
using System.IO;
using UnityEngine;

public class ExtendedGltf
{
    public static bool forceMipGeneration => LoadConfig.forceMipGeneration;


    public static void AllocateTextureResourceFetchURI(Image img, Uri baseUri)
    {
        if (string.IsNullOrEmpty(img.uri))
            return;
        img.uri = LoadConfig.ConstructTextureURL(img.uri);
    }



}
