namespace FileCompare
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // [과제1 기능] 프로그램 시작 시 양쪽 ListView 기본 세팅 적용
            InitializeListView(lvwLeftDir);
            InitializeListView(lvwRightDir);
        }

        // ---------------------------------------------------
        // [과제1 기능] ListView의 열(Column)과 보기 속성을 초기화하는 메서드
        // ---------------------------------------------------
        private void InitializeListView(ListView lv)
        {
            // 강의 교안에 따른 ListView 속성 설정
            lv.View = View.Details;      // 표(상세히) 형태로 보기 설정
            lv.FullRowSelect = true;     // 항목 클릭 시 한 줄 전체가 선택되도록 설정
            lv.GridLines = true;         // 엑셀처럼 뚜렷한 구분선 표시

            // 기존 열이 있다면 초기화한 뒤 새로 3개의 열을 추가합니다.
            lv.Columns.Clear();
            lv.Columns.Add("이름", 300);   // 첫 번째 열: 파일 이름
            lv.Columns.Add("크기", 100);   // 두 번째 열: 파일 크기
            lv.Columns.Add("수정일", 160); // 세 번째 열: 마지막 수정 날짜
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
