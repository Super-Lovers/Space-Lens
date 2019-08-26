using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    public List<GameObject> MainViewObjects;
    public List<GameObject> ZoomedViewObjects;
    private bool _zoomedIn = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
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
            /*
            Vector3 pointerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 origin = new Vector2(pointerPosition.x, pointerPosition.y);
            Vector2 direction = Vector2.down;
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, Mathf.Infinity);
            
            if (hit && hit.transform.CompareTag("Space Object"))
            {
                ObjectDetailsManager.Instance.InitializePanelOf(hit.collider.GetComponent<StarController>(), pointerPosition);
            }
            */
        }
    }
}
