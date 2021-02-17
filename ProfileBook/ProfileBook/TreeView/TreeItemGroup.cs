using System;
using System.Collections.Generic;

namespace ProfileBook.TreeView
{
    [Serializable]
    public class TreeItemGroup
    {
        public List<TreeItemGroup> Children { get; } = new List<TreeItemGroup>();
        public List<TreeItem> TreeItems { get; } = new List<TreeItem>();
        public string Name { get; set; }

        public object ItemDataSource { get; set; }
    }

    [Serializable]
    public class TreeItem
    {
        public object ItemDataSource { get; set; }
        public string Name { get; set; }
    }

    public interface ITreeFolder
    {
        Guid Id { get; set; }

        Guid? Parent_Id { get; set; }

        string Name { get; set; }

        IList<ITreeItem> Items { get; }
    }

    public interface ITreeItem
    {
        string Name { get; }
    }
}
