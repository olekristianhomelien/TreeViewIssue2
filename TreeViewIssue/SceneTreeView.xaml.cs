using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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

namespace TreeViewIssue
{
    /// <summary>
    /// Interaction logic for TreeView.xaml
    /// </summary>
    public class SimpleFileSceneElement
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Visibility ApplyElementCheckboxVisability { get; set; } = Visibility.Visible;

        string _displayName = "";
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                NotifyPropertyChanged();
            }
        }

        bool _isChecked = false;
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<SimpleFileSceneElement> Children { get; set; } = new ObservableCollection<SimpleFileSceneElement>();

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class SimpleSceneGraphViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<SimpleFileSceneElement> _sceneGraphRootNodes = new ObservableCollection<SimpleFileSceneElement>();
        public ObservableCollection<SimpleFileSceneElement> SceneGraphRootNodes { get { return _sceneGraphRootNodes; } set { _sceneGraphRootNodes = value; } }


        SimpleFileSceneElement _selectedNode;
        public SimpleFileSceneElement SelectedNode { get { return _selectedNode; } set { _selectedNode = value; } }

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public partial class SceneTreeView : UserControl
    {

        SimpleSceneGraphViewModel view = new SimpleSceneGraphViewModel();
        public SceneTreeView()
        {


            var root = new SimpleFileSceneElement() { DisplayName = "InitailValue_A" };
            root.Children.Add(new SimpleFileSceneElement() { DisplayName = "InitailValue_B" });
            root.Children.Add(new SimpleFileSceneElement() { DisplayName = "InitailValue_C" });
            view.SceneGraphRootNodes.Add(root);

            InitializeComponent();

            this.DataContextChanged += SceneTreeView_DataContextChanged;
            DataContext = view;

        }

        private void SceneTreeView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void TreeViewItem_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //var treeView = sender as TreeView;
            //var item = treeView.SelectedItem as SimpleFileSceneElement;

            var item = view.SceneGraphRootNodes.First();
            if (item != null)
            {
                item.IsChecked = !item.IsChecked;
                item.DisplayName = "NewDebugName";
            }
            //e.Handled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var item = view.SceneGraphRootNodes.First();
            if (item != null)
            {
                item.IsChecked = !item.IsChecked;
                item.DisplayName = "NewDebugName";
            }
        }
    }
}
