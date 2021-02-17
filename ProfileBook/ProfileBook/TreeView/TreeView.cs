using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using ProfileBook.TreeView;

namespace Xamarin.Forms
{
    public class TreeView : ScrollView
    {
        #region Fields
        private readonly StackLayout stackLayout = new StackLayout { Orientation = StackOrientation.Vertical };

        //TODO: This initialises the list, but there is nothing listening to INotifyCollectionChanged so no nodes will get rendered
        private IList<TreeViewNode> rootNodes = new ObservableCollection<TreeViewNode>();
        private TreeViewNode selectedItem;
        #endregion

        #region Public Properties

        /// <summary>
        /// The item that is selected in the tree
        /// TODO: Make this two way - and maybe eventually a bindable property
        /// </summary>
        public TreeViewNode SelectedItem
        {
            get => selectedItem;

            set
            {
                if (selectedItem == value)
                {
                    return;
                }

                if (selectedItem != null)
                {
                    selectedItem.IsSelected = false;
                }

                selectedItem = value;

                SelectedItemChanged?.Invoke(this, new EventArgs());
            }
        }

        private List<ITreeFolder> departments;
        public List<ITreeFolder> TreeFolders
        {
            get => departments;

            set
            {
                departments = value;

                TreeItemGroup treeItem = new TreeItemGroup();
                ConvertData(departments, treeItem.Children, null);

                var rootNodes = ProcessTreeItemGroups(treeItem);

                RootNodes = rootNodes;

            }
        }

        private ObservableCollection<TreeViewNode> ProcessTreeItemGroups(TreeItemGroup treeItemGroups)
        {
            var rootNodes = new ObservableCollection<TreeViewNode>();

            foreach (var treeItemGroup in treeItemGroups.Children.OrderBy(xig => xig.Name))
            {
                var label = new Label
                {
                    VerticalOptions = LayoutOptions.Center,
                    TextColor = Color.Black
                };
                label.SetBinding(Label.TextProperty, "Name");

                var folderLabel_tap = new TapGestureRecognizer();
                folderLabel_tap.CommandParameter = treeItemGroup;

                folderLabel_tap.Tapped += (s, e) =>
                {
                    FolderTaped?.Invoke(s, e);
                };
                label.GestureRecognizers.Add(folderLabel_tap);

                var groupTreeViewNode = CreateTreeViewNode(treeItemGroup, label, false);

                rootNodes.Add(groupTreeViewNode);

                groupTreeViewNode.Children = ProcessTreeItemGroups(treeItemGroup);

                foreach (var treeItem in treeItemGroup.TreeItems)
                {
                    CreateTreeItem(groupTreeViewNode.Children, treeItem);
                }
            }

            return rootNodes;
        }
        private void CreateTreeItem(IList<TreeViewNode> children, TreeItem treeItem)
        {
            var label = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                TextColor = Color.Black
            };
            label.SetBinding(Label.TextProperty, "Name");
            var itemLabel_tap = new TapGestureRecognizer();
            itemLabel_tap.CommandParameter = treeItem;
            itemLabel_tap.Tapped += (s, e) =>
            {
                ItemTapped?.Invoke(s, e);
            };
            label.GestureRecognizers.Add(itemLabel_tap);


            var treeItemTreeViewNode = CreateTreeViewNode(treeItem, label, true);
            children.Add(treeItemTreeViewNode);
        }

        private TreeViewNode CreateTreeViewNode(object bindingContext, Label label, bool isItem)
        {
            var node = new TreeViewNode
            {
                BindingContext = bindingContext,
                Content = new StackLayout
                {
                    Children =
                        {
                            new ResourceImage
                            {
                                Resource = isItem? "XamarinTest.Resource.Item.png" :"XamarinTest.Resource.FolderOpen.png" ,
                                HeightRequest= 16,
                                WidthRequest = 16
                            },
                            label
                        },
                    Orientation = StackOrientation.Horizontal
                }
            };

            //set DataTemplate for expand button content
            node.ExpandButtonTemplate = new DataTemplate(() => new ExpandButtonContent { BindingContext = node });

            return node;
        }

