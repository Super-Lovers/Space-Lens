using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField]
    private Sprite _defaultSprite = null;
    [SerializeField]
    private Sprite _hoveredSprite = null;

    private Image _imageComponent = null;
    private TextMeshProUGUI _textComponent = null;

    private Color32 _defaultColor = new Color32(255, 255, 255, 255);
    private Color32 _hoveredColor = new Color32(16, 24, 22, 255);

    private void Start()
    {
        _imageComponent = GetComponentInChildren<Image>();
        _textComponent = GetComponentInChildren<TextMeshProUGUI>();
        _defaultColor = _textComponent.color;
    }

    public void Hover()
    {
        _imageComponent.sprite = _hoveredSprite;
        _textComponent.color = _hoveredColor;
    }

    public void Exit()
    {
        _imageComponent.sprite = _defaultSprite;
        _textComponent.color = _defaultColor;
    }

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
