using Syncfusion.Windows.Edit;
using System;
using System.IO;
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
using System.Reflection;

namespace EditControl_Text
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _filePath = "../../Resources/TextFile.txt";
        private bool _isCtrlDown = false;
        private EditControl _editor;

        public MainWindow()
        {
            InitializeComponent();

            _editor = new EditControl() { Height = 400, Width = 400, Background = Brushes.White, Foreground = Brushes.Black };
            this.Content = _editor;
            _editor.DocumentLanguage = Languages.Text;
            _editor.Text = GetFileText();
            _editor.PreviewKeyUp += OnPreviewKeyUp;
            _editor.PreviewKeyDown += OnPreviewKeyDown;
        }

        private void OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.RightCtrl:
                case Key.LeftCtrl:
                    _isCtrlDown = false;
                    break;
                default: break;
            }
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.RightCtrl:
                case Key.LeftCtrl:
                    _isCtrlDown = true;
                    break;
                case Key.S:
                    if (_isCtrlDown)
                    {
                        SetFileText(_editor.Text);
                        e.Handled = true;
                    }
                    break;
            }
        }

        private string GetFileText()
        {
            using (StreamReader sr = new StreamReader(_filePath))
            {
                return sr.ReadToEnd();
            }
        }

        private void SetFileText(string text)
        {
            using (StreamWriter sw = new StreamWriter(_filePath))
            {
                sw.Write(text);
                MethodInfo methodInfo = this._editor.GetType().GetMethod("ChangeLinesState", BindingFlags.NonPublic | BindingFlags.Instance);
                methodInfo?.Invoke(this._editor, new object[] { LineModificationState.Modified, LineModificationState.Saved});
            }
        }
    }
}
