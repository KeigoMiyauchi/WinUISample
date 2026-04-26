using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace WinUISample.Pages;

/// <summary>
/// ダイアログ章のページです。
/// </summary>
public sealed partial class DialogsPage : Page
{
    #region Constructor
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public DialogsPage()
    {
        InitializeComponent();
    }
    #endregion

    #region ShowDialogButton_Click : ContentDialog 表示ボタン押下時のイベントハンドラー
    /// <summary>
    /// ContentDialog 表示ボタン押下時のイベントハンドラー
    /// </summary>
    private async void ShowDialogButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new ContentDialog
        {
            Title = "ContentDialog のサンプル",
            Content = "WinUI 3 では XamlRoot を設定して、現在のページ上にダイアログを表示します。",
            PrimaryButtonText = "保存する",
            SecondaryButtonText = "あとで",
            CloseButtonText = "閉じる",
            DefaultButton = ContentDialogButton.Primary,
            XamlRoot = XamlRoot,
        };

        var result = await dialog.ShowAsync();
        DialogResultTextBlock.Text = result switch
        {
            ContentDialogResult.Primary => "結果: Primary ボタンが選択されました。",
            ContentDialogResult.Secondary => "結果: Secondary ボタンが選択されました。",
            _ => "結果: ダイアログは閉じられました。",
        };
    }
    #endregion
}
