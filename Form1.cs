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
            lv.Columns.Add("이름", 300);
            lv.Columns.Add("크기", 100);
            lv.Columns.Add("수정일", 160);
        }

        private void btnLeftDir_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "폴더를 선택하세요.";
                if (!string.IsNullOrWhiteSpace(txtLeftDir.Text) && Directory.Exists(txtLeftDir.Text))
                {
                    dlg.SelectedPath = txtLeftDir.Text;
                }

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
                dlg.Description = "폴더를 선택하세요.";
                if (!string.IsNullOrWhiteSpace(txtRightDir.Text) && Directory.Exists(txtRightDir.Text))
                {
                    dlg.SelectedPath = txtRightDir.Text;
                }

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
                // 1. 폴더 목록 추가
                var dirs = Directory.EnumerateDirectories(folderPath)
                                    .Select(p => new DirectoryInfo(p))
                                    .OrderBy(d => d.Name);

                foreach (var d in dirs)
                {
                    var item = new ListViewItem(d.Name);
                    item.SubItems.Add("<DIR>");
                    item.SubItems.Add(d.LastWriteTime.ToString("g"));
                    item.Tag = d; // 과제4: 폴더 정보(DirectoryInfo) 저장
                    lv.Items.Add(item);
                }

                // 2. 파일 목록 추가
                var files = Directory.EnumerateFiles(folderPath)
                                     .Select(p => new FileInfo(p))
                                     .OrderBy(f => f.Name);

                foreach (var f in files)
                {
                    var item = new ListViewItem(f.Name);
                    item.SubItems.Add(f.Length.ToString("N0") + " 바이트");
                    item.SubItems.Add(f.LastWriteTime.ToString("g"));
                    item.Tag = f; // 파일 정보(FileInfo) 저장
                    lv.Items.Add(item);
                }

                for (int i = 0; i < lv.Columns.Count; i++)
                {
                    lv.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
                }
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show(this, "폴더를 찾을 수 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show(this, "입출력 오류: " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                lv.EndUpdate();
            }
        }

        // ---------------------------------------------------
        // [과제4 핵심] 폴더와 파일을 모두 비교하도록 수정된 메서드
        // ---------------------------------------------------
        private void CompareFiles()
        {
            if (lvwLeftDir.Items.Count == 0 || lvwRightDir.Items.Count == 0) return;

            // 초기화
            foreach (ListViewItem item in lvwLeftDir.Items) item.ForeColor = Color.Black;
            foreach (ListViewItem item in lvwRightDir.Items) item.ForeColor = Color.Black;

            // 1. 왼쪽 리스트 기준 비교
            foreach (ListViewItem leftItem in lvwLeftDir.Items)
            {
                // [수정점] 폴더 건너뛰기 조건 삭제 -> 파일과 폴더 모두 FileSystemInfo로 캐스팅
                FileSystemInfo lf = leftItem.Tag as FileSystemInfo;
                if (lf == null) continue;

                ListViewItem rightItem = FindItemByName(lvwRightDir, lf.Name);

                if (rightItem != null)
                {
                    FileSystemInfo rf = rightItem.Tag as FileSystemInfo;
                    if (rf == null) continue;

                    // 상태 결정 및 색상 적용 (폴더의 수정 시간도 동일하게 비교)
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
                else
                {
                    leftItem.ForeColor = Color.Purple; // 단독 파일/폴더
                }
            }

            // 2. 오른쪽 리스트 기준 단독 파일/폴더 찾기
            foreach (ListViewItem rightItem in lvwRightDir.Items)
            {
                FileSystemInfo rf = rightItem.Tag as FileSystemInfo;
                if (rf == null) continue;

                ListViewItem leftItem = FindItemByName(lvwLeftDir, rf.Name);
                if (leftItem == null)
                {
                    rightItem.ForeColor = Color.Purple;
                }
            }
        }

        private ListViewItem FindItemByName(ListView lv, string name)
        {
            foreach (ListViewItem item in lv.Items)
            {
                // [수정점] 폴더도 정상적으로 찾아낼 수 있도록 조건 변경
                if (item.Text == name) return item;
            }
            return null;
        }

        // ---------------------------------------------------
        // [과제4 핵심] 폴더 복사 로직 추가 (>>> 버튼)
        // ---------------------------------------------------
        private void btnCopyFromLeft_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRightDir.Text) || !Directory.Exists(txtRightDir.Text))
            {
                MessageBox.Show("복사할 대상(오른쪽) 폴더를 먼저 선택하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (ListViewItem item in lvwLeftDir.SelectedItems)
            {
                FileSystemInfo srcItem = item.Tag as FileSystemInfo;
                if (srcItem == null) continue;

                string destPath = Path.Combine(txtRightDir.Text, srcItem.Name);

                // 선택된 것이 폴더인지 파일인지 구분하여 처리
                if (srcItem is DirectoryInfo)
                {
                    CopyDirectory(srcItem.FullName, destPath);
                }
                else if (srcItem is FileInfo)
                {
                    CopyFileWithConfirmation(srcItem.FullName, destPath);
                }
            }

            PopulateListView(lvwRightDir, txtRightDir.Text);
            CompareFiles();
        }

        private void btnCopyFromRight_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLeftDir.Text) || !Directory.Exists(txtLeftDir.Text))
            {
                MessageBox.Show("복사할 대상(왼쪽) 폴더를 먼저 선택하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (ListViewItem item in lvwRightDir.SelectedItems)
            {
                FileSystemInfo srcItem = item.Tag as FileSystemInfo;
                if (srcItem == null) continue;

                string destPath = Path.Combine(txtLeftDir.Text, srcItem.Name);

                if (srcItem is DirectoryInfo)
                {
                    CopyDirectory(srcItem.FullName, destPath);
                }
                else if (srcItem is FileInfo)
                {
                    CopyFileWithConfirmation(srcItem.FullName, destPath);
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
                        string msg = $"대상에 동일한 이름의 파일이 이미 있습니다.\n" +
                                     $"대상 파일이 더 최신 파일(또는 동일)입니다. 그래도 덮어쓰시겠습니까?\n\n" +
                                     $"원본: {srcInfo.LastWriteTime}\n" +
                                     $"대상: {destInfo.LastWriteTime}";

                        var result = MessageBox.Show(msg, "덮어쓰기 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result != DialogResult.Yes)
                        {
                            return;
                        }
                    }
                }
                File.Copy(srcPath, destPath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"파일 복사 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------------------------------------------------
        // [과제4 추가] 하위 폴더의 모든 내용(파일+폴더)을 복사하는 재귀 메서드
        // ---------------------------------------------------
        private void CopyDirectory(string sourceDir, string destDir)
        {
            try
            {
                // 대상 폴더가 없으면 새로 생성
                if (!Directory.Exists(destDir))
                {
                    Directory.CreateDirectory(destDir);
                }

                // 해당 폴더 안의 모든 파일들을 복사 (덮어쓰기 확인 로직 재사용)
                foreach (string file in Directory.GetFiles(sourceDir))
                {
                    string destFile = Path.Combine(destDir, Path.GetFileName(file));
                    CopyFileWithConfirmation(file, destFile);
                }

                // 해당 폴더 안의 하위 폴더들에 대해 재귀적으로 복사 수행
                foreach (string dir in Directory.GetDirectories(sourceDir))
                {
                    string destSubDir = Path.Combine(destDir, Path.GetFileName(dir));
                    CopyDirectory(dir, destSubDir);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"폴더 복사 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}