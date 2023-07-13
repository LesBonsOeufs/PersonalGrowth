using TMPro;
using UnityEngine;

public class Drawer_SimpleTimer_Countdown : MonoBehaviour
{
	[SerializeField] SimpleTimer timer;

	[SerializeField] string beforeText;
	[SerializeField] string afterText;
	[SerializeField] TextMeshProUGUI chronoText;

	public void Update()
	{
		if (chronoText != null) chronoText.text = beforeText + string.Format("{0:0}", timer.TimeLeft) + afterText;
	}

}
