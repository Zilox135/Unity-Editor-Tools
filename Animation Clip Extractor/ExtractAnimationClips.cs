using System.Globalization;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ExtractAnimationClips
{
    [MenuItem("Assets/Extract Animation Clips")]
    static void ExtractAnimation()
    {
        foreach (Object selectedObject in Selection.objects)
        {
            string path = AssetDatabase.GetAssetPath(selectedObject);
            var culture = CultureInfo.CurrentCulture;

            if (!string.IsNullOrEmpty(path) && (path.EndsWith(".fbx", true, culture) || path.EndsWith(".obj", true, culture)))
            {
                AssetImporter importer = AssetImporter.GetAtPath(path);

                if (importer is ModelImporter modelImporter)
                {
                    int clipCount = modelImporter.clipAnimations.Length;

                    // Create a new array to hold the clip animations
                    ModelImporterClipAnimation[] clipAnimations = new ModelImporterClipAnimation[clipCount];
                    for (int i = 0; i < clipCount; i++)
                    {
                        clipAnimations[i] = modelImporter.clipAnimations[i];
                    }

                    for (int i = 0; i < clipCount; i++)
                    {
                        string clipName = clipAnimations[i].name;
                        AnimationClip animationClip = new();
                        AssetDatabase.CreateAsset(animationClip, Path.GetDirectoryName(path) + "/" + clipName + ".anim");

                        // Set the clip animations array to contain only the current clip
                        modelImporter.clipAnimations = new ModelImporterClipAnimation[] { clipAnimations[i] };
                        modelImporter.SaveAndReimport();

                        Object[] objects = AssetDatabase.LoadAllAssetsAtPath(modelImporter.assetPath);
                        foreach (Object obj in objects)
                        {
                            if (obj is AnimationClip clip && obj.name == clipName)
                            {
                                EditorCurveBinding[] curveBindings = AnimationUtility.GetCurveBindings(clip);
                                foreach (EditorCurveBinding curveBinding in curveBindings)
                                {
                                    AnimationCurve curve = AnimationUtility.GetEditorCurve(clip, curveBinding);
                                    AnimationUtility.SetEditorCurve(animationClip, curveBinding, curve);
                                }

                                break;
                            }
                        }

                        AssetDatabase.SaveAssets();
                    }
                }
            }
        }
    }

    [MenuItem("Assets/Extract Animation Clips", true)]
    static bool ValidateExtractAnimation()
    {
        foreach (Object selectedObject in Selection.objects)
        {
            string path = AssetDatabase.GetAssetPath(selectedObject);
            var culture = CultureInfo.CurrentCulture;

            if (!string.IsNullOrEmpty(path) && (path.EndsWith(".fbx", true, culture) || path.EndsWith(".obj", true, culture)))
            {
                return true;
            }
        }

        return false;
    }
}
