using AYellowpaper.SerializedCollections;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Drawing;
#if UNITY_STANDALONE_WIN
using System.Windows.Forms;
using ContextMenu = System.Windows.Forms.ContextMenu;
#endif
using UnityEngine;
using UnityEngine.Events;
using Application = UnityEngine.Application;

public class SystemTray : Singleton<SystemTray>
{
    private static Dictionary<string, Action> staticMenuItems = new Dictionary<string, Action>();

    [SerializedDictionary("Name", "Event"), SerializeField]
    private SerializedDictionary<string, UnityEvent> menuItems = default;

#if UNITY_STANDALONE_OSX

    [DllImport("libMacOS")]
	private extern static void SetUnitySendMessageFunctionWrapper(UnitySendMessageDelegate functionPointer);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void UnitySendMessageDelegate(string methodName);

#elif UNITY_STANDALONE_WIN

    private NotifyIcon tray;
    private bool isWinSysTrayInit = false;

#endif

    protected override void Awake()
    {
        base.Awake();

#if UNITY_STANDALONE_WIN
        if (!isWinSysTrayInit)
            InitializeWinSystemTray();
#endif
    }

    private void Start()
    {
#if UNITY_STANDALONE_OSX
        // Pass the UnitySendMessage function to the dynamic library
        SetUnitySendMessageFunctionWrapper(OnMacOSTrigger);
#endif
    }

#if UNITY_STANDALONE_WIN
    private void InitializeWinSystemTray()
    {
        isWinSysTrayInit = true;

        tray = new NotifyIcon
        {
            Text = "Olly",
            Icon = new Icon(Application.streamingAssetsPath + "/Icon_Olly.ico"),
            ContextMenu = new ContextMenu(),
            Visible = true
        };

        foreach (KeyValuePair<string, Action> staticMenuItem in staticMenuItems)
            AddMenuItem(staticMenuItem.Key, staticMenuItem.Value);

        foreach (KeyValuePair<string, UnityEvent> menuItem in menuItems)
            AddMenuItem(menuItem.Key, () => { menuItem.Value?.Invoke(); });

        AddMenuItem("Quit", OnExit);
    }
#endif

    public void AddMenuItem(string name, Action callback)
    {
#if UNITY_STANDALONE_WIN
        if (!isWinSysTrayInit)
            InitializeWinSystemTray();

        tray.ContextMenu.MenuItems.Add(name, (object sender, EventArgs e) => { callback?.Invoke(); });
#endif
    }

    public static void AddStaticMenuItem(string name, Action callback)
    {
#if UNITY_STANDALONE_WIN
        if (Instance != null)
            Instance.AddMenuItem(name, callback);

        staticMenuItems.Add(name, callback);
#endif
    }

#if UNITY_STANDALONE_OSX

    private void OnMacOSTrigger(string message)
    {
        switch (message)
        {
            case "OnExit":
                OnExit();
                break;
            case "OnHide":
                Debug.Log("Hide");
                break;
            case "OnShow":
                Debug.Log("Show");
                break;
        }
    }

#endif

    private void OnExit()
    {
        Application.Quit();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

#if UNITY_STANDALONE_WIN
        tray.Dispose();
#endif
    }
}