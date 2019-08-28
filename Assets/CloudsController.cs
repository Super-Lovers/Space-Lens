using System.Collections.Generic;
using UnityEngine;

public class CloudsController : MonoBehaviour
{
    [SerializeField]
    private Transform _cloudsContainer = null;
    [SerializeField]
    private List<GameObject> _cloudPrefabs = new List<GameObject>();

    [Header("Currently visible clouds")]
    [Space(10)]
    [SerializeField]
    private List<RectTransform> _cloudsRectTransforms = new List<RectTransform>();

    private void Start()
    {
        InvokeRepeating("MoveClouds", 0, 0.7f);
    }

    private void MoveClouds()
    {
        Vector2 currentPosition;
        for (int i = 0; i < _cloudsRectTransforms.Count; i++)
        {
            currentPosition = _cloudsRectTransforms[i].anchoredPosition;
            currentPosition.x -= 10;
            _cloudsRectTransforms[i].anchoredPosition = currentPosition;

            if (currentPosition.x < -810)
            {
                GameObject currentObj = _cloudsRectTransforms[i].gameObject;
                _cloudsRectTransforms.Remove(_cloudsRectTransforms[i]);
                Destroy(currentObj);

                GameObject newCloud = Instantiate(_cloudPrefabs[Random.Range(0, _cloudPrefabs.Count - 1)], _cloudsContainer);

                RectTransform cloudTransform = newCloud.GetComponent<RectTransform>();
                cloudTransform.anchoredPosition = new Vector2(cloudTransform.anchoredPosition.x, Random.Range(115, -133));

                _cloudsRectTransforms.Add(newCloud.GetComponent<RectTransform>());
            }
        }

    }
}
