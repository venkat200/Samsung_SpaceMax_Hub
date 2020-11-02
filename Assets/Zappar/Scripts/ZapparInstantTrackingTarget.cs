using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

using Zappar;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ZapparInstantTrackingTarget : ZapparTrackingTarget, ZapparCamera.ICameraListener
{
    public GameObject dummyCube;
    public GameObject LoadingBar;
    public GameObject _PinButton_Portrait, _UnPinButton_Portrait;
    public GameObject _PinButton_LandScape, _UnPinButton_LandScape;
    public GameObject SceneObject;
    public GameObject RefrigeratorOriginal, RefrigeratorTransparent;

    public bool ARPinned = false; 

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

    // [SerializeField]
    // GameObject gameObjectContainingARVerticalShift;
    // ShiftPositionSceneObject ShiftPositionSceneObjectScript;

    bool turnOnRaycast = false;
    Plane plane;
    public GameObject containerObject;
    Vector3 relativeContainerDifference;


    void Start()
    {
        ZapparCamera.Instance.RegisterCameraListener( this );
        // LoadingBar.SetActive(true);

        sceneZoomClass = gameObjectContainingSceneZoom.GetComponent<SceneZoom>();
        // ShiftPositionSceneObjectScript = gameObjectContainingARVerticalShift.GetComponent<ShiftPositionSceneObject>();

    }

    void OnEnable()
    {
        // SceneObject.transform.localScale = sceneZoomClass.startScaleSceneObject + new Vector3(4f, 4f, 4f);
        sceneZoomClass = gameObjectContainingSceneZoom.GetComponent<SceneZoom>();

        SceneObject.transform.localScale = new Vector3(20f, 20f, 20f);
        // SceneObject.transform.localPosition = sceneZoomClass.startPositionSceneObject + new Vector3(0.85f, -6.14f, 26.5f);
        // SceneObject.transform.localPosition = new Vector3(0.11f, 0.034f, -0.26f) + new Vector3(0.85f, -6.14f, 26.5f);
        SceneObject.transform.localPosition = new Vector3(0.96f, -6.106f, 26.24f);

        // relativeContainerDifference = new Vector3(SceneObject.transform.localPosition.x - containerObject.transform.localPosition.x, SceneObject.transform.localPosition.y - containerObject.transform.localPosition.y, SceneObject.transform.localPosition.z - containerObject.transform.localPosition.z);

        m_userHasPlaced = false;
        ARPinned = false;
        pinClick = false;
        pinTry = false;

        LoadingBar.SetActive(false);
        RefrigeratorTransparent.SetActive(false);
        RefrigeratorOriginal.SetActive(false);

        sceneZoomClass.TouchScaleSensitivityFactor = 0f;

        turnOnRaycast = false;
    }

    void OnDisable()
    {
        LoadingBar.SetActive(false);
        RefrigeratorTransparent.SetActive(false);
        RefrigeratorOriginal.SetActive(true);

        RefrigeratorOriginal.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
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


        /*
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

            if (!pinClick)
            {
                sceneZoomClass.TouchScaleSensitivityFactor = 0f;
                RefrigeratorTransparent.SetActive(true);
                RefrigeratorOriginal.SetActive(false);
                ARPinned = false;

                StopAllCoroutines();
                RefrigeratorOriginal.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
   
                ShiftPositionSceneObjectScript.verticalPanAR = true;
            }
            else
            {
                sceneZoomClass.TouchScaleSensitivityFactor = 0f;
                RefrigeratorTransparent.SetActive(false);
                RefrigeratorOriginal.SetActive(true);
                ARPinned = true;

                // RefrigeratorOriginal.transform.localScale = new Vector3(0f, 0f, 0f);
                StartCoroutine(ARZoomTransition());
                ShiftPositionSceneObjectScript.verticalPanAR = false;
            }

            pinTry = false;
        }
        */

        // UpdateTargetPose();


        if (turnOnRaycast)
        {
            /*
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit[] hit = Physics.RaycastAll(ray);

            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].collider != null)
                {
                    Debug.Log(hit[i].transform.gameObject.name);
                }
            }

            Debug.Log("Length = " + hit.Length);
            turnOnRaycast = false;
            */


            /*
            if (Input.GetMouseButton(0))
            {
                Vector3 point = new Vector3();
                Event currentEvent = Event.current;
                Vector2 mousePos = new Vector2();

                // Get the mouse position from Event.
                // Note that the y position from Event is inverted.
                // mousePos.x = Input.mouse.x.mousePosition.x;
                // mousePos.y = Camera.main.pixelHeight - currentEvent.mousePosition.y;

                point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Screen.height - Input.mousePosition.y, Camera.main.nearClipPlane));
                point += Camera.main.transform.forward * 35f;

                SceneObject.transform.position = new Vector3(point.x, Camera.main.transform.position.y - 8f, point.z);

                turnOnRaycast = false;
            }
            */



            /*
            if (Input.GetMouseButton(0))
            {
                //Create a ray from the Mouse click position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit[] hit = Physics.RaycastAll(ray);

                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i].collider != null && hit[i].transform.gameObject.name == "Plane")
                    {
                        Debug.Log("Hit Object = " + hit[i].transform.gameObject.name);
                        // containerObject.transform.position = hit[i].point;

                        SceneObject.transform.position = new Vector3(hit[i].point.x - relativeContainerDifference.x, hit[i].point.y - relativeContainerDifference.y, hit[i].point.z - relativeContainerDifference.z);

                        turnOnRaycast = false;
                    }
                }

                
                //Initialise the enter variable
                // float enter = 0.0f;

                // Debug.Log("ININININ");
                // if (plane.Raycast(ray, out enter))
                // {
                //     //Get the point that is clicked
                //     Vector3 hitPoint = ray.GetPoint(enter);
                //    Debug.Log("ININININ - IF");
                //    Debug.Log(hitPoint);

                //    //Move your cube GameObject to the point where you clicked
                //    containerObject.transform.position = hitPoint;
                // }              
            }
            */

            /*
            if (Input.GetMouseButton(0))
            {
                RaycastHit hit;
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(Camera.transform.position, transform.TransformDirection(Vector3.forward), out hit))
                {
                    if(hit.collider.tag == "Ground")
                    {
                        SceneObject.transform.position = new Vector3(hit.point.x, hit.point.y + 8f, hit.point.z);
                        Debug.Log("Did Ground Hit");

                    }
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    Debug.Log("Did Hit");
                }

            }
            */

            /*
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Ground")
                    {
                        SceneObject.transform.position = new Vector3(hit.point.x, hit.point.y + 8f, hit.point.z);
                        Debug.Log("Did Ground Hit");
                    }
                }
            }
            */

            /*
            if (Input.GetMouseButton(0))
            {
                RaycastHit hit;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                { 
                    
                    // if (hit.collider.tag == "Ground")
                    // {
                    //     SceneObject.transform.position = new Vector3(hit.point.x, hit.point.y - 10.154f, hit.point.z);

                    //    Debug.Log("Did Ground Hit");
                    // }
                    // else if (hit.collider.tag == "wall")
                    // {
                    //     SceneObject.transform.position = new Vector3(hit.point.x, hit.point.y - 10.154f, 35f);
                    //     Debug.Log("Did Cube Hit");
                    // }           
                    

                    if (hit.collider.tag == "Surface_Front")
                    {
                        SceneObject.transform.position = new Vector3(hit.point.x, -10.154f, 40f);
                        // SceneObject.transform.position = new Vector3(hit.point.x, hit.point.y + 10.154f, 40f);
                        Debug.Log("Did Front Surface Hit");
                    }

                    Debug.Log("Did Mouse Click");
                    Debug.Log(hit.collider.gameObject.tag);                
                }
            }
            */
        }



        if (turnOnRaycast)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 fingerPos = Input.mousePosition;
                float xPos = ((fingerPos.x / Screen.width) * 2) - 1;
                float yPos = ((fingerPos.y / Screen.height) * 2) - 1;
                Vector3 pos = new Vector3(xPos, yPos, fingerPos.z);
                Debug.Log("Calculated Position" + pos.ToString());
                fingerPos.z = 62.75194f * yPos + 54.71429f;
                Vector3 objPos1 = Camera.main.ScreenToWorldPoint(fingerPos);
               
                //Instantiate(dummyCube, objPos1, Quaternion.identity);

                fingerPos.y = fingerPos.y + Screen.height * 0.1f;
                //fingerPos.x = fingerPos.x + Screen.height * f;

                Vector3 objPos = Camera.main.ScreenToWorldPoint(fingerPos);
                //// GameObject go = Instantiate(SceneObject, objPos, Quaternion.identity);
                SceneObject.transform.position = objPos;
                //// Debug.Log("Object Position" + go.transform.position.ToString());              

                turnOnRaycast = false;
            }

            /*
            #elif UNITY_ANDROID
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Vector3 fingerPos = Input.GetTouch(0).position;
                float xPos = ((fingerPos.x / Screen.width) * 2) - 1;
                float yPos = ((fingerPos.y / Screen.height) * 2) - 1;
                if (yPos > 0)
                {
                    yPos = 0;
                }
                yPos += 1;
                Vector3 pos = new Vector3(xPos, yPos, fingerPos.z);
                Debug.Log("Calculated Position" + pos.ToString());
                //Vector3 objPos = Camera.main.ScreenToWorldPoint(fingerPos);
                Vector3 objPos = new Vector3(0, -3.5f, 0);
                objPos.x = 5.22484f * xPos - 1.98022f * yPos + 0.43191f;
                objPos.z = 19.64615f * yPos + 5.99092f;
                GameObject go = Instantiate(Obj, objPos, Quaternion.identity);
                Debug.Log("Object Position" + go.transform.position.ToString());
            }
            #endif
            */
        }

        // TextField.GetComponentInChildren<Text>().text = "X=" + SceneObject.transform.localPosition.x.ToString() + ", " + "Y=" + SceneObject.transform.localPosition.y.ToString() + ", " + "Z=" + SceneObject.transform.localPosition.z.ToString() + ", " + "S=" + SceneObject.transform.localScale.x.ToString();
    }


    IEnumerator ARZoomTransition()
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
        {
            RefrigeratorOriginal.transform.localScale = Vector3.MoveTowards(new Vector3(0f, 0f, 0f), new Vector3(0.35f, 0.35f, 0.35f), t);
            yield return null;

            if (RefrigeratorOriginal.transform.localScale == new Vector3(0.35f, 0.35f, 0.35f))
                break;
        }
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
        RefrigeratorTransparent.SetActive(!pinClick);
        SceneObject.SetActive(pinClick);
    }

    public void OnARCloseButtonClicked()
    {
        LoadingBar.SetActive(false);
    }

    public void OnTapButtonClicked()
    {
        pinClick = !pinClick;
        m_userHasPlaced = !m_userHasPlaced;

        LoadingBar.SetActive(!pinClick);

        if (!pinClick)
        {
            sceneZoomClass.TouchScaleSensitivityFactor = 0f;
            RefrigeratorTransparent.SetActive(true);
            RefrigeratorOriginal.SetActive(false);
            ARPinned = false;

            StopAllCoroutines();
            RefrigeratorOriginal.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            // ShiftPositionSceneObjectScript.verticalPanAR = true;
        }
        else
        {
            sceneZoomClass.TouchScaleSensitivityFactor = 0f;
            RefrigeratorTransparent.SetActive(false);
            RefrigeratorOriginal.SetActive(true);
            ARPinned = true;

            // RefrigeratorOriginal.transform.localScale = new Vector3(0f, 0f, 0f);
            StartCoroutine(ARZoomTransition());
            // ShiftPositionSceneObjectScript.verticalPanAR = false;
        }
    }

    
    public void TurnRayCastFloor()
    {
        turnOnRaycast = true;
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





    ////////////////////////////
    float posX, posY, posZ, Scale;
    [SerializeField]
    GameObject TextField;

    public void PosXP()
    {
        SceneObject.transform.localPosition = SceneObject.transform.localPosition + new Vector3(0.5f, 0f, 0f);
        posX = SceneObject.transform.localPosition.x;
    }

    public void PosXM()
    {
        SceneObject.transform.localPosition = SceneObject.transform.localPosition - new Vector3(0.5f, 0f, 0f);
        posX = SceneObject.transform.localPosition.x;
    }

    public void PosYP()
    {
        SceneObject.transform.localPosition = SceneObject.transform.localPosition + new Vector3(0f, 0.5f, 0f);
        posY = SceneObject.transform.localPosition.y;
    }

    public void PosYM()
    {
        SceneObject.transform.localPosition = SceneObject.transform.localPosition - new Vector3(0f, 0.5f, 0f);
        posY = SceneObject.transform.localPosition.y;
    }

    public void PosZP()
    {
        SceneObject.transform.localPosition = SceneObject.transform.localPosition + new Vector3(0f, 0f, 0.5f);
        posZ = SceneObject.transform.localPosition.z;
    }

    public void PosZM()
    {
        SceneObject.transform.localPosition = SceneObject.transform.localPosition - new Vector3(0f, 0f, 0.5f);
        posZ = SceneObject.transform.localPosition.z;
    }

    public void PosSP()
    {
        SceneObject.transform.localScale = SceneObject.transform.localScale + new Vector3(1f, 1f, 1f);
        Scale = SceneObject.transform.localScale.x;
    }

    public void PosSM()
    {
        SceneObject.transform.localScale = SceneObject.transform.localScale - new Vector3(1f, 1f, 1f);
        Scale = SceneObject.transform.localScale.x;
    }

    ////////////////////////////
}
