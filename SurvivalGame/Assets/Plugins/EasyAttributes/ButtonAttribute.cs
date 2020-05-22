using System;

namespace EasyAttributes
{
    public enum ViewMode
    {
        AlwaysEnabled,
        EnabledInPlayMode,
        DisabledInPlayMode
    }

    [Flags]
    public enum Spacing 
    {
        None = 0,
        Before = 1,
        After = 2
    }
    
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
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class ButtonAttribute : Attribute
    {
        private string name = null;
        private ViewMode mode = ViewMode.AlwaysEnabled;
        private Spacing spacing = Spacing.None;

        public string Name { get { return name; } }
        public ViewMode Mode { get { return mode; } }
        public Spacing Spacing { get { return spacing; } }

        public ButtonAttribute()
        {
        }

        public ButtonAttribute(string name)
        {
            this.name = name;
        }

        public ButtonAttribute(ViewMode mode)
        {
            this.mode = mode;
        }
        
        public ButtonAttribute(Spacing spacing) 
        {
            this.spacing = spacing;
        }
        
        public ButtonAttribute(string name, ViewMode mode)
        {
            this.name = name;
            this.mode = mode;
        }

        public ButtonAttribute(string name, Spacing spacing) 
        {
            this.name = name;
            this.spacing = spacing;
        }

        public ButtonAttribute(string name, ViewMode mode, Spacing spacing) 
        {
            this.name = name;
            this.mode = mode;
            this.spacing = spacing;
        }
    }
}

