using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BinaryQuestion : MonoBehaviour
{
	public TextMeshProUGUI questionSlot;

	public Button buttonYes;
	public TextMeshProUGUI textSlotYes;

	public Button buttonNo;
	public TextMeshProUGUI textSlotNo;

	private bool? response = null;
	public virtual async Task<bool> AskBinaryQuestion(string question, string answerYes, string answerNo)
	{
		response = null;

		questionSlot.text = question;
		textSlotNo.text = answerNo;
		textSlotYes.text = answerYes;

		buttonNo.onClick.AddListener(() => response = false);
		buttonYes.onClick.AddListener(() => response = true);

		while (response is null)
		{
			await Task.Yield();
		}

		return (bool)response;
	}
}