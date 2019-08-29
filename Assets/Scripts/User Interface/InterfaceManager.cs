using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    public void CloseWindow(Object obj)
    {
        AudioManager.Instance.AudioController.PlaySound("Open/Close Menu");
        GameObject gameObj = (GameObject)obj;
        gameObj.SetActive(false);
    }

    public void OpenWindow(Object obj)
    {
        AudioManager.Instance.AudioController.PlaySound("Open/Close Menu");
        GameObject gameObj = (GameObject)obj;
        gameObj.SetActive(true);
    }

    public void ToggleWindow(Object obj)
    {
        AudioManager.Instance.AudioController.PlaySound("Open/Close Menu");
        GameObject gameObj = (GameObject)obj;
        gameObj.SetActive(!gameObj.activeSelf);
    }
}
