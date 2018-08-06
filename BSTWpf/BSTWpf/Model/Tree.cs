using BSTWpf.ExtensionMethods;
using System;
using System.ComponentModel;

namespace BSTWpf.Model
{
    public class Tree : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Node root;
        private int _count;

        public Node Root
        {
            get { return root; }
            set { root = value; OnPropertyChanged("Root"); }
        }
        public int Count
        {
            get { return _count; }
            set { _count = value;OnPropertyChanged("Count"); }
        }

        public Tree(double[] arr)
        {
            this.MakeTree(arr);
        }

        public static Tree GetTree()
        {
            Random random = new Random();
            double[] arr = new double[10];
            for(int i = 0; i<10; i++)
            {
                arr[i] = random.Next(10);
            }

            var array = new double[] { 17, 137, 914, 90, 3, 123, 5, 50};

            return new Tree(array);
        }
    }
}
