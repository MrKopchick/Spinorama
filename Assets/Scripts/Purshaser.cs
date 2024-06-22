using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using DG.Tweening;

public class Purshaser : MonoBehaviour
{
    [SerializeField] private GameObject chelHaroshPanel;
    [SerializeField] private RectTransform RemoveAdsPanelRect, NicePanelRect;
    public void OnPurchaceCompleted(Product product)
    {
        switch (product.definition.id)
        {
            case "com.pixeldreams.removeads":
                RemoveAds();
                break;
            case "com.pixeldreams.add500coins":
                Add500();
                break;
            case "com.pixeldreams.add1000coins":
                Add1000();
                break;
        }
    }

    private void RemoveAds()
    {
        PlayerPrefs.SetInt("RemoveAds", 1);
        Debug.Log("Purchase: Remove ads!");
        UIInfo.Instance.UpdateRemoveAds();
        if (chelHaroshPanel != null && RemoveAdsPanelRect != null && NicePanelRect != null)
        {
            chelHaroshPanel.SetActive(true);
            RemoveAdsPanelRect.DOAnchorPosY(5306, 0.4f);
            NicePanelRect.DOAnchorPosY(0, 0.4f);
        }
    }
    private void Add500()
    {
        bank bank;
        bank = GameObject.Find("Bank").GetComponent<bank>();
        bank.AddCoin(500);
    }
    private void Add1000()
    {
        bank bank;
        bank = GameObject.Find("Bank").GetComponent<bank>();
        bank.AddCoin(1000);
    }
}
