using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//contains a reference to the fox data. Showcases the fox's core info
//note, generally used as a child under the top UI system 

public class ui_FoxCard : MonoBehaviour {

    public string foxName;
    public Image foxImage;
    public Text txtFoxName;
    public Text foxType;
    public Text rarity;
    public FoxCollectionLog foxData;

    private ui_FoxCard foxCard;

    private ShopManager shopManager;

    // Use this for initialization
    void Start()
    {
        shopManager = FindObjectOfType<ShopManager>();
    }

    public void SetupFoxInfo(FoxCollectionLog _foxData, string _foxName)
    {
        //get a random name
        txtFoxName.text = _foxName;
        foxName = _foxName;
        foxType.text = _foxData.foxType;
        rarity.text = _foxData.rarity.ToString();
        foxImage.sprite = _foxData.foxSprite;
        foxData = _foxData;
    }

}
