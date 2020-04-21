using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace EasyAttributes
{


    public static class ButtonsEditorExtensions
    {
        public static void DrawEasyButtons(this Editor editor)
        {
            // Loop through all methods with no parameters
            var methods = editor.target.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(m => m.GetParameters().Length == 0);
            foreach (var method in methods)
            {
                // Get the ButtonAttribute on the method (if any)
                var ba = (ButtonAttribute)Attribute.GetCustomAttribute(method, typeof(ButtonAttribute));

                if (ba != null)
                {
                    // Determine whether the button should be enabled based on its mode
                    var wasEnabled = GUI.enabled;
                    GUI.enabled = ba.Mode == ViewMode.AlwaysEnabled
                        || (EditorApplication.isPlaying ? ba.Mode == ViewMode.EnabledInPlayMode : ba.Mode == ViewMode.DisabledInPlayMode);


                    if (((int)ba.Spacing & (int)Spacing.Before) != 0) GUILayout.Space(10);
                    
                    // Draw a button which invokes the method
                    var buttonName = String.IsNullOrEmpty(ba.Name) ? ObjectNames.NicifyVariableName(method.Name) : ba.Name;
                    if (GUILayout.Button(buttonName))
                    {
                        foreach (var t in editor.targets)
                        {
                            method.Invoke(t, null);
                        }
                    }

                    if (((int)ba.Spacing & (int)Spacing.After) != 0) GUILayout.Space(10);
                    
                    GUI.enabled = wasEnabled;
                }
            }
        }
    }
}
