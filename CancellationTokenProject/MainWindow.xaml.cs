using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace CancellationTokenProject
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        CancellationTokenSource ct = new CancellationTokenSource();
        CancellationTokenSource ct2 = new CancellationTokenSource();
        CancellationTokenSource ct3 = new CancellationTokenSource();

        private void Btn_Start1_Click(object sender, RoutedEventArgs e)
        {
            if (ct2.Token.IsCancellationRequested)
            {
                ct3.Cancel();
            }
            Task.Factory.StartNew(() => DoWork(100, 500, Lbl_Op1, ct));
        }

        private void Btn_Stop1_Click(object sender, RoutedEventArgs e)
        {
            if (ct != null)
            {
                ct.Cancel();
                ct = null;
            }
        }

        private void Btn_Start2_Click(object sender, RoutedEventArgs e)
        {
            ct2 = new CancellationTokenSource();
            if (ct2.Token.IsCancellationRequested)
                ct2.Cancel();

            int max = Convert.ToInt32(Txt_CountOp2.Text);
            Task.Factory.StartNew(() => DoWork(max, 100, Lbl_Op2, ct2));
        }

        private void Btn_Start3_Click(object sender, RoutedEventArgs e)
        {
            ct3 = new CancellationTokenSource();
            if (ct3.Token.IsCancellationRequested)
                ct3.Cancel();
                
            int max = Convert.ToInt32(Txt_CountOp3.Text);
            int delay = Convert.ToInt32(Txt_DelayOp3.Text);
            Task.Factory.StartNew(() => DoWork(max, delay, Lbl_Op3, ct2));
        }

        private void Btn_Stop2and3_Click(object sender, RoutedEventArgs e)
        {
            if (ct2 != null)
            {
                ct2.Cancel();
                ct2 = null;
            }

            if (ct3!=null)
            {
                ct3.Cancel();
                ct3 = null;
            }
        }

        private void Btn_StopAll_Click(object sender, RoutedEventArgs e)
        {
            if (ct != null)
            {
                ct.Cancel();
                ct = null;
            }
            if (ct2 != null)
            {
                ct2.Cancel();
                ct2 = null;
            }
        }

        void DoWork(int max, int delay, Label lbl, CancellationTokenSource ct)
        {
            for (int i = 0; i < delay; i++)
            {
                
                Dispatcher.Invoke(() => UpdateUI(i, lbl));
                Thread.Sleep(delay);
                if (ct.Token.IsCancellationRequested)
                    break;
                Console.ReadLine();
            }
        }

        private void UpdateUI(int i, Label lbl)
        {
            lbl.Content = i.ToString();
        }
    }
}
