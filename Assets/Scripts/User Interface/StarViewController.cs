using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarViewController : MonoBehaviour
{
    [SerializeField]
    private Transform _starView = null;

    // Parameters of the speed at which the
    // star view rotates in the background
    // to simulate Earth's rotation.
    [Header("======> Lower is faster!")]
    [Space(10)]
    [Range(0, 5)]
    [SerializeField]
    private float _rotationDelay = 0;
    private bool _rotationCooldown = false;

    [Header("======> Recently selected objects")]
    [Space(10)]
    public StarController PreviousStarController;
    public StarController CurrentStarController;

    [Header("======> Bookmarks-related system")]
    [Space(10)]
    [SerializeField]
    private Transform _bookmarksContainer = null;
    [SerializeField]
    private GameObject _bookmarkEntryPrefab = null;
    private List<StarController> _bookmarkedObjects = new List<StarController>();

    #region Dependencies
    public PanelController PanelController = null;
    public List<HUDIndicatorController> HUDIndicators;
    #endregion

    private void Start()
    {
        PanelController = FindObjectOfType<PanelController>();
    }

    public void DisplayBookmarks()
    {
        PanelController.BookmarksPanel.SetActive(true);
        PanelController.gameObject.SetActive(false);
        for (int i = 0; i < _bookmarksContainer.transform.childCount; i++)
        {
            Destroy(_bookmarksContainer.transform.GetChild(i).gameObject);
        }

        foreach (StarController bookmark in _bookmarkedObjects)
        {
            string entryText = bookmark.Name + " - <color=#FF3EC1>" + bookmark.StarClassification + "</color>";

            GameObject bookmarkEntry = Instantiate(_bookmarkEntryPrefab, _bookmarksContainer);
            bookmarkEntry.GetComponentInChildren<TextMeshProUGUI>().text = entryText;

            bookmarkEntry.GetComponent<BookmarkController>().StarController = bookmark;
        }
    }

    private void Update()
    {
        if (_starView != null)
        {
            if (_rotationCooldown == false)
            {
                _starView.Rotate(new Vector3(0, 0, 0.2f));
                Invoke("EnableRotation", _rotationDelay);
                _rotationCooldown = true;
            }
        }
    }

    public void BookmarkToggleCurrent()
    {
        if (isObjectBookmarked(CurrentStarController))
        {
            _bookmarkedObjects.Remove(CurrentStarController);

            foreach (HUDIndicatorController indicator in HUDIndicators)
            {
                if (CurrentStarController == indicator.Target)
                {
                    indicator.IsTargetFound = false;
                    indicator.UpdateStatus();
                }
            }
        } else
        {
            _bookmarkedObjects.Add(CurrentStarController);

            foreach (HUDIndicatorController indicator in HUDIndicators)
            {
                if (CurrentStarController == indicator.Target)
                {
                    indicator.IsTargetFound = true;
                    indicator.UpdateStatus();
                }
            }
        }

        SortBookmarks();
        CurrentStarController.UpdateBookmarkStatus();
    }

    public int GetNumberOfBookmarks()
    {
        return _bookmarkedObjects.Count;
    }

    public bool isObjectBookmarked(StarController obj)
    {
        foreach (StarController controller in _bookmarkedObjects)
        {
            if (obj == controller)
            {
                return true;
            }
        }

        return false;
    }

    private void SortBookmarks()
    {
        List<StarController> newList = _bookmarkedObjects;
        for (int i = 0; i < newList.Count; i++)
        {
            for (int j = 0; j < newList.Count - 1; j++)
            {
                if (newList[j].Name.CompareTo(newList[j + 1].Name) > 0)
                {
                    StarController temp = newList[j];
                    newList[j] = newList[j + 1];
                    newList[j + 1] = temp;
                }
            }
        }

        _bookmarkedObjects = newList;
    }

    private void EnableRotation()
    {
        _rotationCooldown = false;
    }

    public float GetRotationDelay()
    {
        return _rotationDelay;
    }
}
