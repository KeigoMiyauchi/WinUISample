using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WinUISample.Pages;

namespace WinUISample
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
            GuideNavigationView.SelectedItem = GettingStartedNavigationItem;
            NavigateToPage(GettingStartedNavigationItem);
        }
        #endregion

        #region GuideNavigationView_SelectionChanged : 章選択時のイベントハンドラー
        /// <summary>
        /// 章選択時のイベントハンドラー
        /// </summary>
        private void GuideNavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem is NavigationViewItem selectedItem)
            {
                NavigateToPage(selectedItem);
            }
        }
        #endregion

        #region NavigateToPage : 選択された章へ遷移
        /// <summary>
        /// 選択された章へ遷移します。
        /// </summary>
        private void NavigateToPage(NavigationViewItem selectedItem)
        {
            var pageType = selectedItem.Tag switch
            {
                "GettingStarted" => typeof(GettingStartedPage),
                "AppStructure" => typeof(AppStructurePage),
                "Navigation" => typeof(NavigationPage),
                "Dialogs" => typeof(DialogsPage),
                "Layout" => typeof(LayoutPage),
                "ItemsControls" => typeof(ItemsControlsPage),
                "Styling" => typeof(StylingPage),
                "AppAppearance" => typeof(AppAppearancePage),
                _ => typeof(GettingStartedPage),
            };

            GuideNavigationView.Header = selectedItem.Content;

            if (ContentFrame.CurrentSourcePageType != pageType)
            {
                ContentFrame.Navigate(pageType);
            }
        }
        #endregion
    }
}
