using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    GameObject _Zappar_Camera, _InstantTracker, _Virtual_Camera;

    [SerializeField]
    GameObject _ModelSphere;

    [SerializeField]
    GameObject _AR_Button_Icon, _Virtual_Button_Icon;

    public bool ARTextField = true, VirtualTextField = false;

    [SerializeField]
    GameObject _FadeInOut, _FeaturePlay;

    public Image fadeImage;

    [SerializeField]
    GameObject _FoodManagement_Icon, _FoodManagement_Hide_Icon, _FamilyConnection_Icon, _FamilyConnection_Hide_Icon,
               _HomeConnection_Icon, _HomeConnection_Hide_Icon, _SpaceMaxTechnology_Icon, _SpaceMaxTechnology_Hide_Icon;

    [SerializeField]
    GameObject _FoodManagement_Icon_Portrait, _FoodManagement_Hide_Icon_Portrait, _FamilyConnection_Icon_Portrait, _FamilyConnection_Hide_Icon_Portrait,
               _HomeConnection_Icon_Portrait, _HomeConnection_Hide_Icon_Portrait, _SpaceMaxTechnology_Icon_Portrait, _SpaceMaxTechnology_Hide_Icon_Portrait;

    [SerializeField]
    GameObject _SpaceMaxSeries_Header, _ViewInside_Header;
    [SerializeField]
    GameObject _FridgeScreen_WithoutBottle, _AddBottle, _FridgeScreen_WithBottle;

    [SerializeField]
    GameObject _CloseDoor_Icon, _OpenDoor_Icon;
    [SerializeField]
    GameObject Door_1, Door_2;

    bool doorOpen = false, doorClose = true;

    Animator animatorDoor_1, animatorDoor_2;

    [SerializeField]
    GameObject _InsideFridge_Phone, _InsideFridge_Phone_SequenceSprite;
    [SerializeField]
    GameObject Object_1, Object_2, Object_3, Object_4, Object_5, Object_6, Object_7, Object_8, Object_9, Object_10;
    Animator animatorInsideFridge_Phone;

    [SerializeField]
    GameObject _InsideFridge_Fridge, _InsideFridge_Fridge_SequenceSprite, _FridgeTVInsideScreen;

    Animator animatorInsideFridge_Fridge;

    [SerializeField]
    GameObject _FridgeScreen, _FridgeScreen_SequenceSprite;

    Animator animatorFridgeScreen;

    [SerializeField]
    GameObject _BixbyConnection, _AfterNote, _BeforeNote, _BixbyScreen;

    Animator animator_BixbyConnection;

    [SerializeField]
    GameObject _FoodManagement_Label, _FamilyConnection_Label, _HomeControl_Label, _SpaceMaxTechnology_Label, _SmartView_Label;
    [SerializeField]
    GameObject _FoodManagement_Text, _FoodManagement_Text_AddOn, _FamilyConnection_Text, _HomeControl_Text, _SpaceMaxTechnology_Text;

    private float lerpTime;

    Vector3 initialPosition = new Vector3(0, 0, -3.15f);
    Vector3 positionFamilyConnection = new Vector3(0.12f, 0.32f, -1.57f);
    Vector3 positionHomeControl = new Vector3(0.34f, 0.3f, -1.72f);
    Vector3 positionHomeControl_Portrait = new Vector3(0.34f, 0.3f, -2.78f);
    Vector3 initialScale = new Vector3(1f, 1f, 1f);


    bool FamilyConnection_ZoomIn = false, FamilyConnection_ZoomOut = false;
    bool HomeControl_ZoomIn = false, HomeControl_ZoomOut = false;
    bool ResetPosition = false;

    [SerializeField]
    GameObject _Panel_LandScape, _Panel_Portrait, _Panel_Portrait_Close, _Panel_LandScape_Close;
    public GameObject _Pin_UnPin_ButtonCover_LandSpace, _Pin_UnPin_ButtonCover_Portrait;

    public Camera virtualSceneCamera;
    bool currentHomeConnection = false, currentFamilyConnection = false;
    [SerializeField]
    GameObject _FoodManagement_Text_Portrait, _FoodManagement_Text_Portrait_AddOn, _FamilyConnection_Text_Portrait, _FamilyConnection_Text_Portrait_AddOn, _HomeControl_Text_Portrait, _SpaceMaxTechnology_Text_Portrait, _SpaceMaxTechnology_Text_Portrait_AddOn;
    [SerializeField]
    GameObject InsideFridge_Phone_SequenceSprite_Portrait;
    Animator animatorInsideFridge_Phone_Portrait;

    public GameObject SceneObject;

    bool resetScaleTransform = false;

    Animator animatorPhoneConnection;
    [SerializeField]
    GameObject PhoneConnection_SequenceSprite;

    [SerializeField]
    GameObject _LargeSpace_Callout, _StockUp_Callout;
    [SerializeField]
    GameObject _SpaceFit, _SpaceFit_Extended;
    [SerializeField]
    GameObject FamilyConnection_Portrait_Label;
    Animator animatorSpaceFit;

    [SerializeField]
    GameObject _InstantSharing_Header, _InstantSharing_Header_Portrait;
   
    [SerializeField]
    GameObject _LargeSpace_Callout_Portrait, _StockUp_Callout_Portrait;

    [SerializeField]
    GameObject _Stock_1, _Stock_2, _Stock_3, _Stock_4, _Stock_5, _Stock_6, _Stock_7, _Stock_8;

    [SerializeField]
    GameObject _BuyNow_Portrait, _BuyNow_LandScape;

    [SerializeField]
    GameObject ResetDimension;
    ObjectRotate ObjectRotateScript;

    [SerializeField]
    GameObject Connecting, Connecting_Sprite;
    Animator animatorConnecting;

    [SerializeField]
    GameObject _DefaultFridgeScreen;

    [SerializeField]
    GameObject _InsideView_Fridge;
    Animator animatorInsideView_Fridge;

    [SerializeField]
    GameObject ScaleSelectionObject;
    SceneZoom SceneZoomScript;

    public IEnumerator InitialInfoShow()
    {
        StartCoroutine(InfoButtonTransition());
        yield return new WaitForSeconds(3f);
        StartCoroutine(InfoButtonTransition());
    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitialInfoShow());

        SceneZoomScript = ScaleSelectionObject.GetComponent<SceneZoom>();

        if (Screen.width > Screen.height)
        {
            _Panel_LandScape.SetActive(true);
            _Panel_Portrait.SetActive(false);

            _Panel_Portrait_Close.SetActive(false);
            _Panel_LandScape_Close.SetActive(true);

            virtualSceneCamera.fieldOfView = 60f;
            initialPosition = new Vector3(0, 0f, -3.15f);

        }
        else
        {
            _Panel_LandScape.SetActive(false);
            _Panel_Portrait.SetActive(true);

            _Panel_Portrait_Close.SetActive(true);
            _Panel_LandScape_Close.SetActive(false);

            virtualSceneCamera.fieldOfView = 75f;
            _Virtual_Camera.transform.position = new Vector3(0f, -0.38f, -3.15f);
            initialPosition = new Vector3(0, -0.38f, -3.15f);
        }

        /*
        if(SystemInfo.deviceType == DeviceType.Handheld)
        {
            _Panel_LandScape.transform.localPosition = new Vector3(0, -130f, 0);
        }
        else
        {
            _Panel_LandScape.transform.localPosition = new Vector3(0, -240f, 0);
        }
        */

        animatorDoor_1 = Door_1.GetComponent<Animator>();
        animatorDoor_2 = Door_2.GetComponent<Animator>();


        animatorInsideView_Fridge = _InsideView_Fridge.GetComponent<Animator>();
        animatorInsideFridge_Phone = _InsideFridge_Phone_SequenceSprite.GetComponent<Animator>();
        animatorInsideFridge_Fridge = _InsideFridge_Fridge_SequenceSprite.GetComponent<Animator>();
        animatorInsideFridge_Phone_Portrait = InsideFridge_Phone_SequenceSprite_Portrait.GetComponent<Animator>();
        animatorPhoneConnection = PhoneConnection_SequenceSprite.GetComponent<Animator>();

        animatorFridgeScreen = _FridgeScreen_SequenceSprite.GetComponent<Animator>();
        animator_BixbyConnection = _BixbyConnection.GetComponent<Animator>();
        animatorSpaceFit = _SpaceFit.GetComponent<Animator>();

        animatorMealMaker = _MealPlanner_Food.GetComponent<Animator>();
        animatorSmartView = _SmartView_Device.GetComponent<Animator>();
        animatorHomeEntertainment = _HomeEntertainment_Device.GetComponent<Animator>();
        animatorFoodPlanner_Fridge = _FoodPlanner_FridgeScreen.GetComponent<Animator>();

        animatorSpaceMax_Left = _SpaceMax_LeftColumn.GetComponent<Animator>();
        animatorSpaceMax_Right = _SpaceMax_RightColumn.GetComponent<Animator>();

        animatorConnecting = Connecting_Sprite.GetComponent<Animator>();
        animatorSmartView_FridgeScreen = _SmartViewScreen.GetComponent<Animator>();

        ObjectRotateScript = ResetDimension.GetComponent<ObjectRotate>();
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Screen.width > Screen.height || VirtualTextField)
        {
            _BuyNow_LandScape.SetActive(true);
            _BuyNow_Portrait.SetActive(false);

            _Panel_LandScape.SetActive(true);
            _Panel_Portrait.SetActive(false);

            if(dimensionClick)
            {
                virtualSceneCamera.fieldOfView = 70f;
            }
            else
            {
                virtualSceneCamera.fieldOfView = 60f;
            }

            initialPosition = new Vector3(0, 0f, -3.15f);

            InfoPanel_Portrait.SetActive(false);
            InfoPanel_Landscape.SetActive(true);

            if (VirtualTextField)
            {
                _Panel_Portrait_Close.SetActive(true);
                _Panel_LandScape_Close.SetActive(false);
            }
            else
            {
                _Panel_Portrait_Close.SetActive(false);
                _Panel_LandScape_Close.SetActive(true);
            }
        }
        else
        {
            _BuyNow_Portrait.SetActive(true);
            _BuyNow_LandScape.SetActive(false);

            _Panel_LandScape.SetActive(false);
            _Panel_Portrait.SetActive(true);

            if (currentFamilyConnection)
            {
                virtualSceneCamera.fieldOfView = 88f;
            }
            else
            {
                virtualSceneCamera.fieldOfView = 80f;
            }
            
            _Panel_Portrait_Close.SetActive(true);
            _Panel_LandScape_Close.SetActive(false);       

            initialPosition = new Vector3(0, -0.38f, -3.15f);

            InfoPanel_Landscape.SetActive(false);
            InfoPanel_Portrait.SetActive(true);
        }

        /*
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            _Panel_LandScape.transform.localPosition = new Vector3(0, -130f, 0);
        }
        else
        {
            _Panel_LandScape.transform.localPosition = new Vector3(0, -220f, 0);
        }
        */


        if (FamilyConnection_ZoomIn == true && !(FamilyConnection_ZoomOut && HomeControl_ZoomIn && HomeControl_ZoomOut && ResetPosition))
        {
            _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, positionFamilyConnection, Time.deltaTime);

            if (_Virtual_Camera.transform.position == positionFamilyConnection)
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


        if (HomeControl_ZoomIn == true && !(FamilyConnection_ZoomIn && FamilyConnection_ZoomOut && HomeControl_ZoomOut && ResetPosition))
        {
            if (currentHomeConnection == true)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, positionHomeControl_Portrait, Time.deltaTime);
            }
            else
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, positionHomeControl, Time.deltaTime);
            }

            if (((_Virtual_Camera.transform.position == positionHomeControl) && !currentHomeConnection) || ((_Virtual_Camera.transform.position == positionHomeControl_Portrait) && currentHomeConnection))
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
            _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, initialPosition, 5 * Time.deltaTime);

            if (_Virtual_Camera.transform.position == initialPosition)
            {
                ResetPosition = false;
            }
        }

        if (resetScaleTransform == true)
        {
            if (VirtualTextField)
            {
                initialScale = new Vector3(4f, 4f, 4f);
            }
            else
            {
                initialScale = new Vector3(1f, 1f, 1f);
            }

            SceneObject.transform.localScale = Vector3.MoveTowards(SceneObject.transform.localScale, initialScale, 5 * Time.deltaTime);
            if (SceneObject.transform.localScale == initialScale)
            {
                resetScaleTransform = false;
            }
        }
    }

    IEnumerator resetTransformation()
    {
        resetScaleTransform = true;
        yield return new WaitUntil(() => resetScaleTransform == false);
    }

    public void ResetActions()
    {
        // StartCoroutine(resetTransformation());
        resetScaleTransform = true;

        _AfterNote.SetActive(false);
        _BixbyConnection.SetActive(false);
        _FridgeScreen.SetActive(false);
        _FridgeTVInsideScreen.SetActive(false);
        _InsideFridge_Phone.SetActive(false);
        _InsideView_Fridge.SetActive(false);
        //_FeaturePlay.SetActive(true);

        FamilyConnection_ZoomIn = FamilyConnection_ZoomOut = HomeControl_ZoomIn = HomeControl_ZoomOut = false;

        StopAllCoroutines();
        doorOpen = false;
        ResetPosition = true;
        animatorDoor_1.Play("Still");
        animatorDoor_2.Play("Still");
        animatorInsideView_Fridge.Play("Still");
        animatorInsideFridge_Phone.Play("Still");
        animatorInsideFridge_Fridge.Play("Still");
        animatorFridgeScreen.Play("Still");
        animator_BixbyConnection.Play("Still");
        animatorPhoneConnection.Play("Still");
        animatorMealMaker.Play("Still");
        animatorSmartView.Play("Still");
        animatorHomeEntertainment.Play("Still");
        animatorConnecting.Play("Still");
        animatorFoodPlanner_Fridge.Play("Still");
        animatorSpaceMax_Left.Play("Still");
        animatorSpaceMax_Right.Play("Still");
        animatorSmartView_FridgeScreen.Play("Still");

        // _SpaceMaxTechnology_Container.SetActive(false);
        _SpaceMax_LeftColumn.SetActive(false);
        _SpaceMax_RightColumn.SetActive(false);

        _FoodManagement_Label.SetActive(false);
        _FamilyConnection_Label.SetActive(false);
        _HomeControl_Label.SetActive(false);
        _SpaceMaxTechnology_Label.SetActive(false);
        _SmartView_Label.SetActive(false);

        _SpaceMaxSeries_Header.SetActive(false);
        _ViewInside_Header.SetActive(false);
        _InstantSharing_Header.SetActive(false);
        _InstantSharing_Header_Portrait.SetActive(false);

        _FoodManagement_Text.SetActive(false);
        _FoodManagement_Text_AddOn.SetActive(false);
        _FamilyConnection_Text.SetActive(false);
        _HomeControl_Text.SetActive(false);
        _SpaceMaxTechnology_Text.SetActive(false);

        _BixbyScreen.SetActive(false);
        _BeforeNote.SetActive(false);
        _AfterNote.SetActive(false);

        currentHomeConnection = false;
        currentFamilyConnection = false;


        _FoodManagement_Text_Portrait.SetActive(false);
        _FoodManagement_Text_Portrait_AddOn.SetActive(false);
        _FamilyConnection_Text_Portrait.SetActive(false);
        _FamilyConnection_Text_Portrait_AddOn.SetActive(false);
        _HomeControl_Text_Portrait.SetActive(false);
        _SpaceMaxTechnology_Text_Portrait.SetActive(false);
        _SpaceMaxTechnology_Text_Portrait_AddOn.SetActive(false);
        InsideFridge_Phone_SequenceSprite_Portrait.SetActive(false);

        _AddBottle.SetActive(false);
        _FridgeScreen_WithBottle.SetActive(false);
        _FridgeScreen_WithoutBottle.SetActive(false);


        _SpaceMaxSeries_Header.transform.localPosition = new Vector3(2.64f, 0.75f, -0.52f);
        _ViewInside_Header.transform.localPosition = new Vector3(2.64f, 0.197f, -0.52f);
        // _InstantSharing_Header.transform.localPosition = new Vector3(0.24f, 1.08f, -2.4f);
        // _InstantSharing_Header.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);


        PhoneConnection_SequenceSprite.SetActive(false);

        _LargeSpace_Callout.SetActive(false);
        _StockUp_Callout.SetActive(false);
        _SpaceFit.SetActive(false);
        _SpaceFit_Extended.SetActive(false);

        _Stock_1.SetActive(true);
        _Stock_2.SetActive(true);
        _Stock_3.SetActive(true);
        _Stock_4.SetActive(true);
        _Stock_5.SetActive(true);
        _Stock_6.SetActive(true);
        _Stock_7.SetActive(true);
        _Stock_8.SetActive(true);

        FamilyConnection_Portrait_Label.SetActive(false);
        _LargeSpace_Callout_Portrait.SetActive(false);
        _StockUp_Callout_Portrait.SetActive(false);

        _HomeScreen.SetActive(false);
        _ExploreBixby_Label.SetActive(false);
        _ExploreBixby_Text_Portrait.SetActive(false);
        _MealPlannerScreen.SetActive(false);
        _MealPlanner_Text_Portrait.SetActive(false);
        _MealPlanner_Text.SetActive(false);
        _MealPlanner_Food.SetActive(false);
        _FoodPlanner_FridgeScreen.SetActive(false);
        _SmartViewScreen.SetActive(false);
        _SmartView_Text.SetActive(false);
        _SmartView_Text_Portrait.SetActive(false);
        _SmartView_Device.SetActive(false);
        _SmartView_ScreenChange_Fridge.SetActive(false);

        _HomeEntertainmentScreen.SetActive(false);
        _HomeEntertainment_Text.SetActive(false);
        _HomeEntertainment_Text_Portrait.SetActive(false);
        _HomeEntertainment_Device.SetActive(false);

        Connecting.SetActive(false);

        Arrow_L.SetActive(false);
        Arrow_H.SetActive(false);
        Arrow_B.SetActive(false);

        _DefaultFridgeScreen.SetActive(true);

    }

    public void ResetDimensionAction()
    {
        if (dimensionClick == true)
        {
            OnDimensionClicked();
            ObjectRotateScript.OnDimensionClicked();
        }
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
        ResetActions();

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

            SceneZoomScript.TouchScrollSensitivity = 1.5f;
            // _Pin_UnPin_ButtonCover_LandSpace.SetActive(false);
            // _Pin_UnPin_ButtonCover_Portrait.SetActive(false);

            SceneObject.transform.localScale = new Vector3(1f, 1f, 1f);
            SceneObject.transform.localPosition = new Vector3(0.1097156f, 0.03454585f, -0.2568359f);
            SceneObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else
        {
            _Zappar_Camera.SetActive(VirtualTextField);
            _Virtual_Camera.SetActive(ARTextField);
            _InstantTracker.SetActive(VirtualTextField);

            SceneZoomScript.TouchScrollSensitivity = 3f;

            StartCoroutine(InitialInfoShow());

            // _Pin_UnPin_ButtonCover_LandSpace.SetActive(true);
            // _Pin_UnPin_ButtonCover_Portrait.SetActive(true);

            SceneObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        // Camera Switch

        _AR_Button_Icon.SetActive(ARTextField);
        _Virtual_Button_Icon.SetActive(VirtualTextField);
    }



    public IEnumerator FoodManagementTransition()
    {
        // _InsideView_Fridge.SetActive(false);
        // animatorInsideView_Fridge.Play("Still");

        if (Screen.width > Screen.height || VirtualTextField)
        {
            /*
            _SpaceMaxSeries_Header.SetActive(true);
            _SpaceMaxSeries_Header.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(1f);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(1, 0, t));
                _SpaceMaxSeries_Header.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            _SpaceMaxSeries_Header.SetActive(false);
            */

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(0.0f, 0.25f, -2.19f), t);
                yield return null;

                if (_Virtual_Camera.transform.position == new Vector3(0.0f, 0.25f, -2.19f))
                    break;
            }

            yield return new WaitForSeconds(1f);

            _DefaultFridgeScreen.SetActive(false);

            _InsideView_Fridge.SetActive(true);
            animatorInsideView_Fridge.Play("InsideView_Fridge_Animation");

            yield return new WaitForSeconds(2f);
            _InsideFridge_Phone.SetActive(true);
            animatorInsideFridge_Phone.Play("InsideFridge_Phone_Animation");

            // _FoodManagement_Label.SetActive(true);
            _FoodManagement_Text.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                // _FoodManagement_Label.GetComponent<SpriteRenderer>().color = newColor;
                _FoodManagement_Text.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
            

            Connecting.SetActive(true);
            Connecting.transform.localPosition = new Vector3(0.27f, 0.38f, -0.526123f);
            animatorConnecting.Play("Connecting_Animation");

            

            yield return new WaitForSeconds(3f);
            // _FridgeTVInsideScreen.SetActive(true);

            Connecting.SetActive(false);
            yield return new WaitForSeconds(1f);

            animatorInsideFridge_Phone.Play("InsideFridge_PhoneMovingOut_Animation");
            yield return new WaitForSeconds(1.5f);
            _InsideFridge_Phone.SetActive(false);

            /*
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(0.02f, 0.36f, -1.61f), t);
                yield return null;

                if (_Virtual_Camera.transform.position == new Vector3(0.02f, 0.36f, -1.61f))
                    break;
            }
            */

            yield return new WaitForSeconds(1.5f);

            _FoodManagement_Text.SetActive(false);
            _FoodManagement_Text_AddOn.SetActive(true);

            _FridgeTVInsideScreen.SetActive(false);
            _InsideView_Fridge.SetActive(false);
            _FridgeScreen_WithoutBottle.SetActive(true);
            yield return new WaitForSeconds(1f);

            _AddBottle.SetActive(true);
            yield return new WaitForSeconds(2.5f);

            _AddBottle.SetActive(false);
            _FridgeScreen_WithoutBottle.SetActive(false);
            _FridgeScreen_WithBottle.SetActive(true);
            yield return new WaitForSeconds(1f);

            yield return new WaitForSeconds(2f);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, initialPosition, t);
                yield return null;

                if (_Virtual_Camera.transform.position == initialPosition)
                    break;
            }

        }
        else
        {
            
            _FoodManagement_Text_Portrait.SetActive(true);

            /*
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(0.4f, -0.11f - 0.38f, -3.15f), t);
                yield return null;

                if (_Virtual_Camera.transform.position == new Vector3(0.4f, -0.11f - 0.38f, -3.15f))
                    break;
            }
            */

            // _SpaceMaxSeries_Header.SetActive(true);
            // _SpaceMaxSeries_Header.transform.localPosition = new Vector3(2f, 1.74f, 1.12f);

            /*
            _SpaceMaxSeries_Header.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(1f);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(1, 0, t));
                _SpaceMaxSeries_Header.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }
            */

            _DefaultFridgeScreen.SetActive(false);

            _InsideView_Fridge.SetActive(true);
            animatorInsideView_Fridge.Play("InsideView_Fridge_Animation");

            // _SpaceMaxSeries_Header.SetActive(false);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(0.4f, -0.38f, -3.15f), t);
                yield return null;

                if (_Virtual_Camera.transform.position == new Vector3(0.4f, -0.38f, -3.15f))
                    break;
            }

            InsideFridge_Phone_SequenceSprite_Portrait.SetActive(true);
            animatorInsideFridge_Phone_Portrait.Play("InsideFridge_Phone_Portrait_Animation");
            yield return new WaitForSeconds(0.9f);

            Connecting.SetActive(true);
            Connecting.transform.localPosition = new Vector3(0.415f, 0.38f, -0.3f);
            animatorConnecting.Play("Connecting_Animation");

            yield return new WaitForSeconds(0.8f);
            // _FridgeTVInsideScreen.SetActive(true);
            yield return new WaitForSeconds(3f);

            Connecting.SetActive(false);

            yield return new WaitForSeconds(1.807f);
            InsideFridge_Phone_SequenceSprite_Portrait.SetActive(false);


            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, initialPosition, t);
                yield return null;

                if (_Virtual_Camera.transform.position == initialPosition)
                    break;
            }

            _FoodManagement_Text_Portrait.SetActive(false);
            _FoodManagement_Text_Portrait_AddOn.SetActive(true);

            // _FridgeTVInsideScreen.SetActive(false);
            _InsideView_Fridge.SetActive(false);
            _FridgeScreen_WithoutBottle.SetActive(true);
            yield return new WaitForSeconds(1f);

            _AddBottle.SetActive(true);
            yield return new WaitForSeconds(2.5f);

            _AddBottle.SetActive(false);
            _FridgeScreen_WithoutBottle.SetActive(false);
            _FridgeScreen_WithBottle.SetActive(true);
            yield return new WaitForSeconds(1f);
        }

        /*
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
        yield return new WaitForSeconds(1.5f);

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
        */



        /*
        if(Screen.width < Screen.height)
        {
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(0.4f, -0.11f - 0.38f, -3.15f), t);
                yield return null;

                if (_Virtual_Camera.transform.position == new Vector3(0.4f, -0.11f - 0.38f, -3.15f))
                    break;
            }

            _ViewInside_Header.transform.localPosition = new Vector3(2.21f, 0.197f + 0.38f, 1.41f);
        }
        */

        // _ViewInside_Header.SetActive(true);
        
        /*
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
            _ViewInside_Header.GetComponent<SpriteRenderer>().color = newColor;
            yield return null;
        }
        */
        
    }

    public void OnFoodManagementClicked()
    {
        _FoodManagement_Icon.SetActive(true);
        _FoodManagement_Hide_Icon.SetActive(false);
        _FamilyConnection_Icon.SetActive(false);
        _FamilyConnection_Hide_Icon.SetActive(true);
        _HomeConnection_Icon.SetActive(false);
        _HomeConnection_Hide_Icon.SetActive(true);
        _SpaceMaxTechnology_Icon.SetActive(false);
        _SpaceMaxTechnology_Hide_Icon.SetActive(true);

        _FoodManagement_Icon_Portrait.SetActive(true);
        _FoodManagement_Hide_Icon_Portrait.SetActive(false);
        _FamilyConnection_Icon_Portrait.SetActive(false);
        _FamilyConnection_Hide_Icon_Portrait.SetActive(true);
        _HomeConnection_Icon_Portrait.SetActive(false);
        _HomeConnection_Hide_Icon_Portrait.SetActive(true);
        _SpaceMaxTechnology_Icon_Portrait.SetActive(false);
        _SpaceMaxTechnology_Hide_Icon_Portrait.SetActive(true);

        ResetActions();
        ResetDimensionAction();

        StartCoroutine(FoodManagementTransition());
    }


    public IEnumerator FamilyConnectionTransition()
    {

        if (Screen.width > Screen.height || VirtualTextField)
        {
            _InstantSharing_Header.SetActive(true);
            _FamilyConnection_Label.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1); ;
            // _FamilyConnection_Label.SetActive(true);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _InstantSharing_Header.GetComponent<SpriteRenderer>().color = newColor;
                _FamilyConnection_Label.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            yield return new WaitForSeconds(1.5f);


            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 250)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(0.18f, 0.3f, -1.8f), t);
                yield return null;

                if (_Virtual_Camera.transform.position == new Vector3(0.18f, 0.3f, -1.8f))
                    break;
            }

            PhoneConnection_SequenceSprite.SetActive(true);
            // _FamilyConnection_Label.SetActive(false);

            animatorPhoneConnection.Play("PhoneConnection_Animation");

            yield return new WaitForSeconds(2f);

            Connecting.SetActive(true);
            Connecting.transform.localPosition = new Vector3(0.29f, 0.38f, -0.3f);
            animatorConnecting.Play("Connecting_Animation");

            // FamilyConnection_ZoomIn = true;
            // yield return new WaitUntil(() => FamilyConnection_ZoomIn == false);
            yield return new WaitForSeconds(0.5f);

            /*
            _InstantSharing_Header.SetActive(true);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _InstantSharing_Header.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }
            */

            yield return new WaitForSeconds(4f);

            _DefaultFridgeScreen.SetActive(false);
            _FridgeScreen.SetActive(true);

            animatorFridgeScreen.Play("FridgeScreen_Animation");

            yield return new WaitForSeconds(4.1f);

            FamilyConnection_ZoomOut = true;

            /*
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 350)
            {
                _InstantSharing_Header.transform.localPosition = Vector3.MoveTowards(_InstantSharing_Header.transform.localPosition, new Vector3(-0.64f, 0.79f, -2.4f), t);

                yield return null;

                if (_InstantSharing_Header.transform.localPosition == new Vector3(-0.64f, 0.79f, -2.4f))
                    break;
            }
            */

            /*
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 1000)
            {
                _InstantSharing_Header.transform.localScale = Vector3.MoveTowards(_InstantSharing_Header.transform.localScale, new Vector3(0.15f, 0.15f, 0.15f), t);
                if (_InstantSharing_Header.transform.localScale == new Vector3(0.15f, 0.15f, 0.15f))
                    break;
            }
            */
            yield return new WaitForSeconds(1f);
            _FeaturePlay.SetActive(false);
            
        }
        else
        {
            /*
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 500)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(0.4f, -0.11f, -3.15f), t);
                yield return null;

                if (_Virtual_Camera.transform.position == new Vector3(0.4f, -0.11f, -3.15f))
                    break;
            }
            */
            // FamilyConnection_Portrait_Label.SetActive(true);

            currentFamilyConnection = true;

            _FamilyConnection_Label.transform.localScale = new Vector3();
            _FamilyConnection_Label.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            _FamilyConnection_Label.SetActive(true);

            yield return new WaitForSeconds(1f);


            _FamilyConnection_Label.SetActive(false);


            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 500)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(0.51f, 0.15f, -1.99f), t);
                yield return null;

                if (_Virtual_Camera.transform.position == new Vector3(0.51f, 0.15f, -1.99f))
                    break;
            }


            PhoneConnection_SequenceSprite.SetActive(true);
            _FamilyConnection_Text_Portrait.SetActive(true);

            animatorPhoneConnection.Play("PhoneConnection_Animation");
            yield return new WaitForSeconds(2f);


            Connecting.SetActive(true);
            Connecting.transform.localPosition = new Vector3(0.32f, 0.38f, -0.306f);
            animatorConnecting.Play("Connecting_Animation");

            _FamilyConnection_Text_Portrait.SetActive(false);
            _FamilyConnection_Text_Portrait_AddOn.SetActive(true);

            yield return new WaitForSeconds(0.5f);


            // _InstantSharing_Header_Portrait.SetActive(true);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _InstantSharing_Header_Portrait.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            yield return new WaitForSeconds(3.5f);

            _DefaultFridgeScreen.SetActive(false);
            _FridgeScreen.SetActive(true);

            animatorFridgeScreen.Play("FridgeScreen_Animation");
            yield return new WaitForSeconds(4.1f);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 500)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, initialPosition, t);
                yield return null;

                if (_Virtual_Camera.transform.position == initialPosition)
                    break;
            }


        }


    }

    public void OnFamilyConnectionClicked()
    {
        ResetActions();
        ResetDimensionAction();

        _FoodManagement_Icon.SetActive(false);
        _FoodManagement_Hide_Icon.SetActive(true);
        _FamilyConnection_Icon.SetActive(true);
        _FamilyConnection_Hide_Icon.SetActive(false);
        _HomeConnection_Icon.SetActive(false);
        _HomeConnection_Hide_Icon.SetActive(true);
        _SpaceMaxTechnology_Icon.SetActive(false);
        _SpaceMaxTechnology_Hide_Icon.SetActive(true);

        _FoodManagement_Icon_Portrait.SetActive(false);
        _FoodManagement_Hide_Icon_Portrait.SetActive(true);
        _FamilyConnection_Icon_Portrait.SetActive(true);
        _FamilyConnection_Hide_Icon_Portrait.SetActive(false);
        _HomeConnection_Icon_Portrait.SetActive(false);
        _HomeConnection_Hide_Icon_Portrait.SetActive(true);
        _SpaceMaxTechnology_Icon_Portrait.SetActive(false);
        _SpaceMaxTechnology_Hide_Icon_Portrait.SetActive(true);

        StartCoroutine(FamilyConnectionTransition());
    }



    public void OnHomeConnectionClicked()
    {
        ResetActions();
        ResetDimensionAction();

        _FoodManagement_Icon.SetActive(false);
        _FoodManagement_Hide_Icon.SetActive(false);
        _FamilyConnection_Icon.SetActive(false);
        _FamilyConnection_Hide_Icon.SetActive(false);
        _HomeConnection_Icon.SetActive(false);
        _HomeConnection_Hide_Icon.SetActive(false);
        _SpaceMaxTechnology_Icon.SetActive(false);
        _SpaceMaxTechnology_Hide_Icon.SetActive(false);

        _ExploreBixby_Icon.SetActive(true);
        _ExploreBixby_Icon_Hide.SetActive(false);
        _MealPlanner_Icon.SetActive(true);
        _MealPlanner_Icon_Hide.SetActive(false);
        _SmartView_Icon.SetActive(true);
        _SmartView_Icon_Hide.SetActive(false);
        _HomeEntertainment_Icon.SetActive(true);
        _HomeEntertainment_Icon_Hide.SetActive(false);
        _CloseHomeConnectionIcon.SetActive(true);


        _FoodManagement_Icon_Portrait.SetActive(false);
        _FoodManagement_Hide_Icon_Portrait.SetActive(false);
        _FamilyConnection_Icon_Portrait.SetActive(false);
        _FamilyConnection_Hide_Icon_Portrait.SetActive(false);
        _HomeConnection_Icon_Portrait.SetActive(false);
        _HomeConnection_Hide_Icon_Portrait.SetActive(false);
        _SpaceMaxTechnology_Icon_Portrait.SetActive(false);
        _SpaceMaxTechnology_Hide_Icon_Portrait.SetActive(false);

        _ExploreBixby_Icon_Portrait.SetActive(true);
        _ExploreBixby_Icon_Hide_Portrait.SetActive(false);
        _MealPlanner_Icon_Portrait.SetActive(true);
        _MealPlanner_Icon_Hide_Portrait.SetActive(false);
        _SmartView_Icon_Portrait.SetActive(true);
        _SmartView_Icon_Hide_Portrait.SetActive(false);
        _HomeEntertainment_Icon_Portrait.SetActive(true);
        _HomeEntertainment_Icon_Hide_Portrait.SetActive(false);


        _CloseHomeConnectionIcon_Portrait.SetActive(true);

        /*
        if(Screen.width > Screen.height || VirtualTextField)
        {
            _CloseHomeControlButton_Panel_Portrait.SetActive(false);
        }
        else
        {
            _CloseHomeControlButton_Panel_Portrait.SetActive(true);
        }
        */
    }

    [SerializeField]
    GameObject _SpaceMaxTechnology_Container, _SpaceMax_LeftColumn, _SpaceMax_RightColumn;
    Animator animatorSpaceMax_Left, animatorSpaceMax_Right;

    public IEnumerator SpaceMaxTechnologyTransition()
    {
        _Stock_1.SetActive(false);
        _Stock_2.SetActive(false);
        _Stock_3.SetActive(false);
        _Stock_4.SetActive(false);
        _Stock_5.SetActive(false);
        _Stock_6.SetActive(false);
        _Stock_7.SetActive(false);
        _Stock_8.SetActive(false);

        if (Screen.width > Screen.height || VirtualTextField)
        {
            _SpaceMaxTechnology_Label.SetActive(true);
            // _StockUp_Callout.SetActive(true);
            // _SpaceMaxTechnology_Text.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _SpaceMaxTechnology_Label.GetComponent<SpriteRenderer>().color = newColor;
                // _StockUp_Callout.GetComponent<SpriteRenderer>().color = newColor;
                // _SpaceMaxTechnology_Text.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }
            yield return new WaitForSeconds(1f);

            OnDoorOpenCloseClicked();
            yield return new WaitForSeconds(0.1f);
            // _SpaceMaxTechnology_Container.SetActive(true);
            _SpaceMax_LeftColumn.SetActive(true);
            _SpaceMax_RightColumn.SetActive(true);

            // _LargeSpace_Callout.SetActive(true);
            yield return new WaitForSeconds(0.9f);

            animatorSpaceMax_Left.Play("SpaceMax_Left_Animation");
            animatorSpaceMax_Right.Play("SpaceMax_Right_Animation");

            yield return new WaitForSeconds(0.833f);
            animatorSpaceMax_Left.Play("Still");
            animatorSpaceMax_Right.Play("Still");
            yield return new WaitForSeconds(0.2f);

            animatorSpaceMax_Left.Play("SpaceMax_Left_Animation");
            animatorSpaceMax_Right.Play("SpaceMax_Right_Animation");

            yield return new WaitForSeconds(0.833f);
            animatorSpaceMax_Left.Play("Still");
            animatorSpaceMax_Right.Play("Still");
            yield return new WaitForSeconds(0.2f);

            animatorSpaceMax_Left.Play("SpaceMax_Left_Animation");
            animatorSpaceMax_Right.Play("SpaceMax_Right_Animation");
            yield return new WaitForSeconds(5.5f);

            /*
            _SpaceFit.SetActive(true);
            _SpaceFit.transform.localScale = new Vector3(0.091f, 0.091f, 0.10f);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 500)
            {
                _SpaceFit.transform.localScale = Vector3.MoveTowards(_SpaceFit.transform.localScale, new Vector3(0.112f, 0.113f, 0.126f), t);
                yield return null;

                if (_SpaceFit.transform.localScale == new Vector3(0.112f, 0.113f, 0.126f))
                    break;
            }
            yield return new WaitForSeconds(1f);

            _SpaceFit.SetActive(false);
            _SpaceFit_Extended.SetActive(true);

            yield return new WaitForSeconds(2f);

            _SpaceFit_Extended.SetActive(false);
            // _LargeSpace_Callout.SetActive(false);

            // _StockUp_Callout.SetActive(true);

            yield return new WaitForSeconds(0.5f);
            _Stock_1.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _Stock_2.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _Stock_3.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _Stock_4.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _Stock_5.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _Stock_6.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _Stock_7.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _Stock_8.SetActive(true);
            yield return new WaitForSeconds(1.5f);

            yield return new WaitForSeconds(4);
            */

            OnDoorOpenCloseClicked();
            yield return new WaitForSeconds(0.6f);
            // _SpaceMaxTechnology_Container.SetActive(false);
            _SpaceMax_LeftColumn.SetActive(false);
            _SpaceMax_RightColumn.SetActive(false);

            _StockUp_Callout.SetActive(false);

        }
        else
        {
            // _SpaceMaxTechnology_Text_Portrait.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 500)
            {
                _Virtual_Camera.transform.localPosition = Vector3.MoveTowards(_Virtual_Camera.transform.localPosition, new Vector3(0f, -0.18f - 0.18f, -3.15f), t);
                yield return null;

                if (_Virtual_Camera.transform.localPosition == new Vector3(0f, -0.18f -0.18f, -3.15f))
                    break;
            }

            _LargeSpace_Callout_Portrait.SetActive(true);

            OnDoorOpenCloseClicked();

            /*
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _LargeSpace_Callout_Portrait.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }
            */
            yield return new WaitForSeconds(0.1f);
            //_SpaceMaxTechnology_Container.SetActive(true);
            _SpaceMax_LeftColumn.SetActive(true);
            _SpaceMax_RightColumn.SetActive(true);

            // _LargeSpace_Callout.SetActive(true);
            yield return new WaitForSeconds(0.9f);

            animatorSpaceMax_Left.Play("SpaceMax_Left_Animation");
            animatorSpaceMax_Right.Play("SpaceMax_Right_Animation");

            yield return new WaitForSeconds(0.833f);
            animatorSpaceMax_Left.Play("Still");
            animatorSpaceMax_Right.Play("Still");
            yield return new WaitForSeconds(0.2f);

            animatorSpaceMax_Left.Play("SpaceMax_Left_Animation");
            animatorSpaceMax_Right.Play("SpaceMax_Right_Animation");

            yield return new WaitForSeconds(0.833f);
            animatorSpaceMax_Left.Play("Still");
            animatorSpaceMax_Right.Play("Still");
            yield return new WaitForSeconds(0.2f);

            animatorSpaceMax_Left.Play("SpaceMax_Left_Animation");
            animatorSpaceMax_Right.Play("SpaceMax_Right_Animation");

            yield return new WaitForSeconds(5.5f);

            /*
            _SpaceFit.SetActive(true);
            _SpaceFit.transform.localScale = new Vector3(0.09f, 0.092f, 0.10f);

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 500)
            {
                _SpaceFit.transform.localScale = Vector3.MoveTowards(_SpaceFit.transform.localScale, new Vector3(0.112f, 0.113f, 0.126f), t);
                yield return null;

                if (_SpaceFit.transform.localScale == new Vector3(0.112f, 0.113f, 0.126f))
                    break;
            }
            yield return new WaitForSeconds(1f);

            _SpaceFit.SetActive(false);
            _SpaceFit_Extended.SetActive(true);
            _SpaceMaxTechnology_Text_Portrait_AddOn.SetActive(true);

            yield return new WaitForSeconds(2f);

            _SpaceFit_Extended.SetActive(false);
            _LargeSpace_Callout_Portrait.SetActive(false);

            // _StockUp_Callout_Portrait.SetActive(true);
            
            
            // for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            // {
            //    Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
            //    _StockUp_Callout_Portrait.GetComponent<SpriteRenderer>().color = newColor;
            //    yield return null;
            // }
            
            yield return new WaitForSeconds(0.5f);
            _Stock_1.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _Stock_2.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _Stock_3.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _Stock_4.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _Stock_5.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _Stock_6.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _Stock_7.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _Stock_8.SetActive(true);
            yield return new WaitForSeconds(1.5f);

            yield return new WaitForSeconds(4);
            */

            OnDoorOpenCloseClicked();
            yield return new WaitForSeconds(0.6f);
            // _SpaceMaxTechnology_Container.SetActive(false);
            _SpaceMax_LeftColumn.SetActive(false);
            _SpaceMax_RightColumn.SetActive(false);

            _StockUp_Callout_Portrait.SetActive(false);
        }

        _FeaturePlay.SetActive(false);
    }

    public void OnSpaceMaxTechnologyClicked()
    {
        ResetActions();
        ResetDimensionAction();

        _FoodManagement_Icon.SetActive(false);
        _FoodManagement_Hide_Icon.SetActive(true);
        _FamilyConnection_Icon.SetActive(false);
        _FamilyConnection_Hide_Icon.SetActive(true);
        _HomeConnection_Icon.SetActive(false);
        _HomeConnection_Hide_Icon.SetActive(true);
        _SpaceMaxTechnology_Icon.SetActive(true);
        _SpaceMaxTechnology_Hide_Icon.SetActive(false);

        _FoodManagement_Icon_Portrait.SetActive(false);
        _FoodManagement_Hide_Icon_Portrait.SetActive(true);
        _FamilyConnection_Icon_Portrait.SetActive(false);
        _FamilyConnection_Hide_Icon_Portrait.SetActive(true);
        _HomeConnection_Icon_Portrait.SetActive(false);
        _HomeConnection_Hide_Icon_Portrait.SetActive(true);
        _SpaceMaxTechnology_Icon_Portrait.SetActive(true);
        _SpaceMaxTechnology_Hide_Icon_Portrait.SetActive(false);


        StartCoroutine(SpaceMaxTechnologyTransition());
    }



    [SerializeField]
    GameObject _ExploreBixby_Icon, _ExploreBixby_Icon_Hide, _MealPlanner_Icon, _MealPlanner_Icon_Hide, _SmartView_Icon, _SmartView_Icon_Hide, _HomeEntertainment_Icon, _HomeEntertainment_Icon_Hide, _CloseHomeConnectionIcon;

    [SerializeField]
    GameObject _ExploreBixby_Icon_Portrait, _ExploreBixby_Icon_Hide_Portrait, _MealPlanner_Icon_Portrait, _MealPlanner_Icon_Hide_Portrait, _SmartView_Icon_Portrait, _SmartView_Icon_Hide_Portrait, _HomeEntertainment_Icon_Portrait, _HomeEntertainment_Icon_Hide_Portrait, _CloseHomeConnectionIcon_Portrait, _CloseHomeControlButton_Panel_Portrait;

    [SerializeField]
    GameObject _HomeScreen, _ExploreBixby_Text_Portrait, _ExploreBixby_Label;

    public IEnumerator ExploreBixbyTransition()
    {
        _DefaultFridgeScreen.SetActive(false);

        _HomeScreen.SetActive(true);

        if (Screen.width > Screen.height || VirtualTextField)
        {
            _ExploreBixby_Label.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _ExploreBixby_Label.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                // _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(0.34f, 0.3f, -1.72f), t);
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(0.14f, 0.28f, -2.05f), t);
                yield return null;

                if (_Virtual_Camera.transform.position == new Vector3(0.14f, 0.28f, -2.05f))
                    break;
            }

            yield return new WaitForSeconds(1.5f);

            /*
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _HomeControl_Label.GetComponent<SpriteRenderer>().color = newColor;
                _HomeControl_Text.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }
            */

            // HomeControl_ZoomIn = true;
            // yield return new WaitUntil(() => HomeControl_ZoomIn == false);

            // yield return new WaitForSeconds(0.5f);

            _HomeScreen.SetActive(false);
            _BixbyScreen.SetActive(true);
            yield return new WaitForSeconds(1.5f);


            _BixbyConnection.SetActive(true);
            animator_BixbyConnection.Play("BixbyConnection_Animation");
            yield return new WaitForSeconds(2.15f);
            _BixbyScreen.SetActive(false);
            _BeforeNote.SetActive(true);
            yield return new WaitForSeconds(1);
            _BeforeNote.SetActive(false);
            _AfterNote.SetActive(true);
            yield return new WaitForSeconds(4f);

            // HomeControl_ZoomOut = true;

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(0.14f, 0.28f, -2.69f), t);
                yield return null;

                if (_Virtual_Camera.transform.position == new Vector3(0.14f, 0.28f, -2.69f))
                    break;
            }

            _FeaturePlay.SetActive(false);
        }
        else
        {
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(0.55f, 0.2f, -2.31f), t);
                yield return null;

                if (_Virtual_Camera.transform.position == new Vector3(0.55f, 0.2f, -2.31f))
                    break;
            }

            yield return new WaitForSeconds(1.5f);

            /*
            if (Screen.width < Screen.height)
            {
                currentHomeConnection = true;

            }
            */
            _HomeScreen.SetActive(false);
            _BixbyScreen.SetActive(true);
            yield return new WaitForSeconds(1.5f);


            _BixbyConnection.SetActive(true);
            _ExploreBixby_Text_Portrait.SetActive(true);
            animator_BixbyConnection.Play("BixbyConnection_Animation");
            yield return new WaitForSeconds(2.15f);
            _BixbyScreen.SetActive(false);
            _BeforeNote.SetActive(true);
            yield return new WaitForSeconds(1);
            _BeforeNote.SetActive(false);
            _AfterNote.SetActive(true);
            yield return new WaitForSeconds(4f);

            // HomeControl_ZoomOut = true;

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, initialPosition, t);
                yield return null;

                if (_Virtual_Camera.transform.position == initialPosition)
                    break;
            }
        }

        
    }

    public void ExploreBixbyClicked()
    {
        ResetActions();
        ResetDimensionAction();

        _ExploreBixby_Icon.SetActive(true);
        _ExploreBixby_Icon_Hide.SetActive(false);
        _MealPlanner_Icon.SetActive(false);
        _MealPlanner_Icon_Hide.SetActive(true);
        _SmartView_Icon.SetActive(false);
        _SmartView_Icon_Hide.SetActive(true);
        _HomeEntertainment_Icon.SetActive(false);
        _HomeEntertainment_Icon_Hide.SetActive(true);
        _CloseHomeConnectionIcon.SetActive(true);

        _ExploreBixby_Icon_Portrait.SetActive(true);
        _ExploreBixby_Icon_Hide_Portrait.SetActive(false);
        _MealPlanner_Icon_Portrait.SetActive(false);
        _MealPlanner_Icon_Hide_Portrait.SetActive(true);
        _SmartView_Icon_Portrait.SetActive(false);
        _SmartView_Icon_Hide_Portrait.SetActive(true);
        _HomeEntertainment_Icon_Portrait.SetActive(false);
        _HomeEntertainment_Icon_Hide_Portrait.SetActive(true);
        _CloseHomeConnectionIcon_Portrait.SetActive(true);

        StartCoroutine(ExploreBixbyTransition());
    }

    [SerializeField]
    GameObject _MealPlannerScreen, _MealPlanner_Text, _MealPlanner_Text_Portrait, _MealPlanner_Food, _FoodPlanner_FridgeScreen;
    Animator animatorMealMaker, animatorFoodPlanner_Fridge;

    public IEnumerator MealPlannerTransition()
    {

        // _MealPlannerScreen.SetActive(true);

        if (Screen.width > Screen.height || VirtualTextField)
        {
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(0f, 0.33f, -1.82f), t);
                yield return null;

                if (_Virtual_Camera.transform.position == new Vector3(0f, 0.33f, -1.82f))
                    break;
            }

            yield return new WaitForSeconds(1f);

            _DefaultFridgeScreen.SetActive(false);
            _FoodPlanner_FridgeScreen.SetActive(true);
            animatorFoodPlanner_Fridge.Play("FoodPlanner_Fridge_Animation");

            /*
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _HomeControl_Label.GetComponent<SpriteRenderer>().color = newColor;
                _HomeControl_Text.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }
            */

            // HomeControl_ZoomIn = true;
            // yield return new WaitUntil(() => HomeControl_ZoomIn == false);

            // yield return new WaitForSeconds(0.5f);

            yield return new WaitForSeconds(6f);

            _MealPlanner_Food.SetActive(true);
            animatorMealMaker.Play("MealPlanner_Animation");
            yield return new WaitForSeconds(4f);

            _MealPlanner_Text.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _MealPlanner_Text.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            yield return new WaitForSeconds(2f);
            // HomeControl_ZoomOut = true;

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, initialPosition, t);
                yield return null;

                if (_Virtual_Camera.transform.position == initialPosition)
                    break;
            }

            _FeaturePlay.SetActive(false);
        }
        else
        {
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(0.18f, 0.31f, -1.62f), t);
                yield return null;

                if (_Virtual_Camera.transform.position == new Vector3(0.18f, 0.31f, -1.62f))
                    break;
            }

            yield return new WaitForSeconds(1f);

            // _MealPlanner_Food.SetActive(true);
            // animatorMealMaker.Play("MealPlanner_Animation");
            // yield return new WaitForSeconds(4f);

            _MealPlanner_Text_Portrait.SetActive(true);

            _DefaultFridgeScreen.SetActive(false);
            _FoodPlanner_FridgeScreen.SetActive(true);
            animatorFoodPlanner_Fridge.Play("FoodPlanner_Fridge_Animation");

            /*
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _MealPlanner_Text_Portrait.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }
            */
            yield return new WaitForSeconds(7f);
            // HomeControl_ZoomOut = true;

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, initialPosition, t);
                yield return null;

                if (_Virtual_Camera.transform.position == initialPosition)
                    break;
            }
        }

        yield return null;

        
    }

    public void MealPlannerClicked()
    {
        ResetActions();
        ResetDimensionAction();

        _ExploreBixby_Icon.SetActive(false);
        _ExploreBixby_Icon_Hide.SetActive(true);
        _MealPlanner_Icon.SetActive(true);
        _MealPlanner_Icon_Hide.SetActive(false);
        _SmartView_Icon.SetActive(false);
        _SmartView_Icon_Hide.SetActive(true);
        _HomeEntertainment_Icon.SetActive(false);
        _HomeEntertainment_Icon_Hide.SetActive(true);
        _CloseHomeConnectionIcon.SetActive(true);

        _ExploreBixby_Icon_Portrait.SetActive(false);
        _ExploreBixby_Icon_Hide_Portrait.SetActive(true);
        _MealPlanner_Icon_Portrait.SetActive(true);
        _MealPlanner_Icon_Hide_Portrait.SetActive(false);
        _SmartView_Icon_Portrait.SetActive(false);
        _SmartView_Icon_Hide_Portrait.SetActive(true);
        _HomeEntertainment_Icon_Portrait.SetActive(false);
        _HomeEntertainment_Icon_Hide_Portrait.SetActive(true);
        _CloseHomeConnectionIcon_Portrait.SetActive(true);

        StartCoroutine(MealPlannerTransition());
    }

    [SerializeField]
    GameObject _SmartViewScreen, _SmartView_Text, _SmartView_Device, _SmartView_Text_Portrait, _SmartView_ScreenChange_Fridge;
    Animator animatorSmartView, animatorSmartView_FridgeScreen;
    
    public IEnumerator SmartViewTransition()
    {
        if (Screen.width > Screen.height || VirtualTextField)
        {
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(0.39f, 0.24f, -2.16f), t);
                yield return null;

                if (_Virtual_Camera.transform.position == new Vector3(0.39f, 0.24f, -2.16f))
                    break;
            }

            yield return new WaitForSeconds(1f);

            /*
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _HomeControl_Label.GetComponent<SpriteRenderer>().color = newColor;
                _HomeControl_Text.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }
            */

            // HomeControl_ZoomIn = true;
            // yield return new WaitUntil(() => HomeControl_ZoomIn == false);

            // yield return new WaitForSeconds(0.5f);



            _SmartView_Device.SetActive(true);
            animatorSmartView.Play("SmartView_Animation");
            yield return new WaitForSeconds(4f);

            Connecting.SetActive(true);
            Connecting.transform.localPosition = new Vector3(0.33f, 0.38f, -0.3f);
            animatorConnecting.Play("Connecting_Animation");

            yield return new WaitForSeconds(2.3f);
            _DefaultFridgeScreen.SetActive(false);
            _SmartView_ScreenChange_Fridge.SetActive(true);

            yield return new WaitForSeconds(1.5f);

            _SmartView_Text.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _SmartView_Text.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            yield return new WaitForSeconds(0.9f);
            _SmartView_ScreenChange_Fridge.SetActive(false);
            _SmartViewScreen.SetActive(true);
            animatorSmartView_FridgeScreen.Play("SmartView_FridgeScreen_Animation");
            // _SmartView_ScreenChange_Fridge.SetActive(true);
            // _SmartViewScreen.SetActive(false);

            yield return new WaitForSeconds(5f);
            // HomeControl_ZoomOut = true;

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, initialPosition, t);
                yield return null;

                if (_Virtual_Camera.transform.position == initialPosition)
                    break;
            }

            _FeaturePlay.SetActive(false);
        }
        else
        {
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(0.44f, -0.15f, -2.81f), t);
                yield return null;

                if (_Virtual_Camera.transform.position == new Vector3(0.44f, -0.15f, -2.81f))
                    break;
            }

            yield return new WaitForSeconds(1f);

            _SmartView_Device.SetActive(true);
            animatorSmartView.Play("SmartView_Animation");
            yield return new WaitForSeconds(4f);

            Connecting.SetActive(true);
            Connecting.transform.localPosition = new Vector3(0.33f, 0.38f, -0.306f);
            animatorConnecting.Play("Connecting_Animation");

            yield return new WaitForSeconds(2.3f);
            _DefaultFridgeScreen.SetActive(false);
            _SmartView_ScreenChange_Fridge.SetActive(true);

            yield return new WaitForSeconds(1.5f);

            _SmartView_Text_Portrait.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _SmartView_Text.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            yield return new WaitForSeconds(0.9f);
            _SmartView_ScreenChange_Fridge.SetActive(false);
            _SmartViewScreen.SetActive(true);
            animatorSmartView_FridgeScreen.Play("SmartView_FridgeScreen_Animation");
            // _SmartView_ScreenChange_Fridge.SetActive(true);
            // _SmartViewScreen.SetActive(false);

            yield return new WaitForSeconds(5f);
            // HomeControl_ZoomOut = true;

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, initialPosition, t);
                yield return null;

                if (_Virtual_Camera.transform.position == initialPosition)
                    break;
            }
        }
        yield return null;

    }


    public void SmartViewClicked()
    {
        ResetActions();
        ResetDimensionAction();

        _ExploreBixby_Icon.SetActive(false);
        _ExploreBixby_Icon_Hide.SetActive(true);
        _MealPlanner_Icon.SetActive(false);
        _MealPlanner_Icon_Hide.SetActive(true);
        _SmartView_Icon.SetActive(true);
        _SmartView_Icon_Hide.SetActive(false);
        _HomeEntertainment_Icon.SetActive(false);
        _HomeEntertainment_Icon_Hide.SetActive(true);
        _CloseHomeConnectionIcon.SetActive(true);

        _ExploreBixby_Icon_Portrait.SetActive(false);
        _ExploreBixby_Icon_Hide_Portrait.SetActive(true);
        _MealPlanner_Icon_Portrait.SetActive(false);
        _MealPlanner_Icon_Hide_Portrait.SetActive(true);
        _SmartView_Icon_Portrait.SetActive(true);
        _SmartView_Icon_Hide_Portrait.SetActive(false);
        _HomeEntertainment_Icon_Portrait.SetActive(false);
        _HomeEntertainment_Icon_Hide_Portrait.SetActive(true);
        _CloseHomeConnectionIcon_Portrait.SetActive(true);

        StartCoroutine(SmartViewTransition());
    }


    [SerializeField]
    GameObject _HomeEntertainmentScreen, _HomeEntertainment_Text, _HomeEntertainment_Text_Portrait, _HomeEntertainment_Device;
    Animator animatorHomeEntertainment;

    public IEnumerator HomeEntertainmentTransition()
    {
        if (Screen.width > Screen.height || VirtualTextField)
        {
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(0.39f, 0.24f, -2.16f), t);
                yield return null;

                if (_Virtual_Camera.transform.position == new Vector3(0.39f, 0.24f, -2.16f))
                    break;
            }

            yield return new WaitForSeconds(1f);

            /*
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _HomeControl_Label.GetComponent<SpriteRenderer>().color = newColor;
                _HomeControl_Text.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }
            */

            // HomeControl_ZoomIn = true;
            // yield return new WaitUntil(() => HomeControl_ZoomIn == false);

            // yield return new WaitForSeconds(0.5f);



            _HomeEntertainment_Device.SetActive(true);
            animatorHomeEntertainment.Play("HomeEntertainment_Animation");
            yield return new WaitForSeconds(3.5f);

            Connecting.SetActive(true);
            Connecting.transform.localPosition = new Vector3(0.33f, 0.38f, -0.3f);
            animatorConnecting.Play("Connecting_Animation");

            yield return new WaitForSeconds(5f);
            _DefaultFridgeScreen.SetActive(false);
            _HomeEntertainmentScreen.SetActive(true);

            yield return new WaitForSeconds(1f);

            _HomeEntertainment_Text.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _HomeEntertainment_Text.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            yield return new WaitForSeconds(3f);
            // HomeControl_ZoomOut = true;

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, initialPosition, t);
                yield return null;

                if (_Virtual_Camera.transform.position == initialPosition)
                    break;
            }

            _FeaturePlay.SetActive(false);
        }
        else
        {
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, new Vector3(0.44f, -0.15f, -2.81f), t);
                yield return null;

                if (_Virtual_Camera.transform.position == new Vector3(0.44f, -0.15f, -2.81f))
                    break;
            }

            // yield return new WaitForSeconds(1f);

            _HomeEntertainment_Device.SetActive(true);
            animatorHomeEntertainment.Play("HomeEntertainment_Animation");
            yield return new WaitForSeconds(3.5f);

            Connecting.SetActive(true);
            Connecting.transform.localPosition = new Vector3(0.33f, 0.38f, -0.306f);
            animatorConnecting.Play("Connecting_Animation");

            yield return new WaitForSeconds(5f);
            _DefaultFridgeScreen.SetActive(false);
            _HomeEntertainmentScreen.SetActive(true);

            yield return new WaitForSeconds(1f);

            _HomeEntertainment_Text_Portrait.SetActive(true);
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
                _HomeEntertainment_Text.GetComponent<SpriteRenderer>().color = newColor;
                yield return null;
            }

            yield return new WaitForSeconds(3f);
            // HomeControl_ZoomOut = true;

            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 100)
            {
                _Virtual_Camera.transform.position = Vector3.MoveTowards(_Virtual_Camera.transform.position, initialPosition, t);
                yield return null;

                if (_Virtual_Camera.transform.position == initialPosition)
                    break;
            }
        }
        yield return null;

    }

    public void HomeEntertainmentClicked()
    {
        ResetActions();
        ResetDimensionAction();

        _ExploreBixby_Icon.SetActive(false);
        _ExploreBixby_Icon_Hide.SetActive(true);
        _MealPlanner_Icon.SetActive(false);
        _MealPlanner_Icon_Hide.SetActive(true);
        _SmartView_Icon.SetActive(false);
        _SmartView_Icon_Hide.SetActive(true);
        _HomeEntertainment_Icon.SetActive(true);
        _HomeEntertainment_Icon_Hide.SetActive(false);

        _ExploreBixby_Icon_Portrait.SetActive(false);
        _ExploreBixby_Icon_Hide_Portrait.SetActive(true);
        _MealPlanner_Icon_Portrait.SetActive(false);
        _MealPlanner_Icon_Hide_Portrait.SetActive(true);
        _SmartView_Icon_Portrait.SetActive(false);
        _SmartView_Icon_Hide_Portrait.SetActive(true);
        _HomeEntertainment_Icon_Portrait.SetActive(true);
        _HomeEntertainment_Icon_Hide_Portrait.SetActive(false);

        _CloseHomeConnectionIcon.SetActive(true);
        StartCoroutine(HomeEntertainmentTransition());
    }


    public void CloseHomeConnectionClicked()
    {
        ResetActions();

        _ExploreBixby_Icon.SetActive(false);
        _ExploreBixby_Icon_Hide.SetActive(false);
        _MealPlanner_Icon.SetActive(false);
        _MealPlanner_Icon_Hide.SetActive(false);
        _SmartView_Icon.SetActive(false);
        _SmartView_Icon_Hide.SetActive(false);
        _HomeEntertainment_Icon.SetActive(false);
        _HomeEntertainment_Icon_Hide.SetActive(false);

        _FoodManagement_Icon.SetActive(true);
        _FoodManagement_Hide_Icon.SetActive(false);
        _FamilyConnection_Icon.SetActive(true);
        _FamilyConnection_Hide_Icon.SetActive(false);
        _HomeConnection_Icon.SetActive(true);
        _HomeConnection_Hide_Icon.SetActive(false);
        _SpaceMaxTechnology_Icon.SetActive(true);
        _SpaceMaxTechnology_Hide_Icon.SetActive(false);


        _ExploreBixby_Icon_Portrait.SetActive(false);
        _ExploreBixby_Icon_Hide_Portrait.SetActive(false);
        _MealPlanner_Icon_Portrait.SetActive(false);
        _MealPlanner_Icon_Hide_Portrait.SetActive(false);
        _SmartView_Icon_Portrait.SetActive(false);
        _SmartView_Icon_Hide_Portrait.SetActive(false);
        _HomeEntertainment_Icon_Portrait.SetActive(false);
        _HomeEntertainment_Icon_Hide_Portrait.SetActive(false);

        _FoodManagement_Icon_Portrait.SetActive(true);
        _FoodManagement_Hide_Icon_Portrait.SetActive(false);
        _FamilyConnection_Icon_Portrait.SetActive(true);
        _FamilyConnection_Hide_Icon_Portrait.SetActive(false);
        _HomeConnection_Icon_Portrait.SetActive(true);
        _HomeConnection_Hide_Icon_Portrait.SetActive(false);
        _SpaceMaxTechnology_Icon_Portrait.SetActive(true);
        _SpaceMaxTechnology_Hide_Icon_Portrait.SetActive(false);

        _CloseHomeConnectionIcon.SetActive(false);
        _CloseHomeConnectionIcon_Portrait.SetActive(false);

    }

    [SerializeField]
    GameObject Arrow_L, Arrow_B, Arrow_H;
    
    bool dimensionClick = false;
    public void OnDimensionClicked()
    {
        dimensionClick = !dimensionClick;

        if (dimensionClick == true)
        {
            ResetActions();

            // Arrow_B.SetActive(true);
            // Arrow_H.SetActive(true);
            // Arrow_L.SetActive(true);
        }
        else
        {
            // Arrow_B.SetActive(false);
            // Arrow_H.SetActive(false);
            // Arrow_L.SetActive(false);
        }

    }


    [SerializeField]
    GameObject InfoButton_Landscape, InfoButton_Portrait, InfoPanel_Landscape, InfoPanel_Portrait;
    bool clickInfo = false;

    public IEnumerator InfoButtonTransition()
    {
        clickInfo = !clickInfo;

        if(clickInfo == true)
        {
            if (Screen.width > Screen.height || VirtualTextField)
            {
                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
                {
                    InfoButton_Landscape.GetComponent<RectTransform>().localPosition = Vector3.Lerp(InfoButton_Landscape.GetComponent<RectTransform>().localPosition, new Vector3(-289.36f, 0f, 0f), t); //-164.75f
                    yield return null;

                    if ( Mathf.Abs(InfoButton_Landscape.GetComponent<RectTransform>().localPosition.x + 289.36f) < 1f )
                    {
                        InfoButton_Landscape.GetComponent<RectTransform>().localPosition = new Vector3(-289.36f, 0f, 0f);
                        break;
                    }
                       
                }
            }
            else
            {
                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
                {
                    InfoButton_Portrait.GetComponent<RectTransform>().localPosition = Vector3.Lerp(InfoButton_Portrait.GetComponent<RectTransform>().localPosition, new Vector3(-312.21f, 0f, 0f), t); 
                    yield return null;

                    if (Mathf.Abs(InfoButton_Portrait.GetComponent<RectTransform>().localPosition.x + 312.21f) < 1f)
                    {
                        InfoButton_Portrait.GetComponent<RectTransform>().localPosition = new Vector3(-312.21f, 0f, 0f);
                        break;
                    }

                }
            }
        }
        else
        {
            if (Screen.width > Screen.height || VirtualTextField)
            {
                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
                {
                    InfoButton_Landscape.GetComponent<RectTransform>().localPosition = Vector3.Lerp(InfoButton_Landscape.GetComponent<RectTransform>().localPosition, new Vector3(0f, 0f, 0f), t); 
                    yield return null;

                    if (Mathf.Abs(InfoButton_Landscape.GetComponent<RectTransform>().localPosition.x) < 1f)
                    {
                        InfoButton_Landscape.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
                        break;
                    }

                }
            }
            else
            {
                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
                {
                    InfoButton_Portrait.GetComponent<RectTransform>().localPosition = Vector3.Lerp(InfoButton_Portrait.GetComponent<RectTransform>().localPosition, new Vector3(0f, 0f, 0f), t); 
                    yield return null;

                    if (Mathf.Abs(InfoButton_Portrait.GetComponent<RectTransform>().localPosition.x) < 1f)
                    {
                        InfoButton_Portrait.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
                        break;
                    }

                }
            }
        }
        
    }


    public void InfoButtonClicked()
    {
        StartCoroutine(InfoButtonTransition());
    }

    public void BuyNowButtonClicked()
    {
        Application.OpenURL("https://www.samsung.com/in/microsite/side-by-side-refrigerators/");
    }
}