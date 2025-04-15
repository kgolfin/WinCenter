using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowCenteringLib
{
    public class WindowCentering
    {
        // Win32 API 함수 선언
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        private static extern int EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, System.Text.StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        // EnumWindows 콜백 대리자
        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        // RECT 구조체 정의
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        /// <summary>
        /// 윈도우 이름 패턴에 맞는 모든 윈도우를 화면 중앙에 배치합니다.
        /// </summary>
        /// <param name="windowNamePattern">윈도우 이름 패턴 (와일드카드: * 와 ? 사용 가능)</param>
        /// <returns>중앙 배치된 윈도우 수</returns>
        public static int CenterWindowsByName(string windowNamePattern)
        {
            if (string.IsNullOrEmpty(windowNamePattern))
            {
                int windowCenterCounter = CenterAllWindow();
                return windowCenterCounter;
            }
            List<IntPtr> matchedWindows = new List<IntPtr>();

            // 와일드카드를 정규표현식으로 변환
            string pattern = "^" + Regex.Escape(windowNamePattern)
                .Replace("\\*", ".*")
                .Replace("\\?", ".") + "$";

            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            // 모든 윈도우 열거
            EnumWindows(delegate (IntPtr hWnd, IntPtr lParam)
            {
                // 윈도우 이름 가져오기
                System.Text.StringBuilder windowText = new System.Text.StringBuilder(256);
                if (IsWindowVisible(hWnd) && GetWindowText(hWnd, windowText, windowText.Capacity) > 0)
                {
                    // 패턴과 일치하는지 확인
                    if (regex.IsMatch(windowText.ToString()))
                    {
                        matchedWindows.Add(hWnd);
                    }
                }
                return true;
            }, IntPtr.Zero);

            // 일치하는 윈도우가 없는 경우
            if (matchedWindows.Count == 0)
            {
                return 0;
            }

            int centeredCount = 0;

            // 모든 일치하는 윈도우를 중앙에 배치
            foreach (IntPtr hWnd in matchedWindows)
            {
                if (CenterWindow(hWnd))
                {
                    // 윈도우 이름을 콘솔에 출력
                    //System.Text.StringBuilder windowText = new System.Text.StringBuilder(256);
                    //GetWindowText(hWnd, windowText, windowText.Capacity);
                    //Console.WriteLine($"Centered Window !!! : {windowText}");

                    centeredCount++;
                }
            }

            return centeredCount;
        }
 
        /// <summary>
        /// 모든 윈도우를 화면 중앙에 배치하고, 중앙에 배치된 윈도우 수를 반환합니다.
        /// </summary>
        /// <returns>중앙에 배치된 윈도우 수</returns>
        public static int CenterAllWindow()
        {
            int centeredCount = 0;

            // 모든 윈도우 열거
            EnumWindows(delegate (IntPtr hWnd, IntPtr lParam)
            {
                // 윈도우 이름 가져오기
                System.Text.StringBuilder windowText = new System.Text.StringBuilder(256);
                if (IsWindowVisible(hWnd) && GetWindowText(hWnd, windowText, windowText.Capacity) > 0)
                {
                    // 윈도우를 중앙에 배치
                    if (CenterWindow(hWnd))
                    {
                        centeredCount++;
                    }
                }
                return true; // 계속 열거
            }, IntPtr.Zero);

            // 중앙에 배치된 윈도우 수 반환
            return centeredCount;
        }

        /// <summary>
        /// 특정 프로세스 이름 패턴에 맞는 모든 프로세스를 찾고, 해당 프로세스의 자식 창들을 화면 중앙에 배치하고
        /// 중앙에 배치된 윈도우 수를 반환합니다.
        /// </summary>
        /// <param name="processNamePattern">프로세스 이름 패턴 (와일드카드: * 와 ? 사용 가능)</param>
        /// <returns>중앙에 배치된 윈도우 수</returns>
        public static int CenterWindowsByProcess(string processNamePattern)
        {
            if (string.IsNullOrEmpty(processNamePattern))
            {
                return 0;
            }

            List<System.Diagnostics.Process> matchedProcesses = new List<System.Diagnostics.Process>();

            // 와일드카드를 정규표현식으로 변환
            string pattern = "^" + Regex.Escape(processNamePattern)
                .Replace("\\*", ".*")
                .Replace("\\?", ".") + "$";

            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            // 모든 프로세스 열거
            foreach (var process in System.Diagnostics.Process.GetProcesses())
            {
                // 프로세스 이름이 패턴과 일치하는지 확인
                if (regex.IsMatch(process.ProcessName))
                {
                    matchedProcesses.Add(process);
                }
            }

            // 일치하는 프로세스가 없는 경우
            if (matchedProcesses.Count == 0)
            {
                return 0;
            }

            List<IntPtr> matchedWindows = new List<IntPtr>();

            // 모든 윈도우 열거
            EnumWindows(delegate (IntPtr hWnd, IntPtr lParam)
            {
                // 윈도우가 보이는지 확인
                if (IsWindowVisible(hWnd))
                {
                    uint processId;
                    GetWindowThreadProcessId(hWnd, out processId);

                    // 프로세스 ID가 일치하는지 확인
                    foreach (var process in matchedProcesses)
                    {
                        if (process.Id == processId)
                        {
                            matchedWindows.Add(hWnd);
                            break;
                        }
                    }
                }
                return true; // 계속 열거
            }, IntPtr.Zero);

            // 일치하는 윈도우가 없는 경우
            if (matchedWindows.Count == 0)
            {
                return 0;
            }

            int centeredCount = 0;

            // 모든 일치하는 윈도우를 중앙에 배치
            foreach (IntPtr hWnd in matchedWindows)
            {
                if (CenterWindow(hWnd))
                {
                    // 윈도우 이름을 콘솔에 출력
                    //System.Text.StringBuilder windowText = new System.Text.StringBuilder(256);
                    //GetWindowText(hWnd, windowText, windowText.Capacity);
                    //Console.WriteLine($"Centered Window !!! : {windowText}");

                    centeredCount++; 
                }
            }

            return centeredCount;
        }

        /// <summary>
        /// 지정된 윈도우 핸들을 화면 중앙에 배치합니다.
        /// </summary>
        /// <param name="hWnd">윈도우 핸들</param>
        /// <returns>성공 여부</returns>
        public static bool CenterWindow(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero)
            {
                return false;
            }

            // 윈도우 현재 크기 가져오기
            RECT windowRect;
            if (!GetWindowRect(hWnd, out windowRect))
            {
                return false;
            }

            // 윈도우 가로, 세로 크기 계산
            int width = windowRect.Right - windowRect.Left;
            int height = windowRect.Bottom - windowRect.Top;

            // 화면 해상도 가져오기
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            // 화면 중앙 좌표 계산 (왼쪽 위가 중앙에 오도록)
            int centerX = screenWidth / 2;
            int centerY = screenHeight / 2;

            // 윈도우 이동
            bool result = MoveWindow(hWnd, centerX, centerY, width, height, true);
            if (!result)
            {
                int errorCode = Marshal.GetLastWin32Error();
             //   Console.WriteLine($"MoveWindow failed with error code: {errorCode}");
            }
            return result;
        } 
    }
}
