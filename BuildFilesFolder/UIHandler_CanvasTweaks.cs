using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    GameObject _Zappar_Camera, _InstantTracker, _Virtual_Camera;

    [SerializeField]
    GameObject _AR_Button_Icon, _Virtual_Button_Icon;

    bool ARTextField = true, VirtualTextField = false;

    [SerializeField]
    GameObject _FadeInOut, _FeaturePlay;

    public Image fadeImage;

    [SerializeField]
    GameObject _CloseDoor_Icon, _OpenDoor_Icon;
    [SerializeField]
    GameObject Door_1, Door_2;

    bool doorOpen = false, doorClose = true;

    public Animator animatorDoor_1, animatorDoor_2;

    [SerializeField]
    GameObject _InsideFridge_Phone, _InsideFridge_Phone_SequenceSprite;
    [SerializeField]
    GameObject Object_1, Object_2, Object_3, Object_4, Object_5, Object_6, Object_7, Object_8, Object_9, Object_10;
    public Animator animatorInsideFridge_Phone;

    [SerializeField]
    GameObject _InsideFridge_Fridge, _InsideFridge_Fridge_SequenceSprite, _FridgeTVInsideScreen;

    public Animator animatorInsideFridge_Fridge;

    [SerializeField]
    GameObject _FridgeScreen, _FridgeScreen_SequenceSprite;

    public Animator animatorFridgeScreen;
    
    [SerializeField]
    GameObject _BixbyConnection, _AfterNote, _BeforeNote, _BixbyScreen;

    public Animator animator_BixbyConnection;

    [SerializeField]
    GameObject _FoodManagement_Label, _FamilyConnection_Label, _HomeControl_Label, _SpaceMaxTechnology_Label, _SmartView_Label;
    [SerializeField]
    GameObject _FoodManagement_Text, _FamilyConnection_Text, _HomeControl_Text, _SpaceMaxTechnology_Text;

    private float lerpTime;

    Vector3 initialPosition = new Vector3(0, 0, -3.15f);
    Vector3 positionFamilyConnection = new Vector3(0.12f, 0.32f, -1.57f);
    Vector3 positionHomeControl = new Vector3(0.34f, 0.3f, -1.72f);
    Vector3 positionHomeControl_Portrait = new Vector3(0.34f, 0.3f, -2.78f);


    bool FamilyConnection_ZoomIn = false, FamilyConnection_ZoomOut = false;
    bool HomeControl_ZoomIn = false, HomeControl_ZoomOut = false;
    bool ResetPosition = false;

    [SerializeField]
    GameObject _Panel_LandScape, _Panel_Portrait;
    public GameObject _Pin_UnPin_ButtonCover_LandSpace, _Pin_UnPin_ButtonCover_Portrait;

    public Camera virtualSceneCamera;
    bool currentHomeConnection = false;
    [SerializeField]
    GameObject _FoodManagement_Text_Portrait, _FamilyConnection_Text_Portrait, _HomeControl_Text_Portrait, _SpaceMaxTechnology_Text_Portrait;
    [SerializeField]
    GameObject InsideFridge_Phone_SequenceSprite_Portrait;
    public Animator animatorInsideFridge_Phone_Portrait;

    public GameObject SceneObject;

    public GameObject _Canvas_Portrait, _Canvas_LandScape; 

    // Start is called before the first frame update
    void Start()
    {
        if(Screen.width > Screen.height)
        {
            _Panel_LandScape.SetActive(true);
            _Panel_Portrait.SetActive(false);

            virtualSceneCamera.fieldOfView = 60f;
        }
        else
        {
            _Panel_LandScape.SetActive(false);
            _Panel_Portrait.SetActive(true);

            virtualSceneCamera.fieldOfView = 70f;
        }

        /*
        if(SystemInfo.deviceType == DeviceType.Handheld)
        {
            _Panel_LandScape.transform.localPosition = new Vector3(0, -113f, 0);
        }
        else
        {
            _Panel_LandScape.transform.localPosition = new Vector3(0, -240.28f, 0);
        }
        */

        animatorDoor_1 = Door_1.GetComponent<Animator>();
        animatorDoor_2 = Door_2.GetComponent<Animator>();

        animatorInsideFridge_Phone = _InsideFridge_Phone_SequenceSprite.GetComponent<Animator>();
        animatorInsideFridge_Fridge = _InsideFridge_Fridge_SequenceSprite.GetComponent<Animator>();
        animatorInsideFridge_Phone_Portrait = InsideFridge_Phone_SequenceSprite_Portrait.GetComponent<Animator>();
        

        animatorFridgeScreen = _FridgeScreen_SequenceSprite.GetComponent<Animator>();
        animator_BixbyConnection = _BixbyConnection.GetComponent<Animator>();

        // Screen.orientation = ScreenOrientation.Portrait;
    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.width > Screen.height)
        {
            _Panel_LandScape.SetActive(true);
            _Panel_Portrait.SetActive(false);

            virtualSceneCamera.fieldOfView = 60f;
        }
        else
        {
            _Panel_LandScape.SetActive(false);
            _Panel_Portrait.SetActive(true);

            if(currentHomeConnection)
            {
                virtualSceneCamera.fieldOfView = 78f;
            }
            else
            {
                virtualSceneCamera.fieldOfView = 70f;
            }
        }


        // if (SystemInfo.deviceType == DeviceType.Handheld)
        if(Screen.width > Screen.height)
        {
            // _Panel_LandScape.transform.localPosition = new Vector3(0, -130, 0);
            // _Panel_LandScape.transform.localPosition = new Vector3(0, -240, 0);

            _Canvas_LandScape.SetActive(true);
            _Canvas_Portrait.SetActive(false);

        }
        else
        {
            _Canvas_Portrait.SetActive(true);
            _Canvas_LandScape.SetActive(false);

        }





        if (FamilyConnection_ZoomIn == true && !(FamilyConnection_ZoomOut && HomeControl_ZoomIn && HomeControl_ZoomOut && ResetPosition))
        {
            _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, positionFamilyConnection, Time.deltaTime);

            if(_Virtual_Camera.transform.position == positionFamilyConnection)
            {
                FamilyConnection_ZoomIn = false;
            }
        }

        if (FamilyConnection_ZoomOut == true && !(FamilyConnection_ZoomIn && HomeControl_ZoomIn && HomeControl_ZoomOut && ResetPosition))
        {
            _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, initialPosition, Time.deltaTime);

            if (_Virtual_Camera.transform.position == initialPosition)
            {
                FamilyConnection_ZoomOut = false;
            }
        }


        if(HomeControl_ZoomIn == true && !(FamilyConnection_ZoomIn && FamilyConnection_ZoomOut && HomeControl_ZoomOut && ResetPosition))
        {
            if(currentHomeConnection == true)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, positionHomeControl_Portrait, Time.deltaTime);
            }
            else
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, positionHomeControl, Time.deltaTime);
            }

            if (((_Virtual_Camera.transform.position == positionHomeControl)&&!currentHomeConnection) || ((_Virtual_Camera.transform.position == positionHomeControl_Portrait) && currentHomeConnection))
            {
                HomeControl_ZoomIn = false;
            }
        }

        if (HomeControl_ZoomOut == true && !(FamilyConnection_ZoomIn && FamilyConnection_ZoomOut && HomeControl_ZoomIn && ResetPosition))
        {
            _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, initialPosition, Time.deltaTime);

            if (_Virtual_Camera.transform.position == initialPosition)
            {
                HomeControl_ZoomOut = false;
            }
        }


        if (ResetPosition == true && !(FamilyConnection_ZoomIn && FamilyConnection_ZoomOut && HomeControl_ZoomIn && HomeControl_ZoomOut))
        {
            _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, initialPosition, 5*Time.deltaTime);

            if (_Virtual_Camera.transform.position == initialPosition)
            {
                ResetPosition = false;
            }
        }
    }

    public void ResetActions()
    {
        _AfterNote.SetActive(false);
        _BixbyConnection.SetActive(false);
        _FridgeScreen.SetActive(false);
        _FridgeTVInsideScreen.SetActive(false);
        //_FeaturePlay.SetActive(true);

        FamilyConnection_ZoomIn = FamilyConnection_ZoomOut = HomeControl_ZoomIn = HomeControl_ZoomOut = false;

        StopAllCoroutines();
        doorOpen = false;
        ResetPosition = true;
        animatorDoor_1.Play("Still");
        animatorDoor_2.Play("Still");
        animatorInsideFridge_Phone.Play("Still"); 
        animatorInsideFridge_Fridge.Play("Still");
        animatorFridgeScreen.Play("Still");
        animator_BixbyConnection.Play("Still");
        

        _FoodManagement_Label.SetActive(false);
        _FamilyConnection_Label.SetActive(false);
        _HomeControl_Label.SetActive(false);
        _SpaceMaxTechnology_Label.SetActive(false);
        _SmartView_Label.SetActive(false);

        _FoodManagement_Text.SetActive(false);
        _FamilyConnection_Text.SetActive(false);
        _HomeControl_Text.SetActive(false);
        _SpaceMaxTechnology_Text.SetActive(false);

        _BixbyScreen.SetActive(false);
        _BeforeNote.SetActive(false);
        _AfterNote.SetActive(false);

        currentHomeConnection = false;

        _FoodManagement_Text_Portrait.SetActive(false);
        _FamilyConnection_Text_Portrait.SetActive(false);
        _HomeControl_Text_Portrait.SetActive(false);
        _SpaceMaxTechnology_Text_Portrait.SetActive(false);
        InsideFridge_Phone_SequenceSprite_Portrait.SetActive(false);
    }

    public void OnDoorOpenCloseClicked()
    {
        doorOpen = !doorOpen;
        doorClose = !doorClose;

        if (doorOpen == true)
        {
            // _CloseDoor_Icon.SetActive(true);
            // _OpenDoor_Icon.SetActive(false);

            animatorDoor_1.Play("Door_1_Animation");
            animatorDoor_2.Play("Door_2_Animation");

        }
        else
        {
            // _CloseDoor_Icon.SetActive(false);
            // _OpenDoor_Icon.SetActive(true);

            animatorDoor_1.Play("Door_1_Closing_Animation");
            animatorDoor_2.Play("Door_2_Closing_Animation");
        }

    }



    public IEnumerator FadeInOutTransition()
    {
        fadeImage.canvasRenderer.SetAlpha(1.0f);
        fadeImage.CrossFadeAlpha(0, 1.5f, false);
        yield return new WaitForSeconds(1.5f);

        _FadeInOut.SetActive(false);
    }

    public void OnARButtonClicked()
    {
        ARTextField = !ARTextField;
        VirtualTextField = !VirtualTextField;

        _Virtual_Camera.SetActive(true);
        _Zappar_Camera.SetActive(true);

        _FadeInOut.SetActive(true);
        StartCoroutine(FadeInOutTransition());

        // Camera Switch
        if (ARTextField == true)
        {
            _Virtual_Camera.SetActive(ARTextField);
            _Zappar_Camera.SetActive(VirtualTextField);
            _InstantTracker.SetActive(VirtualTextField);

            _Pin_UnPin_ButtonCover_LandSpace.SetActive(false);
            _Pin_UnPin_ButtonCover_Portrait.SetActive(false);

            SceneObject.transform.localScale = new Vector3(1f, 1f, 1f);
            SceneObject.transform.localPosition = new Vector3(0.1097156f, 0.03454585f, -0.2568359f);
        }
        else
        {
            _Zappar_Camera.SetActive(VirtualTextField);
            _Virtual_Camera.SetActive(ARTextField);
            _InstantTracker.SetActive(VirtualTextField);

            _Pin_UnPin_ButtonCover_LandSpace.SetActive(true);
            _Pin_UnPin_ButtonCover_Portrait.SetActive(true);
        }
        // Camera Switch

        _AR_Button_Icon.SetActive(ARTextField);
        _Virtual_Button_Icon.SetActive(VirtualTextField);
    }



    public IEnumerator FoodManagementTransition()
    {
        _FoodManagement_Label.SetActive(true);

        if(Screen.width > Screen.height)
        {
            _FoodManagement_Text.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _FoodManagement_Label.GetComponent<SpriteRenderer>().color = newColor;
                _FoodManagement_Text.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            _InsideFridge_Phone.SetActive(true);
            animatorInsideFridge_Phone.Play("InsideFridge_Phone_Animation");
            yield return new WaitForSeconds(2.01f);
            _FridgeTVInsideScreen.SetActive(true);
            yield return new WaitForSeconds(1.01f);
        }
        else
        {
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _FoodManagement_Label.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }
            _FoodManagement_Label.SetActive(false);
            _FoodManagement_Text_Portrait.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _FoodManagement_Text_Portrait.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            InsideFridge_Phone_SequenceSprite_Portrait.SetActive(true);
            animatorInsideFridge_Phone_Portrait.Play("InsideFridge_Phone_Portrait_Animation");
            yield return new WaitForSeconds(1.11f);
            _FridgeTVInsideScreen.SetActive(true);
            yield return new WaitForSeconds(2.307f);
            InsideFridge_Phone_SequenceSprite_Portrait.SetActive(false);
        }


        _InsideFridge_Fridge.SetActive(true);

        Object_1.SetActive(false);
        Object_2.SetActive(false);
        Object_3.SetActive(false);
        Object_4.SetActive(false);
        Object_5.SetActive(false);
        Object_6.SetActive(false);
        Object_7.SetActive(false);
        Object_8.SetActive(false);
        Object_9.SetActive(false);
        Object_10.SetActive(false);

        OnDoorOpenCloseClicked();
        yield return new WaitForSeconds(1.5f);
        
        animatorInsideFridge_Fridge.Play("InsideFridge_Fridge_Animation");
        yield return new WaitForSeconds(1);

        OnDoorOpenCloseClicked();
        yield return new WaitForSeconds(1.2f);

        _InsideFridge_Fridge.SetActive(false);

        Object_1.SetActive(true);
        Object_2.SetActive(true);
        Object_3.SetActive(true);
        Object_4.SetActive(true);
        Object_5.SetActive(true);
        Object_6.SetActive(true);
        Object_7.SetActive(true);
        Object_8.SetActive(true);
        Object_9.SetActive(true);
        Object_10.SetActive(true);

        yield return new WaitForSeconds(1f);
        _SmartView_Label.SetActive(false);

        if (Screen.width > Screen.height)
        {
            animatorInsideFridge_Phone.Play("InsideFridge_PhoneMovingOut_Animation");
            yield return new WaitForSeconds(1.5f);
        }
        _InsideFridge_Phone.SetActive(false);
  
        _FeaturePlay.SetActive(false);
    }

    public void OnFoodManagementClicked()
    {
        ResetActions();
        
        StartCoroutine(FoodManagementTransition());
    }



    public IEnumerator FamilyConnectionTransition()
    {
        _FamilyConnection_Label.SetActive(true);

        if (Screen.width > Screen.height)
        {
            _FamilyConnection_Text.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _FamilyConnection_Label.GetComponent<SpriteRenderer>().color = newColor;
                _FamilyConnection_Text.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }
        }
        else
        {
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _FamilyConnection_Label.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }
            _FamilyConnection_Label.SetActive(false);
            _FamilyConnection_Text_Portrait.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _FamilyConnection_Text_Portrait.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }
        }

        FamilyConnection_ZoomIn = true;
        yield return new WaitUntil(() => FamilyConnection_ZoomIn == false);
        yield return new WaitForSeconds(0.5f);

        animatorFridgeScreen.Play("FridgeScreen_Animation");
        yield return new WaitForSeconds(3.1f);

        FamilyConnection_ZoomOut = true;
        yield return new WaitForSeconds(1f);
        _FeaturePlay.SetActive(false);
    }

    public void OnFamilyConnectionClicked()
    {
        ResetActions();

        _FridgeScreen.SetActive(true);
        StartCoroutine(FamilyConnectionTransition());
    }



    public IEnumerator HomeConnectionTransition()
    {
        _HomeControl_Label.SetActive(true);

        if (Screen.width > Screen.height)
        {
            _HomeControl_Text.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _HomeControl_Label.GetComponent<SpriteRenderer>().color = newColor;
                _HomeControl_Text.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }
        }
        else
        {
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _HomeControl_Label.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }
            _HomeControl_Label.SetActive(false);
            _HomeControl_Text_Portrait.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _HomeControl_Text_Portrait.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }
        }

        HomeControl_ZoomIn = true;
        yield return new WaitUntil(() => HomeControl_ZoomIn == false);
        yield return new WaitForSeconds(0.5f);

        _BixbyConnection.SetActive(true);
        animator_BixbyConnection.Play("BixbyConnection_Animation");
        yield return new WaitForSeconds(2.15f);
        _BixbyScreen.SetActive(false);
        _BeforeNote.SetActive(true);
        yield return new WaitForSeconds(1);
        _BeforeNote.SetActive(false);
        _AfterNote.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        HomeControl_ZoomOut = true;
        yield return new WaitForSeconds(1.5f);

        _FeaturePlay.SetActive(false);
    }

    public void OnHomeConnectionClicked()
    {
        ResetActions();

        if (Screen.width < Screen.height)
        {
            currentHomeConnection = true;
        }
        _BixbyScreen.SetActive(true);
        StartCoroutine(HomeConnectionTransition());
    }



    public IEnumerator SpaceMaxTechnologyTransition()
    {
        _SpaceMaxTechnology_Label.SetActive(true);

        if (Screen.width > Screen.height)
        {
            _SpaceMaxTechnology_Text.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _SpaceMaxTechnology_Label.GetComponent<SpriteRenderer>().color = newColor;
                _SpaceMaxTechnology_Text.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }
        }
        else
        {
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _SpaceMaxTechnology_Label.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }
            _SpaceMaxTechnology_Label.SetActive(false);
            _SpaceMaxTechnology_Text_Portrait.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _SpaceMaxTechnology_Text_Portrait.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }
        }

        OnDoorOpenCloseClicked();
        yield return new WaitForSeconds(4);
        OnDoorOpenCloseClicked();

        _FeaturePlay.SetActive(false);
    }

    public void OnSpaceMaxTechnologyClicked()
    {
        ResetActions();

        StartCoroutine(SpaceMaxTechnologyTransition());
    }
}