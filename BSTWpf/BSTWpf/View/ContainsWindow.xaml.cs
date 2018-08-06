using System.Windows;

namespace BSTWpf.View
{
    /// <summary>
    /// Логика взаимодействия для ContainsWindow.xaml
    /// </summary>
    public partial class ContainsWindow : Window
    {
        public ContainsWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        public double Value
        {
            get
            {
                double result = 0;

                double.TryParse(ContainsValueBox.Text, out result);

                return result;
            }
        }
    }
}
