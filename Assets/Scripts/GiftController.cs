using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MusicPlayer;
using UnityEngine.EventSystems;

public enum Stage
{
    SelectGift,
    PlaceGift,
    SignGift
}

public class GiftController : MonoBehaviour
{
    public List<GameObject> gifts = new List<GameObject>();
    List<MenuItemSelectionHelper> selectedItems = new List<MenuItemSelectionHelper>();
    public GameObject horizontalMenu;

    public float animationZOffset = 0.1f;
    public float animationYAngularSpeed = 20f;
    public float animationPosAccuracy = 0.1f;
    public float animationRotAccuracy = 5f;
    public float giftDistanceOffset = 0.5f;

    public HorizontalCylinderLayout horizontalLayout;
    public ObjectPlacer objectPlacer;

    public Stage stage;

    public GameObject signMenu;

    public delegate void Action();
    Action action;

    public PointerClickHelper selectedButton;
    public GameObject currentGift;

    public bool currentGiftSelected;
    public float sentYValue = 0.2f;

    int previousTouchCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        horizontalLayout.OnMenuItemsSpawned += new HorizontalCylinderLayout.OnEventRiser(OnMenuItemsSpawned);
        horizontalLayout.itemsToSpawn = gifts.Count;
        horizontalLayout.Initialize();
        stage = Stage.SelectGift;
        action = SelectGift;
        
    }

    // Update is called once per frame
    void Update()
    {
        action?.Invoke();
        if(Input.GetMouseButtonDown(0) || (Input.touchCount != previousTouchCount && Input.touchCount > 0))
        {
            previousTouchCount = Input.touchCount;
            selectedButton?.OnClick();
        }
    }


    public void GoBackToSelectionStage()
    {
        Destroy(currentGift);
        signMenu.SetActive(false);
        stage = Stage.SelectGift;
        action = SelectGift;
    }

    void SelectGift()
    {
        if(!horizontalMenu.activeInHierarchy)
        {
            horizontalMenu.SetActive(true);
            horizontalLayout.Initialize();
        }


        if (stage == Stage.SelectGift && (Input.touchCount > 0 || Input.GetMouseButtonDown(0)))
        {
            if (selectedItems.Count == 0) return;
            var itemToSelect = selectedItems[selectedItems.Count - 1];
            if (itemToSelect.selectionStatus)
            {
                OnSelect(itemToSelect);
                stage = Stage.PlaceGift;
                objectPlacer.ObjectToPlace = gifts[itemToSelect.giftIndex];
                action = PlaceGift;
            }
        }
    }

    void PlaceGift()
    {
        if (stage == Stage.PlaceGift && (Input.touchCount < 1 || Input.GetMouseButtonUp(0)))
        {
            if(objectPlacer.PlaceAndReport(out var position, out var instantiatedGift))
            {
                currentGift = instantiatedGift;
                action = SignGift;
                stage = Stage.SignGift;
                signMenu.SetActive(true);
                signMenu.transform.position = position;
                currentGiftSelected = false;
            }
            else
            {
                GoBackToSelectionStage();
            }
        }
    }

    void SignGift()
    {
        if(stage == Stage.SignGift && currentGiftSelected)
        {
            currentGift.transform.localPosition += Vector3.up * Time.deltaTime;
            if (stage == Stage.SignGift && currentGift.transform.position.y > signMenu.GetComponent<SetUpSignMenu>().sendButton.transform.position.y)
            {
                currentGift.SetActive(false);
                currentGiftSelected = false;
                signMenu.GetComponent<SetUpSignMenu>().SetSent();
                StartCoroutine(ReturningCoroutine());
                action = null;
                stage = Stage.SelectGift;
            }
        }
    }

    IEnumerator ReturningCoroutine()
    {
        yield return new WaitForSeconds(3);
        action = SelectGift;
        signMenu.SetActive(false);
        if (currentGift) Destroy(currentGift);
    }

    void OnMenuItemsSpawned()
    {
        var items = FindObjectsOfType<MenuItemSelectionHelper>();
        for(var i = 0; i < items.Length; i++)
        {
            items[i].giftIndex = i;
            var gift = Instantiate(gifts[i]);
            gift.transform.SetParent(items[i].Model.transform);
            gift.transform.localScale = new Vector3(1, 1, 1);
            gift.transform.localPosition = Vector3.zero;
            gift.transform.localRotation = Quaternion.identity;
        }
    }

    public void OnSelectionStart(MenuItemSelectionHelper itemSelectionHelper)
    {
        if (selectedItems.Contains(itemSelectionHelper)) return;
        selectedItems.Add(itemSelectionHelper);
        StartCoroutine(ItemSelectionCoroitune(itemSelectionHelper));
    }


    public void OnSelect(MenuItemSelectionHelper itemSelectionHelper)
    {
        StopAllCoroutines();
        selectedItems.Clear();
        horizontalMenu.SetActive(false);
    }

    public IEnumerator ItemSelectionCoroitune(MenuItemSelectionHelper itemSelectionHelper)
    {     
        var initialPos = itemSelectionHelper.Model.transform.localPosition;
        while (itemSelectionHelper.selectionStatus)
        {
            if (Mathf.Abs(itemSelectionHelper.Model.transform.localPosition.z - initialPos.z) < Mathf.Abs(animationZOffset))
            {
                itemSelectionHelper.Model.transform.localPosition += new Vector3(0, 0, animationZOffset * Time.deltaTime);
                print("Translating!");
            }
            print($"Position: {itemSelectionHelper.Model.transform.localPosition}");
            itemSelectionHelper.Model.transform.Rotate(itemSelectionHelper.transform.up, Time.deltaTime * animationYAngularSpeed, Space.Self);
            yield return new WaitForEndOfFrame();
        }
        bool initialTransform = false;
        var rotation = itemSelectionHelper.Model.transform.localRotation;
        while (!initialTransform)
        {
            initialTransform = true;
            if ((itemSelectionHelper.Model.transform.localPosition - initialPos).magnitude < animationPosAccuracy)
                itemSelectionHelper.Model.transform.localPosition = initialPos;
            else
            {
                itemSelectionHelper.Model.transform.localPosition = Vector3.Lerp(itemSelectionHelper.Model.transform.localPosition, initialPos, 0.5f);
                initialTransform = false;
            }
            if(rotation.eulerAngles.y > animationRotAccuracy && rotation.eulerAngles.y < 360 - animationRotAccuracy)
            {
                rotation = itemSelectionHelper.Model.transform.localRotation;
                itemSelectionHelper.Model.transform.Rotate(itemSelectionHelper.transform.up, Time.deltaTime * animationYAngularSpeed * 4, Space.Self);
                initialTransform = false;
            }
            else
            {
                rotation = Quaternion.identity;
            }
            yield return new WaitForEndOfFrame();
        }
        itemSelectionHelper.selectable = true;
        selectedItems.Remove(itemSelectionHelper);
    }
}
