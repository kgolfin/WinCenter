using Library.SaveLoadConfig;
using System;
using System.Security.Principal;
using System.Windows.Forms;
using WindowCenteringLib;
using static Library.SaveLoadConfig.LoadSaveConfiguration;

namespace WinCenter
{
    public partial class Form1 : Form
    {
        public Form1()
        { 
            // System.Diagnostics.Process.GetProcessesByName에서 이름을 찾기 위해서 이 프로세스의 이름을 winCenter에 저장한다 
            string winCener = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            // 이 프로그램을 두번 실행하지 못하게 한다.
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName(winCener);
            if (processes.Length > 1)
            {
                MessageBox.Show("Already running.");
                Environment.Exit(0);
            }

            // 관리자 권한으로 실행 중인지 확인
#if !DEBUG 
            if (!IsRunAsAdmin())
            {
                // 관리자 권한으로 실행 한다.
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.WorkingDirectory = Environment.CurrentDirectory;
                startInfo.FileName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                startInfo.Verb = "runas"; // 관리자 권한으로 실행
                try
                {
                    System.Diagnostics.Process.Start(startInfo);
                }
                catch (System.ComponentModel.Win32Exception)
                {
                    // 사용자가 관리자 권한 요청을 거부한 경우
                    MessageBox.Show("Administrator permission is required to run this application.");
                    Environment.Exit(0);
                }

                Environment.Exit(0); // 현재 인스턴스를 종료
            }
#endif

            InitializeComponent();
        }

        private bool IsRunAsAdmin()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        private void buttonWindowCenter_Click(object sender, EventArgs e)
        {
            string windowName = textBoxWndowName.Text.Trim();
            int centeredCount;
            /// 윈도우 이름으로 중앙 배치
            if (radioButtonWindowName.Checked) centeredCount = WindowCentering.CenterWindowsByName(windowName);
            /// 프로세스 이름으로 중앙 배치  
            if (radioButtonProcessName.Checked) centeredCount = WindowCentering.CenterWindowsByProcess(windowName);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SaveLoadConrolsProperty(SaveLoadSelection.Load);
            // 폼의 사이즈를 변경하지 못하게 한다.
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            OnOffAlwaysOnTop();
        }

        private void OnOffAlwaysOnTop()
        {
            if (checkBoxAlwaysOnTop.Checked)
            {
                this.TopMost = true;
            }
            else
            {
                this.TopMost = false;
            }
        }

        /// <summary>
        /// 폼의 컨트롤을 저장하거나 불러온다.
        /// </summary>
        /// <param name="load"></param>
        private void SaveLoadConrolsProperty(SaveLoadSelection load)
        {
            // 저장된 파라메터를 읽고 쓰는 클래스를 생성한다.
            LoadSaveConfiguration para = new LoadSaveConfiguration();
            // 저장된 파라메터를 읽거나 쓴다.
            foreach (Control control in this.Controls)
            {
                SaveLoadControlProperties(control, para, load);
            }
        }

        /// <summary>
        /// 컨트롤의 속성을 저장하거나 불러온다.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="para"></param>
        /// <param name="load"></param>
        private void SaveLoadControlProperties(Control control, LoadSaveConfiguration para, SaveLoadSelection load)
        {
            if (control is CheckBox checkbox)
            {
                if (load == SaveLoadSelection.Load)
                    para.CheckBoxLoadConfiguration(checkbox, false);
                else
                    para.CheckBoxSaveConfiguration(checkbox);
            }
            else if (control is TextBox textBox)
            {
                if (load == SaveLoadSelection.Load)
                    para.TextBoxLoadConfiguration(textBox, "");
                else
                    para.TextBoxSaveConfiguration(textBox);
            }
            else if (control is RadioButton radioButton)
            {
                if (load == SaveLoadSelection.Load)
                    para.RadioButtonLoadConfiguration(radioButton, false);
                else
                    para.RadioButtonSaveConfiguration(radioButton);
            }
            else if (control is GroupBox groupBox)
            {
                // 그룹박스 안의 컨트롤도 검색
                foreach (Control innerControl in groupBox.Controls)
                {
                    SaveLoadControlProperties(innerControl, para, load);
                }
            }
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveLoadConrolsProperty(SaveLoadSelection.Save);
        }

        private void checkBoxAlwaysOnTop_CheckedChanged(object sender, EventArgs e)
        {
            OnOffAlwaysOnTop();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            textBoxWndowName.Text = "vp*";
            radioButtonProcessName.Checked = true;  
        }

        private void radioButtonProcessName_MouseHover(object sender, EventArgs e)
        {
            SetTollTip(radioButtonProcessName, "Clik this if you want to select processname (file.name)"); 
        }

        private void SetTollTip(Control toolTipControl, string message)
        { 
            // radioButtonProcessName에 툴팁을 설정한다.
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(toolTipControl, message);
        }
    }
}
