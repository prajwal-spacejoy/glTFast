

using GLTFast.Schema;
using System;
using UnityEngine;

public class ExtendedGltf
{

    public static void AllocateTextureResourceFetchURI(Image img, Uri baseUri)
    {
        Debug.Log("Img: " + img.uri); ;
        Debug.Log("uriAbsoluteUri: " + img.mimeType);
        Debug.Log("uriAbsoluteUri: " + baseUri.AbsoluteUri);
        Debug.Log("uriAbsoluteUri: " + img.bufferView);
        //Debug.Log("uri si path: " + baseUri.Query);
        // Debug.Log("uri LocalPath: " + baseUri.LocalPath);
        //Debug.Log("uri: " + baseUri.IsFile);

        //Check if URI is a local file path
        if (/*!baseUri.IsFile ||*/ string.IsNullOrEmpty(img.uri))
        {
            Debug.LogError("uri not a fiel");
            return;
        }
        Debug.LogError(" FileFetch " + img.uri.ToString());

        Debug.LogError(" FileFetch " + LoadConfig.ConstructTextureURL(img.uri));

        img.uri = LoadConfig.ConstructTextureURL(img.uri);

    }



}
