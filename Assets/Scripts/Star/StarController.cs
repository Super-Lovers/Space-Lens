using UnityEngine;

public class StarController : MonoBehaviour
{
    [SerializeField]
    private string _name;
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }
    [TextArea(0, 11)]
    public string Description = string.Empty;
    public StarClassification StarClassification = StarClassification.K;
    public bool Bookmarked = false;

    [SerializeField]
    private int _temperatureInKelvin;
    private const int MinTemperature = 2400;
    private const int MaxTemperature = 12500;

    [Space(10)]
    [SerializeField]
    private GameObject _marker = null;

    #region Game Object Components
    private SpriteRenderer _spriteRenderer;
    #endregion

    #region Dependencies
    private StarViewController _starViewController;
    private PanelController _panelController;
    #endregion

    private void Start()
    {
        _starViewController = FindObjectOfType<StarViewController>();
        _panelController = FindObjectOfType<PanelController>();
        UpdateBookmarkStatus();
    }

    public void UpdateBookmarkStatus()
    {
        Bookmarked = _starViewController.isObjectBookmarked(this);
        if (_panelController != null)
        {
            _panelController.UpdateBookmarkStatus(Bookmarked);
        }
    }

    private void Update()
    {
        if (_marker.activeSelf)
        {
            StabilizeMarkerRotation();
        }
    }

    private void StabilizeMarkerRotation()
    {
        _marker.transform.rotation = new Quaternion(_starViewController.transform.rotation.x,
            _starViewController.transform.rotation.y,
            -_starViewController.transform.rotation.z,
            _starViewController.transform.rotation.w);
    }

    public void Initialize(string name)
    {
        _name = name;

        _spriteRenderer = GetComponent<SpriteRenderer>();

        int randomKelvin = Random.Range(MinTemperature, MaxTemperature + 2500);
        _temperatureInKelvin = randomKelvin;

        if (randomKelvin >= MinTemperature && randomKelvin <= 3700)
        {
            StarClassification = StarClassification.M;
            //_spriteRenderer.color = Color.yellow;
        }
        else if (randomKelvin > 3700 && randomKelvin <= 5200)
        {
            StarClassification = StarClassification.K;
            //_spriteRenderer.color = Color.red;
        }
        else if (randomKelvin > 5200 && randomKelvin <= 6000)
        {
            StarClassification = StarClassification.G;
            //_spriteRenderer.color = Color.magenta;
        }
        else if (randomKelvin > 6000 && randomKelvin <= 7500)
        {
            StarClassification = StarClassification.F;
            //_spriteRenderer.color = Color.white;
        }
        else if (randomKelvin > 7500 && randomKelvin <= 10000)
        {
            StarClassification = StarClassification.A;
            //_spriteRenderer.color = Color.gray;
        }
        else if (randomKelvin > 10000 && randomKelvin <= 12500)
        {
            StarClassification = StarClassification.B;
            //_spriteRenderer.color = Color.cyan;
        }
        else if (randomKelvin > MaxTemperature)
        {
            StarClassification = StarClassification.O;
            //_spriteRenderer.color = Color.blue;
        } else
        {
            StarClassification = StarClassification.K;
        }
    }

    public void ToggleMarker()
    {
        if (_starViewController.PreviousStarController == null)
        {
            _starViewController.PreviousStarController = this;
            _starViewController.CurrentStarController = this;
        } else if (_starViewController.PreviousStarController != null)
        {
            _starViewController.CurrentStarController.HideMarker();
            _starViewController.PreviousStarController = _starViewController.CurrentStarController;
            _starViewController.CurrentStarController = this;
        }

        _starViewController.CurrentStarController.ShowMarker();
    }

    public void ShowMarker()
    {
        _marker.SetActive(true);
    }

    public void HideMarker()
    {
        _marker.SetActive(false);
    }
}
