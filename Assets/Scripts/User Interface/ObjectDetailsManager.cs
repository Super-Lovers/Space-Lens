using UnityEngine;

public class ObjectDetailsManager : MonoBehaviour
{
    public static ObjectDetailsManager Instance;
    public Transform DetailsPanelsContainer;
    public GameObject DetailsPanelPrefab;
    
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

    public void InitializePanelOf(StarController obj, Vector2 objPos)
    {
        Vector2 newCanvasPosition = Camera.main.WorldToScreenPoint(objPos);
        GameObject newPanel = Instantiate(DetailsPanelPrefab, 
            new Vector3(newCanvasPosition.x, newCanvasPosition.y, 0), Quaternion.identity, DetailsPanelsContainer);
    }
}
