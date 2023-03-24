using mToolkitPlatformComponentLibrary;
using System;
using System.Windows.Controls;

namespace ClipboardHero
{
    public class ClipboardHeroTool : mTool
    {
        private ClipboardHeroControl? UI = null;

        public ClipboardHeroTool(string guid, string directory) : base(guid, directory)
        {
            
        }

        public override UserControl CreateUI()
        {
            UI ??= new ClipboardHeroControl(this);
            return UI;
        }

        public override void Initialise()
        {
        }

        protected override ToolInfo GetInfo()
        {
            return new ToolInfo("Clipboard Hero",
                                "ClipboardHero",
                                "Micah", "1.0", "Clipboard utility tool.");
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            UI = null;
        }

        protected override Type GetToolType()
        {
            return typeof(ClipboardHeroTool);
        }
    }
}
