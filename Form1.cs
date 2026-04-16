using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FileCompare
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeListView(lvwLeftDir);
            InitializeListView(lvwRightDir);
        }

        private void InitializeListView(ListView lv)
        {
            lv.View = View.Details;
            lv.FullRowSelect = true;
            lv.GridLines = true;

            lv.Columns.Clear();
            lv.Columns.Add("이름", 300);   // Index 0
            lv.Columns.Add("크기", 100);   // Index 1
            lv.Columns.Add("수정일", 160); // Index 2
        }

        private void btnLeftDir_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtLeftDir.Text = dlg.SelectedPath;
                    PopulateListView(lvwLeftDir, dlg.SelectedPath);
                    CompareFiles();
                }
            }
        }

        private void btnRightDir_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtRightDir.Text = dlg.SelectedPath;
                    PopulateListView(lvwRightDir, dlg.SelectedPath);
                    CompareFiles();
                }
            }
        }

        private void PopulateListView(ListView lv, string folderPath)
        {
            lv.BeginUpdate();
            lv.Items.Clear();
            try
            {
                var dirs = Directory.EnumerateDirectories(folderPath)
                                    .Select(p => new DirectoryInfo(p)).OrderBy(d => d.Name);
                foreach (var d in dirs)
                {
                    var item = new ListViewItem(d.Name);
                    item.SubItems.Add("<DIR>"); // Index 1
                    item.SubItems.Add(d.LastWriteTime.ToString("g")); // Index 2
                    item.Tag = d;
                    lv.Items.Add(item);
                }

                var files = Directory.EnumerateFiles(folderPath)
                                     .Select(p => new FileInfo(p)).OrderBy(f => f.Name);
                foreach (var f in files)
                {
                    var item = new ListViewItem(f.Name);
                    item.SubItems.Add(f.Length.ToString("N0") + " 바이트"); // Index 1
                    item.SubItems.Add(f.LastWriteTime.ToString("g")); // Index 2
                    item.Tag = f;
                    lv.Items.Add(item);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { lv.EndUpdate(); }
        }

        private void CompareFiles()
        {
            // 한쪽이라도 비어있으면 비교 중단
            if (lvwLeftDir.Items.Count == 0 || lvwRightDir.Items.Count == 0) return;

            foreach (ListViewItem item in lvwLeftDir.Items) item.ForeColor = Color.Black;
            foreach (ListViewItem item in lvwRightDir.Items) item.ForeColor = Color.Black;

            foreach (ListViewItem leftItem in lvwLeftDir.Items)
            {
                if (leftItem.SubItems[1].Text == "<DIR>") continue;
                FileInfo lf = leftItem.Tag as FileInfo;
                if (lf == null) continue;

                ListViewItem rightItem = FindItemByName(lvwRightDir, lf.Name);
                if (rightItem != null)
                {
                    FileInfo rf = rightItem.Tag as FileInfo;
                    if (rf == null) continue;

                    if (lf.LastWriteTime == rf.LastWriteTime)
                    {
                        leftItem.ForeColor = Color.Black;
                        rightItem.ForeColor = Color.Black;
                    }
                    else if (lf.LastWriteTime > rf.LastWriteTime)
                    {
                        leftItem.ForeColor = Color.Red;
                        rightItem.ForeColor = Color.Gray;
                    }
                    else
                    {
                        leftItem.ForeColor = Color.Gray;
                        rightItem.ForeColor = Color.Red;
                    }
                }
                else { leftItem.ForeColor = Color.Purple; }
            }

            foreach (ListViewItem rightItem in lvwRightDir.Items)
            {
                if (rightItem.SubItems[1].Text == "<DIR>") continue;
                FileInfo rf = rightItem.Tag as FileInfo;
                if (rf == null) continue;

                if (FindItemByName(lvwLeftDir, rf.Name) == null)
                    rightItem.ForeColor = Color.Purple;
            }
        }

        private ListViewItem FindItemByName(ListView lv, string name)
        {
            foreach (ListViewItem item in lv.Items)
            {
                if (item.SubItems[1].Text != "<DIR>" && item.Text == name) return item;
            }
            return null;
        }

        // [과제3] 왼쪽 -> 오른쪽 복사 (>>> 버튼)
        private void btnCopyFromLeft_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(txtRightDir.Text)) return;

            if (lvwLeftDir.SelectedItems.Count == 0)
            {
                MessageBox.Show("복사할 파일을 선택해주세요!");
                return;
            }

            foreach (ListViewItem item in lvwLeftDir.SelectedItems)
            {
                if (item.SubItems[1].Text == "<DIR>") continue;
                FileInfo srcFile = item.Tag as FileInfo;
                if (srcFile != null)
                {
                    string destPath = Path.Combine(txtRightDir.Text, srcFile.Name);
                    CopyFileWithConfirmation(srcFile.FullName, destPath);
                }
            }
            PopulateListView(lvwRightDir, txtRightDir.Text);
            CompareFiles();
        }

        // [과제3] 오른쪽 -> 왼쪽 복사 (<<< 버튼)
        private void btnCopyFromRight_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(txtLeftDir.Text)) return;

            if (lvwRightDir.SelectedItems.Count == 0)
            {
                MessageBox.Show("복사할 파일을 선택해주세요!");
                return;
            }

            foreach (ListViewItem item in lvwRightDir.SelectedItems)
            {
                if (item.SubItems[1].Text == "<DIR>") continue;
                FileInfo srcFile = item.Tag as FileInfo;
                if (srcFile != null)
                {
                    string destPath = Path.Combine(txtLeftDir.Text, srcFile.Name);
                    CopyFileWithConfirmation(srcFile.FullName, destPath);
                }
            }
            PopulateListView(lvwLeftDir, txtLeftDir.Text);
            CompareFiles();
        }

        private void CopyFileWithConfirmation(string srcPath, string destPath)
        {
            try
            {
                if (File.Exists(destPath))
                {
                    FileInfo srcInfo = new FileInfo(srcPath);
                    FileInfo destInfo = new FileInfo(destPath);

                    if (srcInfo.LastWriteTime <= destInfo.LastWriteTime)
                    {
                        string msg = "대상 파일이 더 최신이거나 같습니다. 덮어쓰시겠습니까?";
                        if (MessageBox.Show(msg, "확인", MessageBoxButtons.YesNo) != DialogResult.Yes)
                            return;
                    }
                }
                File.Copy(srcPath, destPath, true);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}