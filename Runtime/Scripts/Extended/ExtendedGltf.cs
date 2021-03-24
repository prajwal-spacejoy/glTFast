

using GLTFast.Schema;
using System;
using System.IO;
using UnityEngine;

public class ExtendedGltf
{

    public static void AllocateTextureResourceFetchURI(Image img, Uri baseUri)
    {
        if (string.IsNullOrEmpty(img.uri))
            return;
        img.uri = LoadConfig.ConstructTextureURL(img.uri);




        //Debug.Log("Img: " + img.uri); ;
        //Debug.Log("uriAbsoluteUri: " + img.mimeType);
        Debug.Log("uriAbsoluteUri: " + baseUri.AbsoluteUri);
        Debug.Log("img.uri: " + img.uri);
        //Debug.Log("uriAbsoluteUri: " + img.bufferView);
        //Debug.Log("uri si path: " + baseUri.Query);
        // Debug.Log("uri LocalPath: " + baseUri.LocalPath);
        //Debug.Log("uri: " + baseUri.IsFile);

        //Check if URI is a local file path
        if (!baseUri.IsFile)
        {
            Debug.LogError("uri not a fiel");
            return;
        }

        Debug.Log("AbsolutePath " + Path.GetDirectoryName(baseUri.AbsolutePath));
        Debug.LogError(" FileFetch " + img.uri.ToString());



    }



}
