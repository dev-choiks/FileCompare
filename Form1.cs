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

            // 과제1에서 작성한 ListView 초기화 속성 적용
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

        // ---------------------------------------------------
        // [과제2] 왼쪽/오른쪽 폴더 선택 기능 구현 [3]
        // ---------------------------------------------------
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
                    CompareFiles(); // 파일 비교 색상 업데이트
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
                    CompareFiles(); // 파일 비교 색상 업데이트
                }
            }
        }

        // ---------------------------------------------------
        // [과제2] 하위 폴더 및 파일 목록 스트리밍(Enumerate) 읽기 [4-6]
        // ---------------------------------------------------
        private void PopulateListView(ListView lv, string folderPath)
        {
            lv.BeginUpdate();
            lv.Items.Clear();

            try
            {
                // 1. 디렉터리 추가
                var dirs = Directory.EnumerateDirectories(folderPath)
                                    .Select(p => new DirectoryInfo(p))
                                    .OrderBy(d => d.Name);

                foreach (var d in dirs)
                {
                    var item = new ListViewItem(d.Name);
                    item.SubItems.Add("<DIR>"); // 인덱스 1
                    item.SubItems.Add(d.LastWriteTime.ToString("g")); // 인덱스 2
                    lv.Items.Add(item);
                }

                // 2. 파일 추가 (GetFiles보다 메모리 효율이 높은 EnumerateFiles 사용)
                var files = Directory.EnumerateFiles(folderPath)
                                     .Select(p => new FileInfo(p))
                                     .OrderBy(f => f.Name);

                foreach (var f in files)
                {
                    var item = new ListViewItem(f.Name);
                    item.SubItems.Add(f.Length.ToString("N0") + " 바이트"); // 인덱스 1
                    item.SubItems.Add(f.LastWriteTime.ToString("g")); // 인덱스 2
                    item.Tag = f; // 비교를 위해 FileInfo 저장
                    lv.Items.Add(item);
                }

                // 컬럼 너비 자동 조정
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
        // [과제2] 파일 이름 및 날짜 비교, 색상 적용 [1, 7]
        // ---------------------------------------------------
        private void CompareFiles()
        {
            if (lvwLeftDir.Items.Count == 0 || lvwRightDir.Items.Count == 0) return;

            // 모든 아이템 색상 초기화 (비교 전 상태)
            foreach (ListViewItem item in lvwLeftDir.Items) item.ForeColor = Color.Black;
            foreach (ListViewItem item in lvwRightDir.Items) item.ForeColor = Color.Black;

            // 왼쪽 기준 오른쪽 파일 비교
            foreach (ListViewItem leftItem in lvwLeftDir.Items)
            {
                // 디렉터리는 비교에서 제외 (인덱스 8 -> 1로 수정)
                if (leftItem.SubItems[1].Text == "<DIR>") continue;

                FileInfo lf = leftItem.Tag as FileInfo;
                if (lf == null) continue;

                ListViewItem rightItem = FindItemByName(lvwRightDir, lf.Name);

                if (rightItem != null)
                {
                    FileInfo rf = rightItem.Tag as FileInfo;
                    if (rf == null) continue;

                    // 상태에 따른 색상 지정 (동일:검정, 최신:빨강, 과거:회색)
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
                    // 단독 파일 (오른쪽에 없음): 보라색
                    leftItem.ForeColor = Color.Purple;
                }
            }

            // 오른쪽 기준 단독 파일 색상 지정
            foreach (ListViewItem rightItem in lvwRightDir.Items)
            {
                // 디렉터리는 비교에서 제외 (인덱스 8 -> 1로 수정)
                if (rightItem.SubItems[1].Text == "<DIR>") continue;

                FileInfo rf = rightItem.Tag as FileInfo;
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
                // 디렉터리가 아닌 파일 중에서만 이름이 일치하는 것을 찾음
                if (item.SubItems[1].Text != "<DIR>" && item.Text == name)
                {
                    return item;
                }
            }
            return null;
        }
    }
}