using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

    public GameObject[] itemStockUI;
    public FoxCollectionLog[] foxesInStock;


    public string[] foxNames;
    public GameObject purchasePanel;

    public FoxCollection foxCollection;
    public int refreshCost = 50;

    public float refreshTimerStart;
    public float refreshTimer;

    public Slider sliderRefresh;

	// Use this for initialization
	void Start () {
        foxesInStock = new FoxCollectionLog[3];
        foxNames = new string[3];

        foxCollection = FindObjectOfType<FoxCollection>();
        GetNewStock();
        refreshTimer = refreshTimerStart;
        sliderRefresh.maxValue = refreshTimerStart;
        //todo have a timer for refreshing the stock
    }

    // Update is called once per frame
    void Update () {
		 
        if (refreshTimer <= 0)
        {
            RefreshItemStock();
            refreshTimer = refreshTimerStart;
        }
        refreshTimer -= Time.deltaTime;
        sliderRefresh.value = refreshTimer;
	}

    public void PurchaseRefresh()
    {
        //todo check if you have enough money to refresh
        RefreshItemStock();
        refreshTimer = refreshTimerStart;
        GameController.instance.SubtractPoints(refreshCost);
    }

    private void RefreshItemStock()
    {
        //reset the timer
        GetNewStock();
    }


    //resets all of the items in the shop
    private void GetNewStock()
    {
        for (int i = 0; i < itemStockUI.Length; i++)
        {
            ResetItemStock(i);
        }


    }

    //updates the specific item at a specific index based on a database
    public void ResetItemStock(int whichIndex)
    {

        //get a new fox based off the data base
        print("Resetting fox stock on index: " + whichIndex.ToString());
        int whichFox = Random.Range(0, foxCollection.foxDatabase.Count);
        FoxCollectionLog foxToAdd = foxCollection.foxDatabase[whichFox];
        print("Getting fox of type:" + foxToAdd.foxType);
        string foxName = NameGenerator.instance.GetName();
        foxesInStock[whichIndex] = foxToAdd;
        foxNames[whichIndex] = foxName;

        //itemStockUI[whichIndex].GetComponent<ui_foxPurchaseCard>().InitPurchaseCard(foxToAdd, foxName);
        //get a new name
        //assign values to that index of the UI object
        

    }

    //updates the current stock based on what's avlaiable. 


    public void PurchaseItem(int whichIndex)
    {
        //check that you have enough money to purchase

        //create a fox based on the data attached to the item stock
        FoxCollectionLog foxData = itemStockUI[whichIndex].GetComponent<ui_foxPurchaseCard>().foxData;
        string foxName = itemStockUI[whichIndex].GetComponent<ui_foxPurchaseCard>().foxName;
        string foxType = foxData.foxType;

        GameController.instance.AddFoxFromShop(foxType, foxName);
        //reset the item stock
        ResetItemStock(whichIndex);
        //closes the shop manager
        purchasePanel.SetActive(false);
        //creates a new pop up 
        UIManager.instance.CreateFloatingUpdate("You've adopted " + foxName + "!");
    }

    public void OpenShopPanel()
    {
        //update displays for each one of the UI elements
        for (int i = 0; i < itemStockUI.Length; i++)
        {
            FoxCollectionLog foxData = foxesInStock[i];
            string foxName = foxNames[i];
            itemStockUI[i].GetComponent<ui_foxPurchaseCard>().InitPurchaseCard(foxData, foxName);
        }
        purchasePanel.SetActive(true);

    }

    public void CloseShopPanel()
    {
        purchasePanel.SetActive(false);
    }
}
