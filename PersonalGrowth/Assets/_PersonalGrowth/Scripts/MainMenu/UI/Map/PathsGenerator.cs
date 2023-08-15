using MoreMountains.Tools;
using NaughtyAttributes;
using UnityEngine;

namespace Com.GabrielBernabeu.PersonalGrowth.MainMenu.UI.Map {
    public class PathsGenerator : MonoBehaviour
    {
        [SerializeField] private MapPath pathPrefab;
        [SerializeField] private Transform pathsContainer;
        [SerializeField] private Transform spotsContainer;

        ///For editor
        [Button("GeneratePath")]
        private void GeneratePath()
        {
            pathsContainer.MMDestroyAllChildren();

            int lSpotsCount = spotsContainer.childCount;

            for (int i = 0; i < lSpotsCount; i++)
            {
                if (!spotsContainer.GetChild(i).TryGetComponent(out MapSpot lSpot))
                    continue;

                if (lSpot.NextSpot == null)
                    continue;

                MapPath lPath = Instantiate(pathPrefab, pathsContainer);
                lPath.SetExtents(lSpot.RectTransform.anchoredPosition, lSpot.NextSpot.RectTransform.anchoredPosition);
            }
        }
    }
}
