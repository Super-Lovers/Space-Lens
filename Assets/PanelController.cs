using UnityEngine;

public class PanelController : MonoBehaviour
{
    public void Close(Object panel)
    {
        GameObject objPanel = (GameObject)panel;
        objPanel.SetActive(false);
    }
}
