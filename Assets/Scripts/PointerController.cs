using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    [SerializeField]
    private LensGlassController LensGlassController = null;
    public List<GameObject> MainViewObjects = new List<GameObject>();
    public List<GameObject> ZoomedViewObjects = new List<GameObject>();
    private bool _zoomedIn = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && LensGlassController.CanCameraZoom)
        {
            if (_zoomedIn)
            {
                foreach (GameObject obj in MainViewObjects)
                {
                    if (obj != null)
                    {
                        obj.SetActive(true);
                    }
                }
                foreach (GameObject obj in ZoomedViewObjects)
                {
                    if (obj != null)
                    {
                        obj.SetActive(false);
                    }
                }
            } else
            {
                foreach (GameObject obj in MainViewObjects)
                {
                    if (obj != null)
                    {
                        obj.SetActive(false);
                    }
                }
                foreach (GameObject obj in ZoomedViewObjects)
                {
                    if (obj != null)
                    {
                        obj.SetActive(true);
                    }
                }
            }
            _zoomedIn = !_zoomedIn;
        }

        if (Input.GetMouseButtonDown(0) && _zoomedIn == true)
        {
            Vector3 pointerPosition = Camera.main.ViewportToWorldPoint(Input.mousePosition);
            Vector2 origin = new Vector2(pointerPosition.x, pointerPosition.y);
            Vector2 direction = Vector2.down;
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, Mathf.Infinity);

            if (hit && hit.transform.CompareTag("Space Object"))
            {
                StarController starController;
                if ((starController = hit.collider.GetComponent<StarController>()) != null)
                {
                    Debug.Log(starController.Name);
                    starController.ToggleMarker();
                }
                //ObjectDetailsManager.Instance.InitializePanelOf(hit.collider.GetComponent<StarController>(), pointerPosition);
            }
        }
    }
}
