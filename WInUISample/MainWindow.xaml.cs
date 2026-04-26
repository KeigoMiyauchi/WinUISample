using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace WInUISample
{
    public sealed partial class MainWindow : Window
    {
        #region Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region HelloButton_Click : HelloButton のクリックイベントハンドラー
        /// <summary>
        /// HelloButton のクリックイベントハンドラー
        /// </summary>
        private async void HelloButton_Click(object sender, RoutedEventArgs e)
        {
            // WinUI では、MessageBox は存在しないため、ContentDialog を使用してメッセージを表示します。
            var dialog = new ContentDialog()
            {
                Title = "Hello, WinUI!",
                Content = "This is a message from the button click event.",
                PrimaryButtonText = "Yes",
                SecondaryButtonText = "No",
                CloseButtonText = "Cancel",
                XamlRoot = Content.XamlRoot,
            };
            _ = dialog.ShowAsync();
        }
        #endregion
    }
}
