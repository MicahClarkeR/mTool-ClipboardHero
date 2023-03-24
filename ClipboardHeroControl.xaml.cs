using ClipboardHeroTool;
using mToolkitPlatformComponentLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace ClipboardHero
{
    /// <summary>
    /// Interaction logic for ClipboardHeroControl.xaml
    /// </summary>
    public partial class ClipboardHeroControl : UserControl, mToolOwned
    {
        private readonly mTool Owner;
        private ClipboardMonitor _clipboardMonitor;

        public ClipboardHeroControl(mTool owner)
        {
            InitializeComponent();
            Owner = owner;
            Loaded += MyUserControl_Loaded;
            Unloaded += MyUserControl_Unloaded;
        }

        public mTool GetOwner()
        {
            return Owner;
        }

        private void MyUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _clipboardMonitor = new ClipboardMonitor();
            _clipboardMonitor.ClipboardUpdated += OnClipboardUpdated;
        }

        private void MyUserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            _clipboardMonitor.ClipboardUpdated -= OnClipboardUpdated;
            _clipboardMonitor.StopListening();
        }

        private void OnClipboardUpdated(object sender, EventArgs e)
        {
            // Handle the clipboard update here.
            ProcessClipboardData();
        }

        private void ProcessClipboardData()
        {
            // Check if the clipboard contains text
            if (Clipboard.ContainsText())
            {
                string clipboardText = Clipboard.GetText();
                // Handle the text data, for example, display it in a TextBox
                ProcessText(clipboardText);
            }
            // Check if the clipboard contains an image
            else if (Clipboard.ContainsImage())
            {
                BitmapSource clipboardImage = Clipboard.GetImage();
                // Handle the image data, for example, display it in an Image control
                myImage.Source = clipboardImage;
            }
            else
            {
                // Handle other data formats or notify the user that the data format is not supported
            }
        }

        private void ProcessText(string text)
        {
            ClipboardInput.Text = text;
            Output.Text = $"{PreappendInput.Text}{text}{AppendInput.Text}";
        }
    }
}
