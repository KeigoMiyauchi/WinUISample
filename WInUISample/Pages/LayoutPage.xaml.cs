using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace WinUISample.Pages;

/// <summary>
/// レイアウト章のページです。
/// </summary>
public sealed partial class LayoutPage : Page
{
    private bool _isInitializing = true;

    #region Constructor
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public LayoutPage()
    {
        InitializeComponent();
        InitializeLayoutControls();
        _isInitializing = false;
        ApplyLayoutPreview();
    }
    #endregion

    #region InitializeLayoutControls : レイアウト操作コントロールを初期化
    /// <summary>
    /// レイアウト操作コントロールを初期化します。
    /// </summary>
    private void InitializeLayoutControls()
    {
        LayoutModeComboBox.SelectedIndex = 0;
        ItemSpacingSlider.Value = 16;
        ItemAlignmentComboBox.SelectedIndex = 2;
    }
    #endregion

    #region LayoutModeComboBox_SelectionChanged : 配置パターン選択時のイベントハンドラー
    /// <summary>
    /// 配置パターン選択時のイベントハンドラー
    /// </summary>
    private void LayoutModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_isInitializing)
        {
            return;
        }

        ApplyLayoutPreview();
    }
    #endregion

    #region ItemSpacingSlider_ValueChanged : 余白変更時のイベントハンドラー
    /// <summary>
    /// 余白変更時のイベントハンドラー
    /// </summary>
    private void ItemSpacingSlider_ValueChanged(object sender, Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
    {
        if (_isInitializing)
        {
            return;
        }

        ApplySpacing();
        UpdateLayoutResult();
    }
    #endregion

    #region ItemAlignmentComboBox_SelectionChanged : 横方向配置選択時のイベントハンドラー
    /// <summary>
    /// 横方向配置選択時のイベントハンドラー
    /// </summary>
    private void ItemAlignmentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_isInitializing)
        {
            return;
        }

        ApplyItemAlignment();
        UpdateLayoutResult();
    }
    #endregion

    #region ApplyLayoutPreview : 選択されたレイアウトを適用
    /// <summary>
    /// 選択されたレイアウトをプレビューへ適用します。
    /// </summary>
    private void ApplyLayoutPreview()
    {
        ResetPreviewGrid();

        switch (GetSelectedTag(LayoutModeComboBox))
        {
            case "Rows":
                ConfigureRowsLayout();
                break;

            case "MasterDetail":
                ConfigureMasterDetailLayout();
                break;

            default:
                ConfigureColumnsLayout();
                break;
        }

        ApplySpacing();
        ApplyItemAlignment();
        UpdateLayoutResult();
    }
    #endregion

    #region ResetPreviewGrid : プレビューグリッドを初期化
    /// <summary>
    /// プレビューグリッドを初期化します。
    /// </summary>
    private void ResetPreviewGrid()
    {
        LayoutPreviewGrid.ColumnDefinitions.Clear();
        LayoutPreviewGrid.RowDefinitions.Clear();

        Grid.SetColumn(LayoutItemOne, 0);
        Grid.SetColumn(LayoutItemTwo, 0);
        Grid.SetColumn(LayoutItemThree, 0);
        Grid.SetRow(LayoutItemOne, 0);
        Grid.SetRow(LayoutItemTwo, 0);
        Grid.SetRow(LayoutItemThree, 0);
        Grid.SetColumnSpan(LayoutItemOne, 1);
        Grid.SetColumnSpan(LayoutItemTwo, 1);
        Grid.SetColumnSpan(LayoutItemThree, 1);
        Grid.SetRowSpan(LayoutItemOne, 1);
        Grid.SetRowSpan(LayoutItemTwo, 1);
        Grid.SetRowSpan(LayoutItemThree, 1);
    }
    #endregion

    #region ConfigureColumnsLayout : 横並びレイアウトを設定
    /// <summary>
    /// 横並びレイアウトを設定します。
    /// </summary>
    private void ConfigureColumnsLayout()
    {
        AddStarColumns(3);
        AddStarRows(1);

        Grid.SetColumn(LayoutItemOne, 0);
        Grid.SetColumn(LayoutItemTwo, 1);
        Grid.SetColumn(LayoutItemThree, 2);
    }
    #endregion

    #region ConfigureRowsLayout : 縦積みレイアウトを設定
    /// <summary>
    /// 縦積みレイアウトを設定します。
    /// </summary>
    private void ConfigureRowsLayout()
    {
        AddStarColumns(1);
        AddAutoRows(3);

        Grid.SetRow(LayoutItemOne, 0);
        Grid.SetRow(LayoutItemTwo, 1);
        Grid.SetRow(LayoutItemThree, 2);
    }
    #endregion

    #region ConfigureMasterDetailLayout : 主従レイアウトを設定
    /// <summary>
    /// 主従レイアウトを設定します。
    /// </summary>
    private void ConfigureMasterDetailLayout()
    {
        LayoutPreviewGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
        LayoutPreviewGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
        AddStarRows(2);

        Grid.SetColumn(LayoutItemOne, 0);
        Grid.SetRow(LayoutItemOne, 0);
        Grid.SetRowSpan(LayoutItemOne, 2);

        Grid.SetColumn(LayoutItemTwo, 1);
        Grid.SetRow(LayoutItemTwo, 0);

        Grid.SetColumn(LayoutItemThree, 1);
        Grid.SetRow(LayoutItemThree, 1);
    }
    #endregion

    #region ApplySpacing : 要素間の余白を適用
    /// <summary>
    /// 要素間の余白を適用します。
    /// </summary>
    private void ApplySpacing()
    {
        LayoutPreviewGrid.ColumnSpacing = ItemSpacingSlider.Value;
        LayoutPreviewGrid.RowSpacing = ItemSpacingSlider.Value;
    }
    #endregion

    #region ApplyItemAlignment : 要素の横方向配置を適用
    /// <summary>
    /// 要素の横方向配置を適用します。
    /// </summary>
    private void ApplyItemAlignment()
    {
        var alignment = GetSelectedTag(ItemAlignmentComboBox) switch
        {
            "Left" => HorizontalAlignment.Left,
            "Center" => HorizontalAlignment.Center,
            _ => HorizontalAlignment.Stretch,
        };

        foreach (var item in new[] { LayoutItemOne, LayoutItemTwo, LayoutItemThree })
        {
            item.HorizontalAlignment = alignment;
            item.Width = alignment == HorizontalAlignment.Stretch ? double.NaN : 180;
        }
    }
    #endregion

    #region AddStarColumns : Star 幅の列を追加
    /// <summary>
    /// Star 幅の列を追加します。
    /// </summary>
    private void AddStarColumns(int count)
    {
        for (var index = 0; index < count; index++)
        {
            LayoutPreviewGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        }
    }
    #endregion

    #region AddStarRows : Star 高さの行を追加
    /// <summary>
    /// Star 高さの行を追加します。
    /// </summary>
    private void AddStarRows(int count)
    {
        for (var index = 0; index < count; index++)
        {
            LayoutPreviewGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        }
    }
    #endregion

    #region AddAutoRows : Auto 高さの行を追加
    /// <summary>
    /// Auto 高さの行を追加します。
    /// </summary>
    private void AddAutoRows(int count)
    {
        for (var index = 0; index < count; index++)
        {
            LayoutPreviewGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        }
    }
    #endregion

    #region UpdateLayoutResult : 現在のレイアウト状態表示を更新
    /// <summary>
    /// 現在のレイアウト状態表示を更新します。
    /// </summary>
    private void UpdateLayoutResult()
    {
        LayoutResultTextBlock.Text = $"現在のレイアウト: 配置 = {GetSelectedContent(LayoutModeComboBox)} / 余白 = {ItemSpacingSlider.Value:0}px / 横方向配置 = {GetSelectedContent(ItemAlignmentComboBox)}";
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
}
