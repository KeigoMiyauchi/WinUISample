using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace WinUISample.Pages;

/// <summary>
/// スタイリング章のページです。
/// </summary>
public sealed partial class StylingPage : Page
{
    private bool _isInitializing = true;

    #region Constructor
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public StylingPage()
    {
        InitializeComponent();
        InitializeStyleControls();
        _isInitializing = false;
        ApplySelectedStyle();
    }
    #endregion

    #region InitializeStyleControls : スタイル操作コントロールを初期化
    /// <summary>
    /// スタイル操作コントロールを初期化します。
    /// </summary>
    private void InitializeStyleControls()
    {
        StylePresetComboBox.SelectedIndex = 0;
        CornerRadiusSlider.Value = 8;
        BorderToggleSwitch.IsOn = true;
    }
    #endregion

    #region StylePresetComboBox_SelectionChanged : スタイルプリセット選択時のイベントハンドラー
    /// <summary>
    /// スタイルプリセット選択時のイベントハンドラー
    /// </summary>
    private void StylePresetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_isInitializing)
        {
            return;
        }

        ApplySelectedStyle();
    }
    #endregion

    #region CornerRadiusSlider_ValueChanged : 角丸変更時のイベントハンドラー
    /// <summary>
    /// 角丸変更時のイベントハンドラー
    /// </summary>
    private void CornerRadiusSlider_ValueChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
    {
        if (_isInitializing)
        {
            return;
        }

        StylePreviewCard.CornerRadius = new CornerRadius(e.NewValue);
        UpdateStyleResult();
    }
    #endregion

    #region BorderToggleSwitch_Toggled : 枠線切り替え時のイベントハンドラー
    /// <summary>
    /// 枠線切り替え時のイベントハンドラー
    /// </summary>
    private void BorderToggleSwitch_Toggled(object sender, RoutedEventArgs e)
    {
        if (_isInitializing)
        {
            return;
        }

        StylePreviewCard.BorderThickness = BorderToggleSwitch.IsOn ? new Thickness(1) : new Thickness(0);
        UpdateStyleResult();
    }
    #endregion

    #region ApplySelectedStyle : 選択されたスタイルを適用
    /// <summary>
    /// 選択されたスタイルをプレビューへ適用します。
    /// </summary>
    private void ApplySelectedStyle()
    {
        switch (GetSelectedTag(StylePresetComboBox))
        {
            case "Accent":
                StylePreviewCard.Padding = new Thickness(24);
                StylePreviewCard.Background = GetBrushResource("StylePreviewAccentBackgroundBrush");
                StylePreviewTitleTextBlock.Style = GetStyleResource("PreviewAccentTitleTextBlockStyle");
                StylePreviewBodyTextBlock.FontSize = 15;
                StylePreviewButton.Style = GetStyleResource("PreviewAccentButtonStyle");
                break;

            case "Compact":
                StylePreviewCard.Padding = new Thickness(12);
                StylePreviewCard.Background = GetBrushResource("StylePreviewNeutralBackgroundBrush");
                StylePreviewTitleTextBlock.Style = GetStyleResource("PreviewCompactTitleTextBlockStyle");
                StylePreviewBodyTextBlock.FontSize = 13;
                StylePreviewButton.Style = GetStyleResource("PreviewCompactButtonStyle");
                break;

            default:
                StylePreviewCard.Padding = new Thickness(18);
                StylePreviewCard.Background = GetBrushResource("StylePreviewNeutralBackgroundBrush");
                StylePreviewTitleTextBlock.Style = GetStyleResource("PreviewStandardTitleTextBlockStyle");
                StylePreviewBodyTextBlock.FontSize = 14;
                StylePreviewButton.Style = GetStyleResource("PreviewStandardButtonStyle");
                break;
        }

        StylePreviewCard.CornerRadius = new CornerRadius(CornerRadiusSlider.Value);
        StylePreviewCard.BorderThickness = BorderToggleSwitch.IsOn ? new Thickness(1) : new Thickness(0);
        UpdateStyleResult();
    }
    #endregion

    #region UpdateStyleResult : 現在のスタイル状態表示を更新
    /// <summary>
    /// 現在のスタイル状態表示を更新します。
    /// </summary>
    private void UpdateStyleResult()
    {
        StyleResultTextBlock.Text = $"現在のスタイル: プリセット = {GetSelectedContent(StylePresetComboBox)} / 角丸 = {CornerRadiusSlider.Value:0}px / 枠線 = {(BorderToggleSwitch.IsOn ? "あり" : "なし")}";
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

    #region GetSelectedContent : 選択項目の表示名を取得
    /// <summary>
    /// 選択項目の表示名を取得します。
    /// </summary>
    private static string GetSelectedContent(ComboBox comboBox)
    {
        return (comboBox.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "未選択";
    }
    #endregion

    #region GetBrushResource : ブラシリソースを取得
    /// <summary>
    /// ブラシリソースを取得します。
    /// </summary>
    private Brush GetBrushResource(string key)
    {
        return (Brush)Resources[key];
    }
    #endregion

    #region GetStyleResource : スタイルリソースを取得
    /// <summary>
    /// スタイルリソースを取得します。
    /// </summary>
    private Style GetStyleResource(string key)
    {
        return (Style)Resources[key];
    }
    #endregion
}
