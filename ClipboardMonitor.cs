using System;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace ClipboardHeroTool
{
    internal class ClipboardMonitor
    {
        private HwndSource _hwndSource;

        public ClipboardMonitor()
        {
            HwndSourceParameters parameters = new HwndSourceParameters("ClipboardListener");

            parameters.WindowStyle = (int)(WS_POPUP);
            parameters.ExtendedWindowStyle = (int)(WS_EX_TOOLWINDOW);
            parameters.Width = 0;
            parameters.Height = 0;

            _hwndSource = new HwndSource(parameters);
            AddClipboardFormatListener(_hwndSource.Handle);
            _hwndSource.AddHook(WndProc);
        }

        public event EventHandler ClipboardUpdated;

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_CLIPBOARDUPDATE)
            {
                ClipboardUpdated?.Invoke(this, EventArgs.Empty);
                handled = true;
            }

            return IntPtr.Zero;
        }

        public void StopListening()
        {
            RemoveClipboardFormatListener(_hwndSource.Handle);
            _hwndSource.RemoveHook(WndProc);
            _hwndSource.Dispose();
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AddClipboardFormatListener(IntPtr hwnd);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

        private const int WM_CLIPBOARDUPDATE = 0x031D;
        private const int WS_POPUP = unchecked((int)0x80000000);
        private const int WS_EX_TOOLWINDOW = 0x00000080;
    }
}