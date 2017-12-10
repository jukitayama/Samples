using System;
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

namespace async_GUI
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 「重たい処理(同期)」ボタンのクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click( object sender, RoutedEventArgs e )
        {
            this.HeavyProc();
        }

        /// <summary>
        /// 「重たい処理(非同期)」ボタンのクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button1_Click( object sender, RoutedEventArgs e )
        {
            await HeavyProcAsync();
        }

        /// <summary>
        /// 重い処理
        /// </summary>
        private void HeavyProc()
        {
            // 5秒間スリープするだけ。
            System.Threading.Thread.Sleep( 5000 );
        }

        /// <summary>
        /// 重い処理(async)
        /// </summary>
        /// <returns></returns>
        async private Task HeavyProcAsync()
        {
            // 1．Task.Delayはasyncで定義されている。なので、await識別子を指定する事が可能。
            // awaitすることで、一旦UIスレッドに処理を返す事が出来る。
            //  = > 重たい処理が実行中でも、UI操作が可能。フリーズしない。
            // 処理が終了すると、処理スレッドに戻る。
            await Task.Delay( 5000 );

            // 2．Thread.Sleep処理はasyncで定義されていないので、await識別子を指定することが出来ない。
            //  = > 重たい処理が実行中なら、UI操作が不可能。
            // System.Threading.Thread.Sleep( 5000 );

            // 3. 2の処理に対して、非同期処理を行いたい場合は、
            // Task処理として動作させれば良い。
            //await Task.Factory.StartNew( () =>
            //{
            //    System.Threading.Thread.Sleep( 5000 );
            //}
            //);
        }
    }
}
