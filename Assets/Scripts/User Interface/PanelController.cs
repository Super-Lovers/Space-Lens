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

    [SerializeField]
    private GameObject _closeDetailsButton = null;
    public Sprite BookmarkedImage = null;
    public Sprite NotBookmarkedImage = null;

    [Header("======> Background of panel")]
    [Space(10)]
    public TextMeshProUGUI PlaceholderText = null;

    public bool IsItLoading = false;
    private const float titlesLoadSpeed = 0.03f;
    private const float plainTextLoadSpeed = 0.000000005f;

    #region Dependencies
    public GameObject BookmarksPanel;
    #endregion

    // Sound effects components
    public AudioController AudioController;

    private void Start()
    {
        AudioController = GetComponent<AudioController>();
        InvokeRepeating("JumblePlaceholderText", 0, 1);
    }

    public void LoadDetails(string name, string type, string description, bool bookmarkStatus, bool isItFromBookmarksMenu)
    {
        AudioController.PlaySound("Text Displaying");
        AudioController.AudioSource.loop = true;
        if (isItFromBookmarksMenu)
        {
            AudioController.PlaySound("Open/Close Menu");
            BookmarksPanel.SetActive(false);
            _bookmarkStatus.gameObject.SetActive(false);
            _closeDetailsButton.SetActive(true);
        } else
        {
            AudioController.PlaySound("Open/Close Menu");
            _closeDetailsButton.SetActive(false);
        }
        if (IsItLoading == false)
        {
            BookmarksPanel.SetActive(false);
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

            IsItLoading = true;
        }
    }

    public void UpdateBookmarkStatus(bool bookmarkStatus)
    {
        if (AudioController != null)
        {
            AudioController.PlaySound("Open/Close Menu");
        }
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
        string followingCharacter = "_";
        for (int i = 0; i < text.Length; i++)
        {
            if (i > 0)
            {
                field.text = field.text.Substring(0, field.text.Length - 1);
                field.text += text[i] + followingCharacter;
            }
            else if (i == 0)
            {
                field.text += text[i] + followingCharacter;
            }

            if (i == text.Length - 1)
            {
                field.text = field.text.Substring(0, field.text.Length - 1);
            }
            yield return new WaitForSeconds(speed);
        }

        if (field == _description)
        {
            AudioController.AudioSource.loop = false;
            AudioController.AudioSource.Stop();
            IsItLoading = false;
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
