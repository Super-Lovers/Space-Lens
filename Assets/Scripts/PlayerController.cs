using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public bool isGameStarted = false;

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
