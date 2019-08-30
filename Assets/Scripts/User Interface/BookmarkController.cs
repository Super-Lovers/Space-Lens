using UnityEngine;

public class BookmarkController : MonoBehaviour
{
    public StarController StarController;
    private StarViewController _starViewController;

    void Start()
    {
        _starViewController = FindObjectOfType<StarViewController>();
    }

    public void LoadDetails()
    {
        _starViewController.PanelController.gameObject.SetActive(true);
        _starViewController.PanelController.LoadDetails(StarController, true);
    }
}
