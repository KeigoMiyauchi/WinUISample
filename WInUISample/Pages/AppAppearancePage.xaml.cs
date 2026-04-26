using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace WinUISample.Pages;

/// <summary>
/// アプリ外観章のページです。
/// </summary>
public sealed partial class AppAppearancePage : Page
{
    private const string DefaultWindowTitle = "WinUI 3 ガイド";
    private bool _isInitializing = true;

    #region Constructor
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public AppAppearancePage()
    {
        InitializeComponent();
        InitializeAppearanceControls();
        _isInitializing = false;
        UpdateAppearanceResult();
    }
    #endregion

    #region InitializeAppearanceControls : 外観設定コントロールを初期化
    /// <summary>
    /// 外観設定コントロールを初期化します。
    /// </summary>
    private void InitializeAppearanceControls()
    {
        WindowTitleTextBox.Text = App.MainWindow?.Title ?? DefaultWindowTitle;
        ThemeComboBox.SelectedIndex = GetCurrentThemeIndex();
        BackdropComboBox.SelectedIndex = GetCurrentBackdropIndex();
    }
    #endregion

    #region ThemeComboBox_SelectionChanged : テーマ選択時のイベントハンドラー
    /// <summary>
    /// テーマ選択時のイベントハンドラー
    /// </summary>
    private void ThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_isInitializing)
        {
            return;
        }

        ApplyTheme();
    }
    #endregion

    #region BackdropComboBox_SelectionChanged : 背景素材選択時のイベントハンドラー
    /// <summary>
    /// 背景素材選択時のイベントハンドラー
    /// </summary>
    private void BackdropComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_isInitializing)
        {
            return;
        }

        ApplyBackdrop();
    }
    #endregion

    #region ApplyTitleButton_Click : タイトル適用ボタン押下時のイベントハンドラー
    /// <summary>
    /// タイトル適用ボタン押下時のイベントハンドラー
    /// </summary>
    private void ApplyTitleButton_Click(object sender, RoutedEventArgs e)
    {
        ApplyWindowTitle();
    }
    #endregion

    #region ResetAppearanceButton_Click : 外観設定初期化ボタン押下時のイベントハンドラー
    /// <summary>
    /// 外観設定初期化ボタン押下時のイベントハンドラー
    /// </summary>
    private void ResetAppearanceButton_Click(object sender, RoutedEventArgs e)
    {
        _isInitializing = true;
        ThemeComboBox.SelectedIndex = 0;
        BackdropComboBox.SelectedIndex = 0;
        WindowTitleTextBox.Text = DefaultWindowTitle;
        _isInitializing = false;

        ApplyTheme();
        ApplyBackdrop();
        ApplyWindowTitle();
    }
    #endregion

    #region ApplyTheme : 選択されたテーマを適用
    /// <summary>
    /// 選択されたテーマを適用します。
    /// </summary>
    private void ApplyTheme()
    {
        if (App.MainWindow?.Content is not FrameworkElement rootElement)
        {
            return;
        }

        rootElement.RequestedTheme = GetSelectedTag(ThemeComboBox) switch
        {
            "Light" => ElementTheme.Light,
            "Dark" => ElementTheme.Dark,
            _ => ElementTheme.Default,
        };

        UpdateAppearanceResult();
    }
    #endregion

    #region ApplyBackdrop : 選択された背景素材を適用
    /// <summary>
    /// 選択された背景素材を適用します。
    /// </summary>
    private void ApplyBackdrop()
    {
        if (App.MainWindow is null)
        {
            return;
        }

        App.MainWindow.SystemBackdrop = GetSelectedTag(BackdropComboBox) switch
        {
            "Acrylic" => new DesktopAcrylicBackdrop(),
            "None" => null,
            _ => new MicaBackdrop(),
        };

        UpdateAppearanceResult();
    }
    #endregion

    #region ApplyWindowTitle : 入力されたタイトルを適用
    /// <summary>
    /// 入力されたタイトルを適用します。
    /// </summary>
    private void ApplyWindowTitle()
    {
        if (App.MainWindow is null)
        {
            return;
        }

        var title = string.IsNullOrWhiteSpace(WindowTitleTextBox.Text)
            ? DefaultWindowTitle
            : WindowTitleTextBox.Text.Trim();

        App.MainWindow.Title = title;
        WindowTitleTextBox.Text = title;
        UpdateAppearanceResult();
    }
    #endregion

    #region UpdateAppearanceResult : 現在の外観設定表示を更新
    /// <summary>
    /// 現在の外観設定表示を更新します。
    /// </summary>
    private void UpdateAppearanceResult()
    {
        var title = App.MainWindow?.Title ?? DefaultWindowTitle;
        AppearanceResultTextBlock.Text = $"現在の外観: テーマ = {GetSelectedContent(ThemeComboBox)} / 背景素材 = {GetSelectedContent(BackdropComboBox)} / タイトル = {title}";
    }
    #endregion

    #region GetSelectedTag : 選択項目のタグを取得
    /// <summary>
    /// 選択項目のタグを取得します。
    /// </summary>
    private static string? GetSelectedTag(ComboBox comboBox)
    {
        return (comboBox.SelectedItem as ComboBoxItem)?.Tag?.ToString();
    }
    #endregion

    #region GetCurrentThemeIndex : 現在のテーマ選択位置を取得
    /// <summary>
    /// 現在のテーマ選択位置を取得します。
    /// </summary>
    private static int GetCurrentThemeIndex()
    {
        return App.MainWindow?.Content is FrameworkElement rootElement
            ? rootElement.RequestedTheme switch
            {
                ElementTheme.Light => 1,
                ElementTheme.Dark => 2,
                _ => 0,
            }
            : 0;
    }
    #endregion

    #region GetCurrentBackdropIndex : 現在の背景素材選択位置を取得
    /// <summary>
    /// 現在の背景素材選択位置を取得します。
    /// </summary>
    private static int GetCurrentBackdropIndex()
    {
        return App.MainWindow?.SystemBackdrop switch
        {
            DesktopAcrylicBackdrop => 1,
            null => 2,
            _ => 0,
        };
    }
    #endregion

    #region GetSelectedContent : 選択項目の表示名を取得
    /// <summary>
    /// 選択項目の表示名を取得します。
    /// </summary>
    private static string GetSelectedContent(ComboBox comboBox)
    {
        return (comboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "未選択";
    }
    #endregion
}
