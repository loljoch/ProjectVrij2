using UnityEditor;

namespace EasyAttributes
{
    /// <summary>
    /// Custom inspector for Object including derived classes.
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UnityEngine.Object), true)]
    public class ObjectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            this.DrawTexturePreview();
            this.DrawEasyButtons();

            // Draw the rest of the inspector as usual
            DrawDefaultInspector();
        }
    }
}
