using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class InAppPurchase : MonoBehaviour
{

    private string removeAds = "com.hansdacorp.stack.removeads";

    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == removeAds)
        {
            PlayerPrefs.SetInt("paid", 1);
            SceneManager.LoadScene(1);

        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        string author1 = "DuplicateTransaction";

        if (String.Equals(failureReason, author1))
        {
            PlayerPrefs.SetInt("paid", 1);
            SceneManager.LoadScene(1);

        }
        else
        {

        }
    }
}
