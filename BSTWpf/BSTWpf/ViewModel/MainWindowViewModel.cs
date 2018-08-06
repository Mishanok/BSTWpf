using BSTWpf.ExtensionMethods;
using BSTWpf.Model;
using System;
using System.ComponentModel;
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
        public ICommand GetMinCommand { get; private set; }
        public ICommand GetMaxCommand { get; private set; }
        public ICommand ContainsCommand { get; private set; }

        public MainWindowViewModel()
        {
            Tree = Tree.GetTree();
            IsCreated = true;
            TreeString = this.Tree.GetString();
            AddCommand = new DelegateCommand(AddElementToTree, CanEdit);
            RemoveCommand = new DelegateCommand(RemoveElementFromTree, CanEdit);
            ClearCommand = new DelegateCommand(Clear);
            CreateCommand = new DelegateCommand(CreateTree);
            GetMinCommand = new DelegateCommand(GetMin, CanEdit);
            GetMaxCommand = new DelegateCommand(GetMax, CanEdit);
            ContainsCommand = new DelegateCommand(Contains, CanEdit);
        }

        private void Contains(object obj)
        {
            if (Tree == null) MessageBox.Show("No active tree", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                string result;

                View.ContainsWindow containsWindow = new View.ContainsWindow();

                if (containsWindow.ShowDialog() == true)
                {
                    result = (Tree.Contains(containsWindow.Value) ? "Exists" : "Doesn`t exists!");
                    MessageBox.Show(result, "Result", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else MessageBox.Show("Invalid Operation", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GetMax(object obj)
        {
            try
            {
                MessageBox.Show("Maximum: " + Tree.GetMax(), "INFO", MessageBoxButton.OK);
            }
            catch (NullReferenceException) { MessageBox.Show("No active tree", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void GetMin(object obj)
        {
            try
            {
                MessageBox.Show("Minimum: " + Tree.GetMin(), "INFO", MessageBoxButton.OK);
            }
            catch(NullReferenceException) { MessageBox.Show("No active tree", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void CreateTree(object obj)
        {
            View.CreateTreeWindow createTreeWindow = new View.CreateTreeWindow();

            if (createTreeWindow.ShowDialog() == true)
            {
                this.Tree = new Tree(createTreeWindow.Doubles);
                TreeString = Tree.GetString();
                MessageBox.Show("Your tree has created succesfully!","Succes",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            else MessageBox.Show("Creating failed","Error",MessageBoxButton.OK, MessageBoxImage.Error);

            IsCreated = true;
        }

        private void Clear(object obj)
        {
            Tree = null;
            TreeString = "";
        }

        private bool CanEdit(object arg)
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
