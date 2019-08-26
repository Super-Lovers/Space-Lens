using UnityEngine;
using UnityEngine.UI;

public class LensGlassController : MonoBehaviour
{
    [SerializeField]
    private Transform LensGlass;
    public Image ConsoleDisplay;

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        if ((mousePosition.x > Mathf.Abs(ConsoleDisplay.rectTransform.anchoredPosition.x) &&
            mousePosition.x < ConsoleDisplay.rectTransform.sizeDelta.x) &&
            ((mousePosition.y > -Mathf.Abs(ConsoleDisplay.rectTransform.anchoredPosition.y) + 80 &&
            mousePosition.y < ConsoleDisplay.rectTransform.sizeDelta.y)))
        {
            LensGlass.gameObject.SetActive(true);
            Vector3 newCameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = newCameraPosition;
            LensGlass.position = Input.mousePosition;
        }
        else
        {
            LensGlass.gameObject.SetActive(false);
        }
    }
}
