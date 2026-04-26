using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace WinUISample.Pages;

/// <summary>
/// ItemsControls 章のページです。
/// </summary>
public sealed partial class ItemsControlsPage : Page
{
    private readonly IReadOnlyList<ItemsControlSample> _allListSamples =
    [
        new(
            "ItemsSource",
            "画面に表示するコレクションを ListView に渡します。",
            "WPF と同じく ItemsSource に IEnumerable を渡せます。更新通知が必要な場合は ObservableCollection を使います。",
            "WinUI 3 でも UI スレッド上でコレクションを更新する前提は変わりません。"),
        new(
            "DataTemplate",
            "1 件分の表示を XAML で定義します。",
            "WPF の DataTemplate と近い考え方ですが、WinUI 3 の標準スタイルや余白に合わせるとアプリ全体の見た目がそろいます。",
            "テンプレート内の要素数が増えるほどスクロール時の負荷も増えます。"),
        new(
            "SelectionChanged",
            "選択された項目を取り出して詳細表示へ反映します。",
            "ListBox / ListView の選択イベントに近い使い方です。複数選択では SelectedItems を確認します。",
            "未選択を許可する画面では null の扱いも UI 表示として設計します。"),
    ];

    /// <summary>
    /// ListView に表示するサンプル項目です。
    /// </summary>
    public ObservableCollection<ItemsControlSample> VisibleListSamples { get; } = [];

    /// <summary>
    /// GridView に表示するタイル項目です。
    /// </summary>
    public IReadOnlyList<ItemsControlTileSample> TileSamples { get; } =
    [
        new("ListView", "縦方向の一覧と選択に向いています。"),
        new("GridView", "カードや画像をタイル状に並べる一覧に向いています。"),
        new("ItemsRepeater", "選択 UI を持たない軽量な繰り返し表示に向いています。"),
    ];

    #region Constructor
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public ItemsControlsPage()
    {
        InitializeComponent();
        RestoreListSamples();
        ItemsSamplesListView.SelectedIndex = 0;
        UpdateEmptyState();
        UpdateSelectionResult();
    }
    #endregion

    #region EmptyStateToggleSwitch_Toggled : 空状態切り替え時のイベントハンドラー
    /// <summary>
    /// 空状態切り替え時のイベントハンドラー
    /// </summary>
    private void EmptyStateToggleSwitch_Toggled(object sender, RoutedEventArgs e)
    {
        if (EmptyStateToggleSwitch.IsOn)
        {
            VisibleListSamples.Clear();
        }
        else
        {
            RestoreListSamples();
            ItemsSamplesListView.SelectedIndex = 0;
        }

        UpdateEmptyState();
        UpdateSelectionResult();
    }
    #endregion

    #region ItemsSamplesListView_SelectionChanged : サンプル選択時のイベントハンドラー
    /// <summary>
    /// サンプル選択時のイベントハンドラー
    /// </summary>
    private void ItemsSamplesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        UpdateSelectionResult();
    }
    #endregion

    #region RestoreListSamples : ListView のサンプル項目を復元
    /// <summary>
    /// ListView のサンプル項目を復元します。
    /// </summary>
    private void RestoreListSamples()
    {
        VisibleListSamples.Clear();

        foreach (var sample in _allListSamples)
        {
            VisibleListSamples.Add(sample);
        }
    }
    #endregion

    #region UpdateEmptyState : 空状態表示を更新
    /// <summary>
    /// 空状態表示を更新します。
    /// </summary>
    private void UpdateEmptyState()
    {
        var isEmpty = VisibleListSamples.Count == 0;
        EmptyStateBorder.Visibility = isEmpty ? Visibility.Visible : Visibility.Collapsed;
        ItemsSamplesListView.Visibility = isEmpty ? Visibility.Collapsed : Visibility.Visible;
    }
    #endregion

    #region UpdateSelectionResult : 選択結果表示を更新
    /// <summary>
    /// 選択結果表示を更新します。
    /// </summary>
    private void UpdateSelectionResult()
    {
        if (ItemsSamplesListView.SelectedItem is not ItemsControlSample selectedSample)
        {
            SelectionResultTextBlock.Text = "項目が選択されていません。ItemsSource が空のときは、一覧とは別に空状態 UI を表示します。";
            return;
        }

        SelectionResultTextBlock.Text = $"{selectedSample.Title}\n{selectedSample.WpfComparison}\n注意点: {selectedSample.Note}";
    }
    #endregion
}

/// <summary>
/// ListView の説明用サンプル項目です。
/// </summary>
public sealed class ItemsControlSample
{
    /// <summary>
    /// 項目名です。
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 一覧に表示する概要です。
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    /// WPF 経験者向けの比較説明です。
    /// </summary>
    public string WpfComparison { get; set; }

    /// <summary>
    /// 実装時の注意点です。
    /// </summary>
    public string Note { get; set; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public ItemsControlSample(string title, string summary, string wpfComparison, string note)
    {
        Title = title;
        Summary = summary;
        WpfComparison = wpfComparison;
        Note = note;
    }
}

/// <summary>
/// GridView の説明用タイル項目です。
/// </summary>
public sealed class ItemsControlTileSample
{
    /// <summary>
    /// タイル名です。
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// タイルに表示する概要です。
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public ItemsControlTileSample(string title, string summary)
    {
        Title = title;
        Summary = summary;
    }
}
