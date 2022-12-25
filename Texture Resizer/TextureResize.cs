using UnityEditor;
using UnityEngine;

public class TextureResize
{
    [MenuItem("Assets/Resize Textures/All Above 512/1024")]
    public static void SetAllAbove512To1k()
    {
        SetMaxTextureSize(1024, 512);
    }

    [MenuItem("Assets/Resize Textures/All Above 512/2048")]
    public static void SetAllAbove512To2k()
    {
        SetMaxTextureSize(2048, 512);
    }

    [MenuItem("Assets/Resize Textures/All Above 512/4096 (Not Recommended)")]
    public static void SetAllAbove512To4K()
    {
        SetMaxTextureSize(4096, 512);
    }

    [MenuItem("Assets/Resize Textures/All/1024")]
    public static void SetAllTo1k()
    {
        SetMaxTextureSize(1024);
    }

    [MenuItem("Assets/Resize Textures/All/2048")]
    public static void SetAllTo2k()
    {
        SetMaxTextureSize(2048);
    }

    [MenuItem("Assets/Resize Textures/All/4096 (Not Recommended)")]
    public static void SetAllTo4k()
    {
        SetMaxTextureSize(4096);
    }

    public static void SetMaxTextureSize(int maxSize, int ignoreBelowSize = 0)
    {
        var textures = Resources.FindObjectsOfTypeAll(typeof(TextureImporter)) as TextureImporter[];      
        int numConverted = 0;

        if (textures.Length <= 0)
        {
            Debug.LogWarning("No textures selected. Please select all the textures you want to modify");
            return;
        }

        foreach (var texture in textures)
        {
            if (texture.maxTextureSize > ignoreBelowSize)
            {
                texture.maxTextureSize = maxSize;
                AssetDatabase.ImportAsset(texture.assetPath);
                numConverted++;
            }
        }
        Debug.Log(numConverted == 1 ? $"{numConverted} texture converted." : $"{numConverted} textures converted.");
    }
}
