using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public bool isGameStarted = false;
    public bool isGameCompleted = false;

    [Space(10)]
    public GameObject IntroContainer = null;
    public GameObject ScreenInitialization = null;

    private FadingController _fadingController;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _fadingController = FindObjectOfType<FadingController>();

        if (isGameCompleted)
        {
            IntroContainer.SetActive(false);
            ScreenInitialization.SetActive(true);
            _fadingController.Fade("out", string.Empty);
        }
        isGameStarted = false;
    }

    // Used whenever the player exits/enter the main menu in order
    // to resume or start playing the game.
    public void EnableGameplay()
    {
        isGameStarted = true;
    }
    public void DisableGameplay()
    {
        isGameStarted = false;
    }
}
