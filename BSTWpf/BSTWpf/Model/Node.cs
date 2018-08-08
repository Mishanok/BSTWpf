using System;
using System.ComponentModel;

namespace BSTWpf.Model
{
    public class Node : INotifyPropertyChanged, IComparable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int CompareTo(object obj)
        {
            return Value.CompareTo((double)obj);
        }

        private double value;
        private Node left;
        private Node right;

        public int ID { get; set; }
        public int? TreeID { get; set; }
        public virtual Tree Tree { get; set; }

        public double Value
        {
            get { return value; }
            set { this.value = value; OnPropertyChanged("Value"); }
        }

        public Node Left
        {
            get { return left; }
            set { left = value; OnPropertyChanged("Left"); }
        }

        public Node Right
        {
            get { return right; }
            set { right = value; OnPropertyChanged("Right"); }
        }

        public Node(double value)
        {
            Value = value;
        }
    }
}
