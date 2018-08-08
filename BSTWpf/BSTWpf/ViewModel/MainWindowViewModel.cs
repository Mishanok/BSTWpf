using BSTWpf.ExtensionMethods;
using BSTWpf.Model;
using BSTWpf.Model.DBModel;
using System;
using System.Collections.ObjectModel;
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

        Tree selectedTree;
        string removeValue;
        string addValue;

        public ObservableCollection<Tree> Trees { get; private set; }
        public Tree SelectedTree
        {
            get { return selectedTree; }
            set { selectedTree = value; OnPropertyChanged("SelectedTree"); }
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
        public ICommand DeleteCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        public MainWindowViewModel()
        {
            Trees = new ObservableCollection<Tree>();
            Trees.Add(Tree.GetTree());
            AddCommand = new DelegateCommand(AddElementToTree, CanEdit);
            RemoveCommand = new DelegateCommand(RemoveElementFromTree, CanEdit);
            ClearCommand = new DelegateCommand(Clear);
            CreateCommand = new DelegateCommand(CreateTree);
            GetMinCommand = new DelegateCommand(GetMin, CanEdit);
            GetMaxCommand = new DelegateCommand(GetMax, CanEdit);
            ContainsCommand = new DelegateCommand(Contains, CanEdit);
            DeleteCommand = new DelegateCommand(DeleteTree, CanEdit);
            SaveCommand = new DelegateCommand(SaveChanges);
        }

        private void SaveChanges(object obj)
        {
            using (TreesContext context = new TreesContext())
            {
                foreach(var item in (ObservableCollection<Tree>)obj)
                {
                    context.Trees.Add(item);
                }

                context.SaveChanges();
            }
        }

        private void DeleteTree(object obj)
        {
            Trees.Remove((Tree)obj);
        }

        private void Contains(object obj)
        {
            if (SelectedTree == null) MessageBox.Show("No active tree", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                string result;

                View.ContainsWindow containsWindow = new View.ContainsWindow();

                if (containsWindow.ShowDialog() == true)
                {
                    result = (SelectedTree.Contains(containsWindow.Value) ? "Exists" : "Doesn`t exists!");
                    MessageBox.Show(result, "Result", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else if (containsWindow.DialogResult.HasValue && !containsWindow.DialogResult.Value)
                {
                    return;
                }
                else MessageBox.Show("Invalid Operation", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GetMax(object obj)
        {
            try
            {
                MessageBox.Show("Maximum: " + SelectedTree.GetMax(), "INFO", MessageBoxButton.OK);
            }
            catch (NullReferenceException) { MessageBox.Show("No active tree", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void GetMin(object obj)
        {
            try
            {
                MessageBox.Show("Minimum: " + SelectedTree.GetMin(), "INFO", MessageBoxButton.OK);
            }
            catch(NullReferenceException) { MessageBox.Show("No active tree", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void CreateTree(object obj)
        {
            View.CreateTreeWindow createTreeWindow = new View.CreateTreeWindow();

            if (createTreeWindow.ShowDialog() == true)
            {
                Trees.Add(new Tree(createTreeWindow.Doubles, createTreeWindow.Name));
                MessageBox.Show("Your tree has created succesfully!","Succes",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            else if(createTreeWindow.DialogResult.HasValue && !createTreeWindow.DialogResult.Value)
            {
                return;
            }
            else MessageBox.Show("Creating failed","Error",MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Clear(object obj)
        {
            SelectedTree = null;
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
                    this.SelectedTree.Remove(Convert.ToDouble(obj));

                SelectedTree.TreeString = null;
                SelectedTree.TreeString = SelectedTree.GetString();
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
                this.SelectedTree.AddItem(Convert.ToDouble(obj));
                SelectedTree.TreeString = SelectedTree.GetString();
            }
            catch (NullReferenceException) { MessageBox.Show("You need to create tree first", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (FormatException) { MessageBox.Show("Incorect input","Error",MessageBoxButton.OK, MessageBoxImage.Error); }
            //catch (Exception) { MessageBox.Show("Unknown error", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
