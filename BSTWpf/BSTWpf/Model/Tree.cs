using BSTWpf.ExtensionMethods;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BSTWpf.Model
{
    public class Tree : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string name;
        private Node root;
        private int _count;
        private string treeString;

        public int ID { get; set; }

        public virtual ICollection<Node> Nodes { get; set; }

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        [NotMapped]
        public string TreeString
        {
            get { return treeString; }
            set { treeString = value; OnPropertyChanged("TreeString"); }
        }

        public Node Root
        {
            get { return root; }
            set { root = value; OnPropertyChanged("Root"); }
        }

        [NotMapped]
        public int Count
        {
            get { return _count; }
            set { _count = value;OnPropertyChanged("Count"); }
        }

        public Tree(double[] arr)
        {
            this.MakeTree(arr);
            this.TreeString = this.GetString();
            Nodes = new List<Node>();
        }

        public Tree(double[] arr, string name) : this(arr)
        {
            this.Name = name;
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

            return new Tree(array, "Example");
        }
    }
}
