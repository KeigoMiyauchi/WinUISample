using Microsoft.UI.Xaml;

namespace WinUISample
{
    /// <summary>
    /// アプリ全体の起動処理と共通動作を管理します。
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// 現在表示しているメインウィンドウです。
        /// </summary>
        public static Window? MainWindow { get; private set; }

        /// <summary>
        /// アプリケーションのシングルトンインスタンスを初期化します。
        /// </summary>
        public App()
        {
            InitializeComponent();
        }

        /// <summary>
        /// アプリ起動時にメインウィンドウを作成して表示します。
        /// </summary>
        /// <param name="args">起動要求の情報です。</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            MainWindow = new MainWindow();
            MainWindow.Activate();
        }
    }
}
