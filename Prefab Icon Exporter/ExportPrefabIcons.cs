using System.IO;
using UnityEditor;
using UnityEngine;

public class ExportPrefabIcons
{
    [MenuItem("Assets/Save Prefab Icon")]
    public static void SavePrefabIcons()
    {
        foreach (var @object in Selection.objects)
        {
            if (!PrefabUtility.IsPartOfAnyPrefab(@object))
            {
                Debug.LogError($"Object with name {@object.name} is not part of any prefab!");
                return;
            }
            var texture2D = GetReadableTexture(AssetPreview.GetAssetPreview(@object));
            if (@object != null && texture2D != null)
            {
                SaveTexture(texture2D, @object.name);
            }
        }
    }

    private static void SaveTexture(Texture2D texture, string name)
    {
        byte[] bytes = texture.EncodeToPNG();
        var dirPath = Application.dataPath + "/Icons";
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
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
