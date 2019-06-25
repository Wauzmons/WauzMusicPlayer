using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WauzMusicPlayer
{
    public partial class MusicPlayerForm : Form
    {

        private void BuildRecursiveFileExplorerTreeTrunk()
        {
            FileExplorerTree.Nodes.Clear();
            fileExplorerMap.Clear();

            DirectoryInfo rootDirectoryInfo = new DirectoryInfo(musicFolderPath);
            if (rootDirectoryInfo.Exists)
            {
                TreeNode rootNode = FileExplorerTree.Nodes.Add(rootDirectoryInfo.Name);
                fileExplorerMap.Add(rootNode, rootDirectoryInfo);
                rootNode.ImageIndex = 1;
                rootNode.SelectedImageIndex = 1;
                rootNode.ContextMenuStrip = DirectoryNodeMenu;
                BuildRecursiveFileExplorerTreeBranch(rootNode.Nodes, rootDirectoryInfo);
            }

            DirectoryInfo streamDirectoryInfo = new DirectoryInfo(streamPath);
            if (streamDirectoryInfo.Exists)
            {
                TreeNode streamNode = FileExplorerTree.Nodes.Add("Streams");
                cachedStreamNode = streamNode;
                streamNode.ImageIndex = 3;
                streamNode.SelectedImageIndex = 3;
                streamNode.ContextMenuStrip = StreamNodeMenu;
                BuildRecursiveFileExplorerTreeBranch(streamNode.Nodes, streamDirectoryInfo);
            }
        }

        private TreeNode cachedStreamNode;

        public void BuildStreamTreeBranch()
        {
            BuildRecursiveFileExplorerTreeBranch(cachedStreamNode.Nodes, new DirectoryInfo(streamPath));
        }

        public void BuildRecursiveFileExplorerTreeBranch(TreeNodeCollection parentNode, DirectoryInfo parentDirectoryInfo)
        {
            BuildRecursiveFileExplorerTreeBranch(parentNode, parentDirectoryInfo, false);
        }

        private bool BuildRecursiveFileExplorerTreeBranch(TreeNodeCollection parentNode, DirectoryInfo parentDirectoryInfo, Boolean dontFilter)
        {
            parentNode.Clear();

            if (!parentDirectoryInfo.Exists)
                return false;

            if (String.IsNullOrWhiteSpace(fileTreeFilter))
                dontFilter = true;

            bool filterMatch = false;

            foreach (DirectoryInfo directoryInfo in parentDirectoryInfo.GetDirectories())
            {
                TreeNode directoryNode = parentNode.Add(directoryInfo.Name);
                fileExplorerMap.Add(directoryNode, directoryInfo);
                directoryNode.ImageIndex = 1;
                directoryNode.SelectedImageIndex = 1;
                directoryNode.ContextMenuStrip = DirectoryNodeMenu;

                if (dontFilter)
                    BuildRecursiveFileExplorerTreeBranch(directoryNode.Nodes, directoryInfo);
                else
                {
                    bool parentFilterMatch = directoryNode.Name.ToUpper().Contains(fileTreeFilter.ToUpper());
                    bool childFilterMatch = BuildRecursiveFileExplorerTreeBranch(directoryNode.Nodes, directoryInfo, parentFilterMatch);
                    if (childFilterMatch)
                        filterMatch = true;
                    else
                    {
                        parentNode.Remove(directoryNode);
                        fileExplorerMap.Remove(directoryNode);
                    }
                }
            }

            foreach (FileInfo fileInfo in parentDirectoryInfo.GetFiles())
            {
                if (!validAudioFormats.Contains(fileInfo.Extension))
                    continue;

                if (!dontFilter && !fileInfo.Name.ToUpper().Contains(fileTreeFilter.ToUpper()))
                    continue;

                Task task = Task.Factory.StartNew(() => SerializeFile(fileInfo));
                TreeNode fileNode = parentNode.Add(fileInfo.Name);
                fileExplorerMap.Add(fileNode, fileInfo);
                fileNode.ImageIndex = fileInfo.Extension.Equals(".wzst") ? 3 : 2;
                fileNode.SelectedImageIndex = fileInfo.Extension.Equals(".wzst") ? 3 : 2;
                fileNode.ContextMenuStrip = FileNodeMenu;
                filterMatch = true;
            }

            return dontFilter || filterMatch;
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!SearchTextBox.Text.Equals(fileTreeFilter))
            {
                fileTreeFilter = SearchTextBox.Text;
                BuildRecursiveFileExplorerTreeTrunk();
                if (!String.IsNullOrWhiteSpace(fileTreeFilter))
                    FileExplorerTree.ExpandAll();
            }
        }

        private string SerializeFile(FileInfo fileInfo)
        {
            //string uuid = SongTag.Serialize(fileInfo.FullName, true);
            //if(!string.IsNullOrWhiteSpace(uuid))
            //    serializedFileMap.Add(uuid, fileInfo);
            //return uuid;
            return "";
        }

        private void SelectNode(TreeNode node)
        {
            FileExplorerTree.SelectedNode = node;
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                AddSelectedToQueue(false);
            }
        }

        private void FileNodeMenu_Opening(object sender, CancelEventArgs e)
        {
            FileInfo fileInfo = (FileInfo)GetFileSystemInfo(FileExplorerTree.SelectedNode);

            bool isLocalFile = !fileInfo.Extension.Equals(".wzst");

            addToPlaylistToolStripMenuItem.Visible = isLocalFile;
            showTagEditorToolStripMenuItem.Visible = isLocalFile;
            toolStripSeparator9.Visible = isLocalFile;
        }

        private FileSystemInfo GetFileSystemInfo(TreeNode node)
        {
            return fileExplorerMap.ContainsKey(node) ? fileExplorerMap[node] : null;
        }

    }
}
