using UnityEditor;
using UnityEngine;

public class SaveMeshAsset : MonoBehaviour
{
    [MenuItem("GameObject/Save Mesh")]
    public static void SaveMesh()
    {
        string path = EditorUtility.SaveFilePanel("Save Separate Mesh Asset", "Assets/", Selection.activeGameObject.name, "asset");
        if (string.IsNullOrEmpty(path)) return;

        path = FileUtil.GetProjectRelativePath(path);

        if (Selection.activeGameObject.TryGetComponent(out MeshFilter meshFilter))
        {
            Mesh mesh = meshFilter.sharedMesh;
            Mesh meshToSave = Instantiate(mesh);
            MeshUtility.Optimize(meshToSave);

            AssetDatabase.CreateAsset(meshToSave, path);
            AssetDatabase.SaveAssets();
        }
    }
}