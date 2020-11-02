using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftPosition : MonoBehaviour
{
    [SerializeField]
    GameObject UIHandlerObject, _Virtual_Camera;
    UIHandler UIHandlerScript;

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
        screenPoint = _Virtual_Camera.GetComponent<Camera>().WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - _Virtual_Camera.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0, Input.mousePosition.y, 3));
    }
    
    void OnMouseDrag()
    {
        if (UIHandlerScript.ARTextField)
        {
            Vector3 curScreenPoint = new Vector3(0, Input.mousePosition.y, 3);
            Vector3 curPosition = _Virtual_Camera.GetComponent<Camera>().ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
            //Clamping position at bottom value with -2 and up value with 1
            var pos = transform.position;
            pos.y = Mathf.Clamp(transform.position.y, -1.5f, 0.5f);
            transform.position = pos;
        }
    }
    
    // Vertical Pan Segment

}
