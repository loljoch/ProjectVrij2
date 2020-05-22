using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using System.Text.RegularExpressions;

namespace EasyAttributes
{
    public static class PreviewTextureEditorExtensions
    {
        const int INDENT_SIZE = 20;
        static bool showPreviews = false;

        public static Regex capitalRegex = new Regex(@"
                    (?<=[A-Z])(?=[A-Z][a-z]) |
                     (?<=[^A-Z])(?=[A-Z]) |
                     (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

        public static void DrawTexturePreview(this Editor editor)
        {
            // Loop through all fields that are texture2d
            var fields = editor.target.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(f => typeof(Texture2D).IsAssignableFrom(f.FieldType) && Attribute.IsDefined(f, typeof(PreviewAttribute)));

            if (fields.Count() < 1) return;

            showPreviews = EditorGUILayout.Foldout(showPreviews, new GUIContent("Preview Textures"));

            if (showPreviews)
            {
                foreach (var field in fields)
                {
                    // Get the ButtonAttribute on the method (if any)
                    var ba = (PreviewAttribute)Attribute.GetCustomAttribute(field, typeof(PreviewAttribute));

                    // Determine whether the button should be enabled based on its mode
                    bool wasEnabled = GUI.enabled;
                    GUI.enabled = ba.Mode == ViewMode.AlwaysEnabled
                        || (EditorApplication.isPlaying ? ba.Mode == ViewMode.EnabledInPlayMode : ba.Mode == ViewMode.DisabledInPlayMode);


                    if (((int)ba.Spacing & (int)Spacing.Before) != 0) GUILayout.Space(10);

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(INDENT_SIZE);
                    string l = field.Name;
                    GUILayout.Label(capitalRegex.Replace(l[0].ToString().ToUpper() + l.Substring(1), " "));
                    GUILayout.Label((Texture2D)field.GetValue(editor.target));
                    GUILayout.EndHorizontal();

                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                    if (((int)ba.Spacing & (int)Spacing.After) != 0) GUILayout.Space(10);

                    GUI.enabled = wasEnabled;

                }
            }
        }
    }
}