        private void ConvertData(List<ITreeFolder> Folders, List<TreeItemGroup> treeItemGroups, Guid? Parent_Id)
        {
            foreach (var itemSourseFolder in Folders.Where(c => c.Parent_Id == Parent_Id))
            {
                var folder = new TreeItemGroup() { Name = itemSourseFolder.Name, ItemDataSource = itemSourseFolder };
                treeItemGroups.Add(folder);
                foreach (var item in itemSourseFolder.Items)
                {
                    folder.TreeItems.Add(new TreeItem() { Name = item.Name, ItemDataSource = item });
                }

                ConvertData(Folders, folder.Children, itemSourseFolder.Id);
            }

            return;
        }


        public IList<TreeViewNode> RootNodes
        {
            get => rootNodes;
            set
            {
                rootNodes = value;

                if (value is INotifyCollectionChanged notifyCollectionChanged)
                {
                    notifyCollectionChanged.CollectionChanged += (s, e) =>
                    {
                        RenderNodes(rootNodes, stackLayout, e, null);
                    };
                }

                RenderNodes(rootNodes, stackLayout, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset), null);
            }
        }

        #endregion

        #region Events
        /// <summary>
        /// Occurs when the user selects a TreeViewItem
        /// </summary>
        public event EventHandler SelectedItemChanged;

        public event EventHandler FolderTaped;

        public event EventHandler ItemTapped;


        #endregion

        #region Constructor
        public TreeView()
        {
            Content = stackLayout;
        }
        #endregion

        #region Private Methods
        private void RemoveSelectionRecursive(IEnumerable<TreeViewNode> nodes)
        {
            foreach (var treeViewItem in nodes)
            {
                if (treeViewItem != SelectedItem)
                {
                    treeViewItem.IsSelected = false;
                }

                RemoveSelectionRecursive(treeViewItem.Children);
            }
        }
        #endregion

        #region Private Static Methods
        private static void AddItems(IEnumerable<TreeViewNode> childTreeViewItems, StackLayout parent, TreeViewNode parentTreeViewItem)
        {
            foreach (var childTreeNode in childTreeViewItems)
            {
                if (!parent.Children.Contains(childTreeNode))
                {
                    parent.Children.Add(childTreeNode);
                }

                childTreeNode.ParentTreeViewItem = parentTreeViewItem;
            }
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// TODO: A bit stinky but better than bubbling an event up...
        /// </summary>
        internal void ChildSelected(TreeViewNode child)
        {
            SelectedItem = child;
            child.IsSelected = true;
            child.SelectionBoxView.Color = child.SelectedBackgroundColor;
            child.SelectionBoxView.Opacity = child.SelectedBackgroundOpacity;
            RemoveSelectionRecursive(RootNodes);
        }
        #endregion

        #region Internal Static Methods
        internal static void RenderNodes(IEnumerable<TreeViewNode> childTreeViewItems, StackLayout parent, NotifyCollectionChangedEventArgs e, TreeViewNode parentTreeViewItem)
        {
            if (e.Action != NotifyCollectionChangedAction.Add)
            {
                //TODO: Reintate this...
                //parent.Children.Clear();
                AddItems(childTreeViewItems, parent, parentTreeViewItem);
            }
            else
            {
                AddItems(e.NewItems.Cast<TreeViewNode>(), parent, parentTreeViewItem);
            }
        }
        #endregion

        public class ExpandButtonContent : ContentView
        {

            protected override void OnBindingContextChanged()
            {
                base.OnBindingContextChanged();

                var node = (BindingContext as TreeViewNode);
                bool isLeafNode = (node.Children == null || node.Children.Count == 0);

                //empty nodes have no icon to expand unless showExpandButtonIfEmpty is et to true which will show the expand
                //icon can click and populated node on demand propably using the expand event.
                if ((isLeafNode) && !node.ShowExpandButtonIfEmpty)
                {
                    Content = new ResourceImage
                    {
                        Resource = isLeafNode ? "ProfileBook.Resource.Blank.png" : "ProfileBook.Resource.FolderOpen.png",
                        HeightRequest = 16,
                        WidthRequest = 16
                    };
                }
                else
                {
                    Content = new ResourceImage
                    {
                        Resource = node.IsExpanded ? "ProfileBook.Resource.OpenGlyph.png" : "ProfileBook.Resource.CollpsedGlyph.png",
                        HeightRequest = 16,
                        WidthRequest = 16
                    };
                }
            }
        }
    }
}