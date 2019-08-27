using System.Collections.Generic;
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

    private List<StarController> _bookmarkedObjects = new List<StarController>();

    private void Update()
    {
        if (_rotationCooldown == false)
        {
            _starView.Rotate(new Vector3(0, 0, 0.2f));
            Invoke("EnableRotation", _rotationDelay);
            _rotationCooldown = true;
        }
    }

    public void BookmarkToggleCurrent()
    {
        if (isObjectBookmarked(CurrentStarController))
        {
            _bookmarkedObjects.Remove(CurrentStarController);
        } else
        {
            _bookmarkedObjects.Add(CurrentStarController);
        }

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

    private void EnableRotation()
    {
        _rotationCooldown = false;
    }

    public float GetRotationDelay()
    {
        return _rotationDelay;
    }
}
