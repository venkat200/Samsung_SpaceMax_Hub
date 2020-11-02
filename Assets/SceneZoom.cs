using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneZoom : MonoBehaviour
{
    public GameObject _SceneObject;
    
    // Zoom Functionality
    private float _diff_Start, _diff_move;
    // ScaleRange.x defines min scale and ScaleRange.y defines max scale
    public Vector2 ScaleRange;

    public float ScrollSensitvity = 2f;
    public float TouchScaleSensitivityFactor = 1.5f;

    [SerializeField]
    GameObject ScaleRangeSelectionObject;
    UIHandler UIHandlerScript;

    public Vector3 startPositionSceneObject, startScaleSceneObject;

    // Start is called before the first frame update
    void Start()
    {
        UIHandlerScript = ScaleRangeSelectionObject.GetComponent<UIHandler>();

        // startScaleSceneObject = _SceneObject.transform.localScale;
        // startPositionSceneObject = _SceneObject.transform.localPosition;

        startScaleSceneObject = new Vector3(1f, 1f, 1f);
        startPositionSceneObject = new Vector3(0.11f, 0.034f, -0.26f);
    }

    void OnEnable()
    {
        // _SceneObject.transform.localPosition = startPositionSceneObject;
        // _SceneObject.transform.localScale = startScaleSceneObject;

        UIHandlerScript = ScaleRangeSelectionObject.GetComponent<UIHandler>();
        if (UIHandlerScript.VirtualTextField)
        {
            _SceneObject.transform.localScale = new Vector3(2f, 2f, 2f);
            ScaleRange = new Vector2(2f, 7f);
        }
        else
        {
            _SceneObject.transform.localScale = new Vector3(1f, 1f, 1f);
            ScaleRange = new Vector2(0.75f, 3.5f);
        }
        
        _SceneObject.transform.localPosition = new Vector3(0.11f, 0.034f, -0.26f);

    }

    // Update is called once per frame
    void Update()
    {
        if (UIHandlerScript.VirtualTextField)
        {
            ScaleRange = new Vector2(2f, 6f);
        }
        else
        {
            ScaleRange = new Vector2(0.75f, 3.5f);
        }
        checkInput();
    }

    void checkInput()
    {
        if (Input.touchCount > 1)
        {
            if (Input.touches[1].phase == TouchPhase.Began)
            {
                _diff_Start = Vector3.Distance(Input.touches[1].position, Input.touches[0].position);
            }

            if (Input.touches[0].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Moved)
            {
                _diff_move = Vector3.Distance(Input.touches[1].position, Input.touches[0].position);

                if (_diff_move > _diff_Start)
                {
                    if (_SceneObject.transform.localScale.y < ScaleRange.y)
                        _SceneObject.transform.localScale += new Vector3(1f, 1f, 1f) * Time.deltaTime * 0.5f * TouchScaleSensitivityFactor;

                    _diff_Start = _diff_move;
                }
                else if (_diff_move < _diff_Start)
                {
                    if (_SceneObject.transform.localScale.y > ScaleRange.x)
                        _SceneObject.transform.localScale -= new Vector3(1f, 1f, 1f) * Time.deltaTime * 0.5f * TouchScaleSensitivityFactor;

                    _diff_Start = _diff_move;
                }
                else
                { }
            }
        }


        //Zooming Input from our Mouse Scroll Wheel
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitvity;
            if (_SceneObject.transform.localScale.y <= ScaleRange.y && _SceneObject.transform.localScale.y >= ScaleRange.x)
            {
                if ( (_SceneObject.transform.localScale + (new Vector3(1f, 1f, 1f) * Time.deltaTime * ScrollAmount * 0.5f)).y > ScaleRange.y)
                {
                    _SceneObject.transform.localScale = new Vector3(ScaleRange.y, ScaleRange.y, ScaleRange.y);
                }
                else if ( (_SceneObject.transform.localScale + (new Vector3(1f, 1f, 1f) * Time.deltaTime * ScrollAmount * 0.5f)).y < ScaleRange.x)
                {
                    _SceneObject.transform.localScale = new Vector3(ScaleRange.x, ScaleRange.x, ScaleRange.x);
                }
                else
                {
                    _SceneObject.transform.localScale += new Vector3(1f, 1f, 1f) * Time.deltaTime * ScrollAmount * 0.5f;
                }
            }
            
            /*
            else
            {
                if (_SceneObject.transform.localScale.y > ScaleRange.y)
                {
                    _SceneObject.transform.localScale = new Vector3(ScaleRange.y, ScaleRange.y, ScaleRange.y);
                }

                if (_SceneObject.transform.localScale.y < ScaleRange.x)
                {
                    _SceneObject.transform.localScale = new Vector3(ScaleRange.x, ScaleRange.x, ScaleRange.x);
                }
            }
            */

        }
    }
}
