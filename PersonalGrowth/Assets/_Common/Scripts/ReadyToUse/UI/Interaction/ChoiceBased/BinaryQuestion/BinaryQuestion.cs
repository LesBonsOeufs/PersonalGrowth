using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class BinaryQuestion : MonoBehaviour
{
	[SerializeField] private Button buttonYes;
	[SerializeField] private Button buttonNo;
	[SerializeField] private bool onlyActiveForAsk = false;

	public virtual async Task<bool> AskBinaryQuestion()
	{
		if (onlyActiveForAsk)
			gameObject.SetActive(true);

        bool? lResponse = null;

		buttonNo.onClick.AddListener(() => lResponse = false);
		buttonYes.onClick.AddListener(() => lResponse = true);

		while (lResponse is null)
			await Task.Yield();

        if (onlyActiveForAsk)
            gameObject.SetActive(false);

        return (bool)lResponse;
	}
}