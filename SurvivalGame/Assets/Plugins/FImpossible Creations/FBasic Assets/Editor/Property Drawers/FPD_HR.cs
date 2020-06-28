using UnityEditor;
using UnityEngine;

namespace FIMSpace.FEditor
{
    [CustomPropertyDrawer(typeof(FD_HRAttribute))]
    public class FD_HR : DecoratorDrawer
    {
        FD_HRAttribute Attribute { get { return ((FD_HRAttribute)base.attribute); } }

        public override void OnGUI(Rect position)
        {
            FEditor.FEditor_Styles.DrawUILine(Attribute.Color, Attribute.thickness, Attribute.padding);
        }

        public override float GetHeight()
        {
            return 0f;
        }
    }

}

