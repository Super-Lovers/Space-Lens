using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDIndicatorController : MonoBehaviour
{
    [SerializeField]
    private Image _indicator = null;
    [Space(10)]
    [SerializeField]
    private Image _lights = null;
    [SerializeField]
    private List<Sprite> _lightVariations = new List<Sprite>();

    private void Start()
    {
        InvokeRepeating("FlickerLights", 0, 1f);
    }

    private void FlickerLights()
    {
        _lights.sprite = _lightVariations[Random.Range(0, _lightVariations.Count - 1)];
    }
}
