using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

using Zappar;
using UnityEngine.EventSystems;

public class ZapparInstantTrackingTarget : ZapparTrackingTarget, ZapparCamera.ICameraListener
{
    public GameObject LoadingBar;
    public GameObject _PinButton_Portrait, _UnPinButton_Portrait;
    public GameObject _PinButton_LandScape, _UnPinButton_LandScape;
    public GameObject SceneObject;

    bool pinClick = false;

    private IntPtr m_instantTracker;
    private bool m_userHasPlaced = false;
    private bool m_hasInitialised = false;
    private bool m_isMirrored = false;


    // Zoom Functionality
    private float _diff_Start, _diff_move;
    // ScaleRange.x defines min scale and ScaleRange.y defines max scale
    public Vector2 ScaleRange;


    // Touch Input Check
    private Touch theTouch;
    private float timeTouchBegan;
    private float timeTouchEnded;
    private Vector2 startPosition, endPosition;
    private float displayTime = 0.5f;
    private Touch touch;
    bool pinTry = false;
    // Touch Input Check

    private Vector3 startPositionSceneObject, startScaleSceneObject;

    [SerializeField]
    GameObject gameObjectContainingSceneZoom;
    SceneZoom sceneZoomClass;

    void Start()
    {
        ZapparCamera.Instance.RegisterCameraListener( this );
        LoadingBar.SetActive(true);

        sceneZoomClass = gameObjectContainingSceneZoom.GetComponent<SceneZoom>();
    }

    void OnEnable()
    {
        // SceneObject.transform.localScale = sceneZoomClass.startScaleSceneObject + new Vector3(4f, 4f, 4f);
        SceneObject.transform.localScale = new Vector3(4f, 4f, 4f);
        SceneObject.transform.localPosition = sceneZoomClass.startPositionSceneObject + new Vector3(0f, -0.8f, 5f);
    }

    public void OnZapparInitialised(IntPtr pipeline) 
    {
        m_instantTracker = Z.InstantWorldTrackerCreate( pipeline );
        m_hasInitialised = true;
    }

    public void OnMirroringUpdate(bool mirrored)
    {
        m_isMirrored = mirrored;
    }

    void UpdateTargetPose()
    {
        Matrix4x4 cameraPose = ZapparCamera.Instance.GetPose();
        Matrix4x4 instantTrackerPose = Z.InstantWorldTrackerAnchorPose(m_instantTracker, cameraPose, m_isMirrored);
        Matrix4x4 targetPose = Z.ConvertToUnityPose(instantTrackerPose);

        transform.localPosition = Z.GetPosition(targetPose);
        transform.localRotation = Z.GetRotation(targetPose);
        transform.localScale = Z.GetScale(targetPose);
    }

    void Update()
    {

        if (!m_hasInitialised) 
        {
            return;
        }

        // checkInput();

        if (!m_userHasPlaced)
        {
            Z.InstantWorldTrackerAnchorPoseSetFromCameraOffset(m_instantTracker, 0, 0, -5, Z.InstantTrackerTransformOrientation.MINUS_Z_AWAY_FROM_USER);
            UpdateTargetPose();
        }

        /*
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Stationary)
            {
                pinClick = !pinClick;
                m_userHasPlaced = !m_userHasPlaced;
                LoadingBar.SetActive(!pinClick);
            }
        }
        */

        /*
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Check if finger is over a UI element
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                Debug.Log("Touched the UI");
            }
        }
        */

        if (Input.touchCount > 0)
        {
	        theTouch = Input.GetTouch(0);
            // Check if finger is over a UI element
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                if (theTouch.phase == TouchPhase.Began)
                {
                    startPosition = theTouch.position;
                    timeTouchBegan = Time.time;
                }
                if (theTouch.phase == TouchPhase.Ended)
                {
                    pinTry = true;
                    endPosition = theTouch.position;
                    timeTouchEnded = Time.time;
                }
            }
        }
        // else if ((Time.time - timeTouchEnded > displayTime) && pinTry==true && (timeTouchEnded - timeTouchBegan < 1f))
        else if (pinTry==true && (timeTouchEnded - timeTouchBegan < 1f) && (Vector2.Distance(startPosition, endPosition) < 4f))
        {
            pinClick = !pinClick;
            m_userHasPlaced = !m_userHasPlaced;
            LoadingBar.SetActive(!pinClick);
            pinTry = false;
        }
        


        // UpdateTargetPose();
    }

    public void OnPinButtonClicked()
    {
        pinClick = !pinClick;

        _PinButton_Portrait.SetActive(!pinClick);
        _UnPinButton_Portrait.SetActive(pinClick);

        _PinButton_LandScape.SetActive(!pinClick);
        _UnPinButton_LandScape.SetActive(pinClick);

        m_userHasPlaced = !m_userHasPlaced;
        LoadingBar.SetActive(!pinClick);
    }

    public void OnARCloseButtonClicked()
    {
        LoadingBar.SetActive(false);
    }

    void OnDestroy()
    {
        if (m_hasInitialised) 
        {
            Z.InstantWorldTrackerDestroy(m_instantTracker);
        }
    }

    public override Matrix4x4 AnchorPoseCameraRelative()
    {
        return Z.InstantWorldTrackerAnchorPoseCameraRelative(m_instantTracker, m_isMirrored);
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
                    if (SceneObject.transform.localScale.y < ScaleRange.y)
                        SceneObject.transform.localScale += new Vector3(1f, 1f, 1f) * Time.deltaTime * 0.5f;

                    _diff_Start = _diff_move;
                }
                else if (_diff_move < _diff_Start)
                {
                    if (SceneObject.transform.localScale.y > ScaleRange.x)
                        SceneObject.transform.localScale -= new Vector3(1f, 1f, 1f) * Time.deltaTime * 0.5f;

                    _diff_Start = _diff_move;
                }
                else
                { }
            }
        }
    }
}
