using UnityEngine;
using UnityEngine.UI;

public class LensGlassController : MonoBehaviour
{
    [SerializeField]
    private Transform LensGlass = null;
    [SerializeField]
    private Transform LensGlassCamera = null;
    [SerializeField]
    private Image ConsoleDisplay = null;
    public bool CanCameraZoom { get; set; }

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
            Vector3 newLensPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = newLensPosition;
            LensGlass.position = Input.mousePosition;
            LensGlassCamera.position = newCameraPosition;
            CanCameraZoom = true;
        }
        else
        {
            CanCameraZoom = false;
            LensGlass.gameObject.SetActive(false);
        }
    }
}
