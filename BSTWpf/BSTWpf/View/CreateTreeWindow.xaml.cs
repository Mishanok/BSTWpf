using System;
using System.Collections.Generic;
using System.Windows;

namespace BSTWpf.View
{
    /// <summary>
    /// Логика взаимодействия для CreateTreeWindow.xaml
    /// </summary>
    public partial class CreateTreeWindow : Window
    {
        public CreateTreeWindow()
        {
            InitializeComponent();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            MakeArray();
            this.Name = nameBox.Text;
        }

        private void MakeArray()
        {
            List<double> list = new List<double>();

            var array = valuesBox.Text.Split(' ');

            double result = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (Double.TryParse(array[i], out result)) list.Add(result);
                else continue;
            }

            Doubles = list.ToArray();
        }

        public double[] Doubles { get; private set; }
        public new string Name { get; private set; }
    }
}
