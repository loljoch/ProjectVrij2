using System;

namespace EasyAttributes
{
    /// <summary>
    /// Attribute to create a button in the inspector for calling the method it is attached to.
    /// The method must have no arguments.
    /// </summary>
    /// <example>
    /// [Button]
    /// public void MyMethod()
    /// {
    ///     Debug.Log("Clicked!");
    /// }
    /// </example>
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class PreviewAttribute : Attribute
    {
        private string name = null;
        private ViewMode mode = ViewMode.AlwaysEnabled;
        private Spacing spacing = Spacing.None;

        public string Name { get { return name; } }
        public ViewMode Mode { get { return mode; } }
        public Spacing Spacing { get { return spacing; } }

        public PreviewAttribute()
        {
        }

        public PreviewAttribute(string name)
        {
            this.name = name;
        }

        public PreviewAttribute(ViewMode mode)
        {
            this.mode = mode;
        }

        public PreviewAttribute(Spacing spacing)
        {
            this.spacing = spacing;
        }

        public PreviewAttribute(string name, ViewMode mode)
        {
            this.name = name;
            this.mode = mode;
        }

        public PreviewAttribute(string name, Spacing spacing)
        {
            this.name = name;
            this.spacing = spacing;
        }

        public PreviewAttribute(string name, ViewMode mode, Spacing spacing)
        {
            this.name = name;
            this.mode = mode;
            this.spacing = spacing;
        }
    }
}
