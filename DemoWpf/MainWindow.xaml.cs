using System;
using System.Collections.Generic;
using System.IO;
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

namespace DemoWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //Sincrona
            /*printOutput( longOperation2( longOperation() ) );

            //Parallela con attesa attiva
            var t1 = Task.Run(() => longOperation());
            var t2 = Task.Run(() => longOperation2(t1.Result));
            Task.Run( () => printOutput( t2.Result ) );

            //Asincrona
            Task.Run(() => longOperation())
                .ContinueWith( p => longOperation2(p.Result))
                .ContinueWith( p => printOutput(p.Result));*/
            
            //Asincrona con await
            var r1 = await longOperationAsync();
            var r2 = await longOperation2Async(r1);
            printOutput(r2);

            printOutput(await longOperation2Async(await longOperationAsync()));
        }

        private Task<int> longOperationAsync()
        {
            return Task.Run(() => longOperation());
        }

        private int longOperation()
        {
            Random random = new Random();
            Thread.Sleep(4000);
            
            int Result = random.Next(3000);
            return Result;
        }

        private Task<int> longOperation2Async(int n)
        {
            return Task.Run(() => longOperation2(n));
        }
        private int longOperation2(int n)
        {
            Random random = new Random();
            Thread.Sleep(n);

            int Result = random.Next();
            return Result;
        }

        private void printOutput(int n)
        {
            MessageBox.Show($"Generato il numero {n}");
        }
    }
}
