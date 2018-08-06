using BSTWpf.ExtensionMethods;
using BSTWpf.Model;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BSTWpf.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        string treeString;
        Tree tree;
        string removeValue;
        string addValue;

        public Tree Tree
        {
            get { return tree; }
            set { tree = value; OnPropertyChanged("Tree"); }
        }
        public string TreeString
        {
            get { return treeString; }
            set { treeString = value; OnPropertyChanged("TreeString"); }
        }
        public string RemoveValue
        {
            get { return removeValue; }
            set { removeValue = value; OnPropertyChanged("RemoveValue"); }
        }
        public string AddValue
        {
            get { return addValue; }
            set { addValue = value; OnPropertyChanged("AddValue"); }
        }
        public string FoundedValue { get; private set; }
        public bool IsCreated { get; set; }
        public ICommand CreateCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }

        public MainWindowViewModel()
        {
            Tree = Tree.GetTree();
            IsCreated = true;
            TreeString = this.Tree.GetString();
            AddCommand = new DelegateCommand(AddElementToTree, CanAdd);
            RemoveCommand = new DelegateCommand(RemoveElementFromTree, CanAdd);
            ClearCommand = new DelegateCommand(Clear);
            CreateCommand = new DelegateCommand(CreateTree);
        }

        private void CreateTree(object obj)
        {
            Tree = Tree.GetTree();
            IsCreated = true;
            TreeString = Tree.GetString();
        }

        private void Clear(object obj)
        {
            Tree = null;
            TreeString = "";
        }

        private bool CanAdd(object arg)
        {
            return IsCreated;
        }

        private void RemoveElementFromTree(object obj)
        {
            try
            {
                if (obj != null)
                    this.Tree.Remove(Convert.ToDouble(obj));

                TreeString = Tree.GetString();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("No active tree", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Incorrect input","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void AddElementToTree(object obj)
        {
            try
            {
                this.Tree.AddItem(Convert.ToDouble(obj));
                TreeString = Tree.GetString();
            }
            catch (NullReferenceException) { MessageBox.Show("You need to create tree first", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (FormatException) { MessageBox.Show("Incorect input","Error",MessageBoxButton.OK, MessageBoxImage.Error); }
            //catch (Exception) { MessageBox.Show("Unknown error", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
