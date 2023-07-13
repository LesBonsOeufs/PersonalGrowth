using UnityEngine;
using System.Threading.Tasks;
using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Threading;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class AvatarKeyPos : MonoBehaviour
{
	[SerializeField] private Transform target = default;
	[SerializeField] private float speed = 10f;

	[Space]
	[SerializedDictionary("Name", "Event"), SerializeField]
	private SerializedDictionary<EKeyPositions, Vector3> _keyPositions = default;

	public Dictionary<EKeyPositions, Vector3> KeyPositions => _keyPositions;

	private CancellationTokenSource cancellationSource;

    public async Task<bool> GoToKeyPos(EKeyPositions pKeyPosition)
	{
		cancellationSource?.Cancel();
		cancellationSource = new CancellationTokenSource();
		CancellationToken lToken = cancellationSource.Token;

        Vector3 lKeyPosition = KeyPositions[pKeyPosition];

		Vector3 lToPosition = lKeyPosition - transform.position;
		Vector3 lVelocity;

		while (lToPosition.magnitude > speed * Time.deltaTime)
		{
			lVelocity = lToPosition.normalized * speed * Time.deltaTime;
            transform.position += lVelocity;

			await Task.Yield();

			if (lToken.IsCancellationRequested)
				return false;

			lToPosition = lKeyPosition - transform.position;
		}

        transform.position = lKeyPosition;

		return true;
	}

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		using (new Handles.DrawingScope())
		{
			Handles.color = Color.magenta;

			foreach (Vector3 keyPos in _keyPositions.Values)
				Handles.DrawWireDisc(keyPos, Vector3.back, 1f);
		}
	}
#endif
}