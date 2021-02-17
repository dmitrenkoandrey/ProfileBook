using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Xamarin.Forms
{
    public class TreeViewNode : StackLayout
    {
        #region Image source for icons
        private DataTemplate expandButtonTemplate = null;

        #endregion

        #region Fields
        private TreeViewNode parentTreeViewItem;

        private DateTime expandButtonClickedTime;

        private readonly BoxView spacerBoxView = new BoxView();
        private readonly BoxView emptyBox = new BoxView { BackgroundColor = Color.Blue, Opacity = .5 };


        private const int expandButtonWidth = 32;
        private ContentView expandButtonContent = new ContentView();

        private readonly Grid mainGrid = new Grid
        {
            VerticalOptions = LayoutOptions.StartAndExpand,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            RowSpacing = 2
        };

        private readonly StackLayout contentStackLayout = new StackLayout { Orientation = StackOrientation.Horizontal };

        private readonly ContentView contentView = new ContentView
        {
            HorizontalOptions = LayoutOptions.FillAndExpand
        };

        private readonly StackLayout childrenStackLayout = new StackLayout
        {
            Orientation = StackOrientation.Vertical,
            Spacing = 0,
            IsVisible = false
        };

        private IList<TreeViewNode> children = new ObservableCollection<TreeViewNode>();
        private readonly TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
        private readonly TapGestureRecognizer expandButtonGestureRecognizer = new TapGestureRecognizer();

        private readonly TapGestureRecognizer doubleClickGestureRecognizer = new TapGestureRecognizer();
        #endregion

        #region Internal Fields
        internal readonly BoxView SelectionBoxView = new BoxView { Color = Color.Blue, Opacity = .5, IsVisible = false };
        #endregion

        #region Private Properties
        private TreeView parentTreeView => Parent?.Parent as TreeView;
        private double indentWidth => depth * spacerWidth;
        private int spacerWidth { get; } = 30;
        private int depth => ParentTreeViewItem?.depth + 1 ?? 0;

        private bool showExpandButtonIfEmpty = false;
        private Color selectedBackgroundColor = Color.Blue;
        private double selectedBackgroundOpacity = .3;
        #endregion

        #region Events
        public event EventHandler Expanded;

        /// <summary>
        /// Occurs when the user double clicks on the node
        /// </summary>
        public event EventHandler ItemDoubleTapped;

        //public event EventHandler ItemTypped;
        #endregion

        #region Protected Overrides
        protected override void OnParentSet()
        {
            base.OnParentSet();
            Render();
        }
        #endregion

        #region Public Properties

        public bool IsSelected
        {
            get => SelectionBoxView.IsVisible;
            set => SelectionBoxView.IsVisible = value;
        }
        public bool IsExpanded
        {
            get => childrenStackLayout.IsVisible;
            set
            {
                childrenStackLayout.IsVisible = value;

                Render();
                if (value)
                {
                    Expanded?.Invoke(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// set to true to show the expand button in case we need to poulate the child nodes on demand
        /// </summary>
        public bool ShowExpandButtonIfEmpty
        {
            get { return showExpandButtonIfEmpty; }
            set { showExpandButtonIfEmpty = value; }
        }

        /// <summary>
        /// set BackgroundColor when node is tapped/selected
        /// </summary>
        public Color SelectedBackgroundColor
        {
            get { return selectedBackgroundColor; }
            set { selectedBackgroundColor = value; }
        }

        /// <summary>
        /// SelectedBackgroundOpacity when node is tapped/selected
        /// </summary>
        public double SelectedBackgroundOpacity
        {
            get { return selectedBackgroundOpacity; }
            set { selectedBackgroundOpacity = value; }
        }

        /// <summary>
        /// customize expand icon based on isExpanded property and or data 
        /// </summary>
        public DataTemplate ExpandButtonTemplate
        {
            get { return expandButtonTemplate; }
            set { expandButtonTemplate = value; }
        }

        public View Content
        {
            get => contentView.Content;
            set => contentView.Content = value;
        }

        public new IList<TreeViewNode> Children
        {
            get => children;
            set
            {
                if (children is INotifyCollectionChanged notifyCollectionChanged)
                {
                    notifyCollectionChanged.CollectionChanged -= ItemsSource_CollectionChanged;
                }

                children = value;

                if (children is INotifyCollectionChanged notifyCollectionChanged2)
                {
                    notifyCollectionChanged2.CollectionChanged += ItemsSource_CollectionChanged;
                }

                TreeView.RenderNodes(children, childrenStackLayout, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset), this);

                Render();
            }
        }

        /// <summary>
        /// TODO: Remove this. We should be able to get the ParentTreeViewNode by traversing up through the Visual Tree by 'Parent', but this not working for some reason.
        /// </summary>
        public TreeViewNode ParentTreeViewItem
        {
            get => parentTreeViewItem;
            set
            {
                parentTreeViewItem = value;
                Render();
            }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Constructs a new TreeViewItem
        /// </summary>
        public TreeViewNode()
        {
            var itemsSource = (ObservableCollection<TreeViewNode>)children;
            itemsSource.CollectionChanged += ItemsSource_CollectionChanged;

            tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
            GestureRecognizers.Add(tapGestureRecognizer);

            mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            mainGrid.Children.Add(SelectionBoxView);

            contentStackLayout.Children.Add(spacerBoxView);
            contentStackLayout.Children.Add(expandButtonContent);
            contentStackLayout.Children.Add(contentView);

            SetExpandButtonContent(expandButtonTemplate);

            expandButtonGestureRecognizer.Tapped += ExpandButton_Tapped;
            expandButtonContent.GestureRecognizers.Add(expandButtonGestureRecognizer);

            doubleClickGestureRecognizer.NumberOfTapsRequired = 2;
            doubleClickGestureRecognizer.Tapped += ItemClick;
            contentView.GestureRecognizers.Add(doubleClickGestureRecognizer);


            mainGrid.Children.Add(contentStackLayout);
            mainGrid.Children.Add(childrenStackLayout, 0, 1);

            base.Children.Add(mainGrid);

            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.Start;

            Render();
        }

        //void _DoubleClickGestureRecognizer_Tapped(object sender, EventArgs e)
        //{
        //}


        #endregion

        #region Private Methods
        /// <summary>
        /// TODO: This is a little stinky...
        /// </summary>
        private void ChildSelected(TreeViewNode child)
        {
            //Um? How does this work? The method here is a private method so how are we calling it?
            ParentTreeViewItem?.ChildSelected(child);
            parentTreeView?.ChildSelected(child);
        }

        private void Render()
        {
            spacerBoxView.WidthRequest = indentWidth;

            if ((Children == null || Children.Count == 0) && !ShowExpandButtonIfEmpty)
            {
                SetExpandButtonContent(expandButtonTemplate);
                return;
            }

            SetExpandButtonContent(expandButtonTemplate);

            foreach (var item in Children)
            {
                item.Render();
            }
        }

        /// <summary>
        /// Use DataTemplae 
        /// </summary>
        private void SetExpandButtonContent(DataTemplate expandButtonTemplate)
        {
            if (expandButtonTemplate != null)
            {
                expandButtonContent.Content = (View)expandButtonTemplate.CreateContent();
            }
            else
            {
                expandButtonContent.Content = (View)new ContentView { Content = emptyBox };
            }
        }
        #endregion

        #region Event Handlers
        private void ExpandButton_Tapped(object sender, EventArgs e)
        {
            expandButtonClickedTime = DateTime.Now;
            IsExpanded = !IsExpanded;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //TODO: Hack. We don't want the node to become selected when we are clicking on the expanded button
            if (DateTime.Now - expandButtonClickedTime > new TimeSpan(0, 0, 0, 0, 50))
            {
                ChildSelected(this);
            }
        }

        private void ItemClick(object sender, EventArgs e)
        {
            ItemDoubleTapped?.Invoke(this, new EventArgs());
        }

        private void ItemsSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            TreeView.RenderNodes(children, childrenStackLayout, e, this);
            Render();
        }

        #endregion
    }
}