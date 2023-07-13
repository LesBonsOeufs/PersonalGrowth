using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
#if UNITY_STANDALONE_WIN
using System.Windows.Forms;
using System.Drawing;
#endif
using UnityEngine;
using UnityEngine.EventSystems;

#if UNITY_STANDALONE_OSX
using Application = UnityEngine.Application;
using Screen = UnityEngine.Screen;
using System.Linq;
using System.Threading;
#endif

public class DesktopOverlay : Singleton<DesktopOverlay>
{

#if UNITY_STANDALONE_WIN

	// Windows Dlls

	[DllImport("dwmapi.dll")]
	private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

	[DllImport("user32.dll", SetLastError = true)]
	private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

	[DllImport("user32.dll", SetLastError = true)]
	private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

	[DllImport("user32.dll", SetLastError = true)]
	private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

	[DllImport("user32.dll", SetLastError = true)]
	private static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

	[DllImport("kernel32.dll", SetLastError = true)]
	private static extern IntPtr GetConsoleWindow();

	// Windows Dlls Params

	private static IntPtr hWnd;

	[StructLayout(LayoutKind.Sequential)]
	private struct RECT
	{
		public int Left;        // x position of upper-left corner
		public int Top;         // y position of upper-left corner
		public int Right;       // x position of lower-right corner
		public int Bottom;      // y position of lower-right corner
	}

	private struct MARGINS
	{
		public int cxLeftWidth;
		public int cxRightWidth;
		public int cyTopHeight;
		public int cyBottomHeight;
	}

	private struct HWND
	{
		public static readonly IntPtr NOTOPMOST = new IntPtr(-2);
		public static readonly IntPtr TOPMOST = new IntPtr(-1);
		public static readonly IntPtr TOP = new IntPtr(0);
		public static readonly IntPtr BOTTOM = new IntPtr(1);
	}

	private const int GWL_EXSTYLE = -20;

	private const uint WS_EX_LAYERED = 0x00080000;
	private const uint WS_EX_TRANSPARENT = 0x00000020;
	private const uint WS_EX_TOOLWINDOW = 0x0080;

	private bool prevClickThrough = true;

#elif UNITY_STANDALONE_OSX

	// MacOSX Dlls

	[DllImport("libMacOS")]
	private extern static void InitWindowWrapper(string imagePath);

	[DllImport("libMacOS")]
	private extern static void PerformZoomWrapper();

	[DllImport("libMacOS")]
	private extern static void IgnoreMouseEventsWrapper(bool isIgnoring);

	[DllImport("libMacOS")]
	private extern static void AskForStartupLaunchWrapper();

	[DllImport("libMacOS")]
	private extern static void SetUnitySendMessageFunctionWrapper(UnitySendMessageDelegate functionPointer);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void UnitySendMessageDelegate(string methodName);

#endif

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	public static void Initialize()
	{
#if UNITY_STANDALONE_OSX
        Screen.fullScreen = false;
	    InitWindowWrapper(Application.streamingAssetsPath + "/Icon_Olly.png");
        AskForStartupLaunchWrapper();
#elif UNITY_STANDALONE_WIN
        hWnd = FindWindow(null, "Olly");

        MARGINS margins = new MARGINS { cxLeftWidth = -1 };
        DwmExtendFrameIntoClientArea(hWnd, ref margins);
        SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT | WS_EX_TOOLWINDOW);
        SetWindowPos(hWnd, HWND.TOPMOST, 0, 0, 0, 0, 0);
#endif
    }

    private void Start()
	{
#if UNITY_STANDALONE_OSX
        PerformZoomWrapper();
#endif
	}

	private void Update()
	{
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition,
        };
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

#if UNITY_STANDALONE_WIN
	    SetClickThrough(!(raycastResults.Count > 0));
#elif UNITY_STANDALONE_OSX
        IgnoreMouseEventsWrapper(!(raycastResults.Count > 0));
#endif
	}

	private void SetClickThrough(bool clickThrough)
	{
#if UNITY_STANDALONE_WIN
        if (clickThrough == prevClickThrough) 
            return;
        else if (clickThrough)
            SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT | WS_EX_TOOLWINDOW);
        else
            SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TOOLWINDOW);

        SetWindowPos(hWnd, HWND.TOPMOST, 0, 0, 0, 0, 0);
        prevClickThrough = clickThrough;
#endif
	}

	public static int GetTaskbarHeight()
	{
#if UNITY_STANDALONE_WIN
        IntPtr TaskBarHandle;
        TaskBarHandle = FindWindow("Shell_traywnd", "");

        RECT rct;
        GetWindowRect(TaskBarHandle, out rct);

        return rct.Bottom - rct.Top;
#else
		return 0;
#endif
	}
}