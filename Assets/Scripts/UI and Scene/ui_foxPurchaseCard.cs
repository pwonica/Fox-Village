using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_foxPurchaseCard : MonoBehaviour {

    public string foxName;
    public int itemIndex;
    public Text cost;
    public Button btnPurchase;
    public FoxCollectionLog foxData;

    public ui_FoxCard foxCard;              //make sure this is always a child of the top

    private ShopManager shopManager;

	// Use this for initialization
	void Awake () {
        shopManager = FindObjectOfType<ShopManager>();
        foxCard = GetComponentInChildren<ui_FoxCard>();
        //get the cost from the info card
	}

    public void InitPurchaseCard(FoxCollectionLog _foxData, string _foxName)
    {
        foxCard.SetupFoxInfo(_foxData, _foxName);
        cost.text = _foxData.cost.ToString();
        //todo probably keep this in one place rather in multiple
        foxData = _foxData;
        foxName = _foxName;
    }

    // Update is called once per frame
    void Update () {
		
	}


    public void PurchaseItem()
    {
        shopManager.PurchaseItem(itemIndex);
    }

}
