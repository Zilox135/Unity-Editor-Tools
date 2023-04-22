using System.IO;
using UnityEditor;
using UnityEngine;

public class ExportPrefabIcons
{
    [MenuItem("Assets/Export Prefab Icon")]
    public static void ExportPrefabIconTexture()
    {
        foreach (var obj in Selection.objects)
        {
            if (!PrefabUtility.IsPartOfAnyPrefab(obj))
            {
                Debug.LogWarning($"Object '{obj.name}' is not part of any prefab! Skipping it.");
                continue;
            }

            var texture2D = GetReadableTexture(AssetPreview.GetAssetPreview(obj));

            if (obj != null && texture2D != null)
            {
                SaveTexture(texture2D, obj.name);
            }
        }
    }

    private static void SaveTexture(Texture2D texture, string name)
    {
        var dirPath = Application.dataPath + "/Exported Icons";

        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes($"{dirPath}/{name}.png", bytes);
        Debug.Log($"{bytes.Length / 1024} Kb was saved as: {dirPath}");

        AssetDatabase.Refresh();
    }

    private static Texture2D GetReadableTexture(Texture2D source)
    {
        if (source != null)
        {
            RenderTexture renderTex = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Default);
            Graphics.Blit(source, renderTex);
            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = renderTex;

            Texture2D readableTexture = new(source.width, source.height, TextureFormat.RGBA32, false);
            readableTexture.SetPixels32(source.GetPixels32());

            readableTexture.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
            readableTexture.Apply();
            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(renderTex);
            return readableTexture;
        }

        return null;
    }
}
