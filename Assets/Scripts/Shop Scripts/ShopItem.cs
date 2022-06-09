using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    ShopManager shopManager;
    [SerializeField]
    private TMP_Text costTxt;
    int i;
    // Start is called before the first frame update
    void Start()
    {
        shopManager = FindObjectOfType<ShopManager>();
        i = Random.Range(0, shopManager.shopItemImgList.Length);
        GetComponent<Image>().sprite = shopManager.shopItemImgList[i].img;
        costTxt.text = shopManager.shopItemImgList[i].cost.ToString();
    }

    public void ItemBuy()
    {
        shopManager.ItemBuy(i);
    }
}
