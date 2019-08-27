using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _name = null;
    [SerializeField]
    private TextMeshProUGUI _type = null;
    [SerializeField]
    private TextMeshProUGUI _description = null;
    [Space(10)]
    [SerializeField]
    private Image _bookmarkStatus = null;

    public Sprite BookmarkedImage = null;
    public Sprite NotBookmarkedImage = null;

    [Header("======> Background of panel")]
    [Space(10)]
    public TextMeshProUGUI PlaceholderText = null;

    private bool _isItLoading = false;
    private const float titlesLoadSpeed = 0.1f;
    private const float plainTextLoadSpeed = 0.02f;

    private void Start()
    {
        InvokeRepeating("JumblePlaceholderText", 0, 1);
    }

    public void LoadDetails(string name, string type, string description, bool bookmarkStatus)
    {
        if (_isItLoading == false)
        {
            PlaceholderText.color = new Color(
                PlaceholderText.color.r,
                PlaceholderText.color.g,
                PlaceholderText.color.b,
                0.07f);
            _bookmarkStatus.gameObject.SetActive(true);
            _name.text = string.Empty;
            _type.text = string.Empty;
            _description.text = string.Empty;

            UpdateBookmarkStatus(bookmarkStatus);

            LoadText(_name, name, titlesLoadSpeed);
            LoadText(_type, "Type: " + type, titlesLoadSpeed);
            if (description == string.Empty)
            {   
                LoadText(_description, "Nothing of significance to report", plainTextLoadSpeed);
            }
            else
            {
                LoadText(_description, description, plainTextLoadSpeed);
            }

            _isItLoading = true;
        }
    }

    public void UpdateBookmarkStatus(bool bookmarkStatus)
    {
        if (bookmarkStatus)
        {
            _bookmarkStatus.sprite = BookmarkedImage;
        }
        else
        {
            _bookmarkStatus.sprite = NotBookmarkedImage;
        }
    }

    private void LoadText(TextMeshProUGUI field, string text, float speed)
    {
        StartCoroutine(LoadTextCo(field, text, speed));
    }

    private IEnumerator LoadTextCo(TextMeshProUGUI field, string text, float speed)
    {
        for (int i = 0; i < text.Length; i++)
        {
            field.text += text[i];
            yield return new WaitForSeconds(speed);
        }

        if (field == _description)
        {
            _isItLoading = false;
        }
    }

    private void JumblePlaceholderText()
    {
        string newCombination = string.Empty;
        for (int i = 0; i < PlaceholderText.text.Length; i++)
        {
            newCombination += Random.Range(0, 9);
        }

        PlaceholderText.text = newCombination;
    }
}
