using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftPositionSceneObject : MonoBehaviour
{
    [SerializeField]
    GameObject UIHandlerObject, _Virtual_Camera;
    UIHandler UIHandlerScript;

    public bool verticalPanAR = false;

    // Start is called before the first frame update
    void Start()
    {
        UIHandlerScript = UIHandlerObject.GetComponent<UIHandler>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Vertical Pan Segment

    bool isDragging = false;
    private Vector3 screenPoint;
    private Vector3 offset;
    
    
    void OnMouseDown()
    {
        screenPoint = UIHandler.Instance._Virtual_Camera.GetComponent<Camera>().WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - UIHandler.Instance._Virtual_Camera.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0, Input.mousePosition.y, 3));
    }
    void OnMouseDrag()
    {
        if (UIHandlerScript.VirtualTextField)
        {
            Vector3 curScreenPoint = new Vector3(0, Input.mousePosition.y, 3);
            Vector3 curPosition = UIHandler.Instance._Virtual_Camera.GetComponent<Camera>().ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
            var pos = transform.position;
            pos.y = Mathf.Clamp(transform.position.y, -1.5f, 0.5f);
            transform.position = pos;
        }
    }
    

    // Vertical Pan Segment
}
