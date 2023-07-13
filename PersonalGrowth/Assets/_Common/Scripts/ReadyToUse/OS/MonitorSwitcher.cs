using UnityEngine;

public class MonitorSwitcher : Singleton<MonitorSwitcher>
{
    private int currentDisplayIndex = 0;

    private void Start()
    {
        currentDisplayIndex = Camera.main.targetDisplay;

#if UNITY_STANDALONE_WIN
        int lDisplaysCount = Display.displays.Length;

        for (int i = 0; i < lDisplaysCount; i++)
        {
            int lDisplayIndex = i;
            SystemTray.AddStaticMenuItem("To Monitor " + lDisplayIndex, () => { ChangeDisplay(lDisplayIndex); });
        }
#endif
    }

    public void ChangeDisplay(int pDisplayIndex)
    {
        Display lNewDisplay = Display.displays[pDisplayIndex];
        lNewDisplay.Activate();
        Camera.main.targetDisplay = pDisplayIndex;
        PlayerPrefs.SetInt("UnitySelectMonitor", pDisplayIndex);
        Screen.SetResolution(lNewDisplay.renderingWidth, lNewDisplay.renderingHeight, true);

        currentDisplayIndex = pDisplayIndex;
        DesktopOverlay.Initialize();
    }

    public Ray GetCurrentDisplayMouseScreenRay()
    {
        Vector3 lRelativeMousePos = Display.RelativeMouseAt(Input.mousePosition);

        // Z is the index of the display on which the mouse is on
        if (currentDisplayIndex != (int)lRelativeMousePos.z)
        {
            // If the mouse's display isn't the current display, position ray at screen center.
            Display lCurrentDisplay = Display.displays[currentDisplayIndex];
            lRelativeMousePos = new Vector3(lCurrentDisplay.renderingWidth * 0.5f, lCurrentDisplay.renderingHeight * 0.5f);
        }

        return Camera.main.ScreenPointToRay(lRelativeMousePos);
    }
}