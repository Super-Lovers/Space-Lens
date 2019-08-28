using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    public void CloseWindow(Object obj)
    {
        GameObject gameObj = (GameObject)obj;
        gameObj.SetActive(false);
    }

    public void OpenWindow(Object obj)
    {
        GameObject gameObj = (GameObject)obj;
        gameObj.SetActive(true);
    }

    public void ToggleWindow(Object obj)
    {
        GameObject gameObj = (GameObject)obj;
        gameObj.SetActive(!gameObj.activeSelf);
    }
}
