using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace WinUISample.Pages;

/// <summary>
/// WPF 経験者向け初回ガイドページです。
/// </summary>
public sealed partial class GettingStartedPage : Page
{
    #region Constructor
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public GettingStartedPage()
    {
        InitializeComponent();
    }
    #endregion

    #region ShowContentDialogButton_Click : ContentDialog 表示ボタン押下時のイベントハンドラー
    /// <summary>
    /// ContentDialog 表示ボタン押下時のイベントハンドラー
    /// </summary>
    private async void ShowContentDialogButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new ContentDialog
        {
            Title = "ContentDialog のサンプル",
            Content = "WinUI 3 では、ContentDialog に XamlRoot を設定して現在の画面上に表示します。",
            PrimaryButtonText = "OK",
            CloseButtonText = "閉じる",
            DefaultButton = ContentDialogButton.Primary,
            XamlRoot = XamlRoot,
        };

        await dialog.ShowAsync();
    }
    #endregion
}
