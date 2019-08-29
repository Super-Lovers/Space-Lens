using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointerController : MonoBehaviour
{
    [SerializeField]
    private LensGlassController LensGlassController = null;
    public List<GameObject> MainViewObjects = new List<GameObject>();
    public List<GameObject> ZoomedViewObjects = new List<GameObject>();
    private bool _zoomedIn = false;

    #region Dependencies
    private PanelController _panelController;
    private StarViewController _starViewController;
    [Space(10)]
    [SerializeField]
    private Image ConsoleDisplay = null;
    [Header("Used for illegal actions by user")]
    [SerializeField]
    private Image _lensCircle = null;
    private bool _isErrorIconBlinking = false;
    [SerializeField]
    private Camera _zoomCamera;
    #endregion

    // Sound effects component
    private AudioController _audioController;

    private void Start()
    {
        _audioController = GetComponent<AudioController>();
        _panelController = FindObjectOfType<PanelController>();
        _starViewController = FindObjectOfType<StarViewController>();
    }

    void Update()
    {
        if (_panelController.IsItLoading == false && PlayerController.Instance.isGameStarted)
        {
            if (Input.GetMouseButtonDown(1) && LensGlassController.CanCameraZoom)
            {
                if (_zoomedIn)
                {
                    _audioController.PlaySound("Zoom Out");
                    _starViewController.DisplayBookmarks();
                    foreach (GameObject obj in MainViewObjects)
                    {
                        if (obj != null)
                        {
                            if (_starViewController.GetNumberOfBookmarks() > 0)
                            {
                                obj.SetActive(true);
                            }
                            else
                            {
                                if (obj.name != "Bookmarks Panel")
                                {
                                    obj.SetActive(true);
                                }
                            }
                        }
                    }
                    foreach (GameObject obj in ZoomedViewObjects)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(false);
                        }
                    }
                }
                else
                {
                    _audioController.PlaySound("Zoom In");
                    _starViewController.DisplayBookmarks();
                    foreach (GameObject obj in MainViewObjects)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(false);
                        }
                    }
                    foreach (GameObject obj in ZoomedViewObjects)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(true);
                        }
                    }
                }
                _zoomedIn = !_zoomedIn;
            }

            Vector3 mousePosition = Input.mousePosition;
            if ((mousePosition.x > -Mathf.Abs(ConsoleDisplay.rectTransform.anchoredPosition.x) &&
                mousePosition.x < ConsoleDisplay.rectTransform.sizeDelta.x) &&
                ((mousePosition.y > -Mathf.Abs(ConsoleDisplay.rectTransform.anchoredPosition.y) + 80 &&
                mousePosition.y < ConsoleDisplay.rectTransform.sizeDelta.y)))
            {
                if (Input.GetMouseButtonDown(0) && _zoomedIn == false && _isErrorIconBlinking == false)
                {
                    _audioController.PlaySound("Zooming Error");
                    StartCoroutine(BlinkIcon());
                    _isErrorIconBlinking = true;
                }
                if (Input.GetMouseButtonDown(0) && _zoomedIn == true)
                {
                    Vector3 pointerPosition = _zoomCamera.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 origin = new Vector2(pointerPosition.x, pointerPosition.y);
                    Vector2 direction = Vector2.one;

                    // Find a way to make it as accurate as possible.
                    RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, Mathf.Infinity, 9);

                    if (hits.Length > 0 && hits[hits.Length - 1].transform.CompareTag("Space Object"))
                    {
                        StarController starController;
                        if ((starController = hits[hits.Length - 1].collider.GetComponent<StarController>()) != null)
                        {
                            // Sound effect
                            AudioController audioController = starController.GetComponent<AudioController>();
                            audioController.PlaySound(Random.Range(0, audioController.CountOfSounds()));

                            starController.ToggleMarker();
                            _panelController.LoadDetails(
                                starController.Name,
                                starController.StarClassification.ToString(),
                                starController.Description,
                                starController.Bookmarked, false);
                        }
                        //ObjectDetailsManager.Instance.InitializePanelOf(hit.collider.GetComponent<StarController>(), pointerPosition);
                    }
                }
            }
        }
    }

    private IEnumerator BlinkIcon()
    {
        Color defaultColor = _lensCircle.color;

        float blinkingSpeed = 0.1f;
        for (int i = 0; i < 2; i++)
        {
            _lensCircle.color = new Color(255, 0, 0, 0.3f);
            yield return new WaitForSeconds(blinkingSpeed);
            _lensCircle.color = defaultColor;
            yield return new WaitForSeconds(blinkingSpeed);
        }

        _isErrorIconBlinking = false;
    }
}
