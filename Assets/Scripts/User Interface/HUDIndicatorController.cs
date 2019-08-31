using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDIndicatorController : MonoBehaviour
{
    public bool IsTargetFound = false;
    [SerializeField]
    private Image _indicator = null;
    [Space(10)]
    public StarController Target = null;
    [SerializeField]
    private Transform _zoomCameraTransform = null;
    [SerializeField]
    private List<Sprite> _indicatorVariations = new List<Sprite>();
    [Space(10)]
    [SerializeField]
    private Image _lights = null;
    [SerializeField]
    private List<Sprite> _lightVariations = new List<Sprite>();

    #region Dependencies
    private PanelController _panelController = null;
    private StarViewController _starViewController = null;
    #endregion

    private AudioController _audioController = null;
    private bool _isSoundPlayed = false;

    private void Start()
    {
        _panelController = FindObjectOfType<PanelController>();
        _audioController = GetComponent<AudioController>();
        if (_panelController != null)
        {
            if (_panelController.HUDIndicators.Contains(this) == false)
            {
                _panelController.HUDIndicators.Add(this);
            }
        }

        _starViewController = FindObjectOfType<StarViewController>();
        if (_starViewController != null)
        {
            if (_starViewController.HUDIndicators.Contains(this) == false)
            {
                _starViewController.HUDIndicators.Add(this);
            }
        }
        InvokeRepeating("FlickerLights", 0, 1f);
    }

    private void FlickerLights()
    {
        _lights.sprite = _lightVariations[Random.Range(0, _lightVariations.Count - 1)];
    }

    public void UpdateHUDLights()
    {
        if (IsTargetFound == false)
        {
            float distanceToTarget = Vector2.Distance(_zoomCameraTransform.position, Target.transform.position);
            if (distanceToTarget > 0.35f)
            {
                // Off
                _indicator.sprite = _indicatorVariations[0];
                //_audioController.PlaySound("Light Indicator Off");
                _isSoundPlayed = false;
            }
            else if (distanceToTarget > 0 && distanceToTarget <= 0.35f)
            {
                // On
                _indicator.sprite = _indicatorVariations[1];
                if (_isSoundPlayed == false)
                {
                    _audioController.PlaySound("Light Indicator On");
                    _isSoundPlayed = true;
                }
            }
        }
    }

    public void UpdateStatus()
    {
        if (IsTargetFound)
        {
            _indicator.sprite = _indicatorVariations[1];
        } else if (IsTargetFound == false)
        {
            _indicator.sprite = _indicatorVariations[0];
        }
    }
}
