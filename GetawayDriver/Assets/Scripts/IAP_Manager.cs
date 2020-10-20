using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAP_Manager : MonoBehaviour, IStoreListener
{
    public static IAP_Manager instance;

    private static IStoreController storeController;
    private static IExtensionProvider storeExtensionProvider;

    //Step 1: Create your product
    private string removeAds = "remove_ads";

    [Header("Coin Products - NonConsumable")]
    private string doubleCoins = "double_coins";

    [Header("Coin Products - Consumable")]
    private string coins7500 = "coins_7500";
    private string coins45000 = "coins_45000";
    private string coins90000 = "coins_90000";
    private string coins180k = "coins_180k";
    private string coins500k = "coins_500k";
    private string coins1m = "coins_1m";

    [Header("Heart Products - Consumable")]
    private string hearts25 = "hearts_25";
    private string hearts50 = "hearts_50";
    private string hearts110 = "hearts_110";
    private string hearts300 = "hearts_300";
    private string hearts650 = "hearts_650";


    void Awake()
    {
        if(instance != this && instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        InitializePurchasing();
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        //Step 2: Choose if your product is a consumable or non consumable

        builder.AddProduct(removeAds, ProductType.NonConsumable);
        builder.AddProduct(doubleCoins, ProductType.NonConsumable);


        builder.AddProduct(coins7500, ProductType.Consumable);
        builder.AddProduct(coins45000, ProductType.Consumable);
        builder.AddProduct(coins90000, ProductType.Consumable);
        builder.AddProduct(coins180k, ProductType.Consumable);
        builder.AddProduct(coins500k, ProductType.Consumable);
        builder.AddProduct(coins1m, ProductType.Consumable);

        builder.AddProduct(hearts25, ProductType.Consumable);
        builder.AddProduct(hearts50, ProductType.Consumable);
        builder.AddProduct(hearts110, ProductType.Consumable);
        builder.AddProduct(hearts300, ProductType.Consumable);
        builder.AddProduct(hearts650, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);

    }

    private bool IsInitialized()
    {
        return storeController != null && storeExtensionProvider != null;
    }

    //Step 3: Create Methods
    public void BuyDoubleCoins()
    {
        BuyProductID(doubleCoins);
    }

    public void BuyCoins7500()
    {
        BuyProductID(coins7500);
    }

    public void BuyCoins45000()
    {
        BuyProductID(coins45000);
    }

    public void BuyCoins90000()
    {
        BuyProductID(coins90000);
    }

    public void BuyCoins180k()
    {
        BuyProductID(coins180k);
    }

    public void BuyCoins500k()
    {
        BuyProductID(coins500k);
    }

    public void BuyCoins1m()
    {
        BuyProductID(coins1m);
    }

    public void BuyHearts25()
    {
        BuyProductID(hearts25);
    }

    public void BuyHearts50()
    {
        BuyProductID(hearts50);
    }

    public void BuyHearts110()
    {
        BuyProductID(hearts110);
    }

    public void BuyHearts300()
    {
        BuyProductID(hearts300);
    }

    public void BuyHearts650()
    {
        BuyProductID(hearts650);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (string.Equals(args.purchasedProduct.definition.id, doubleCoins, StringComparison.Ordinal))
        {
            Debug.Log("Player now has double coins");

            //set the coin value to 2
            GameManager.instance.player.SetCoinValue(2);

            //grey out the UI to indicate that it has been bought
            GameManager.instance.boughtDoubleCoins.SetActive(true);

            //remove the button so it cannot be bought again
            GameManager.instance.doubleCoinsButton.SetActive(false);

        }
        else if (string.Equals(args.purchasedProduct.definition.id, coins7500, StringComparison.Ordinal))
        {
            Debug.Log("Give player 7500 coins");

            GameManager.instance.player.SavePlayer(0, 7500, 0);

        }
        else if (string.Equals(args.purchasedProduct.definition.id, coins45000, StringComparison.Ordinal))
        {
            Debug.Log("Give player 45000 coins");

            GameManager.instance.player.SavePlayer(0, 45000, 0);

        }
        else if (string.Equals(args.purchasedProduct.definition.id, coins90000, StringComparison.Ordinal))
        {
            Debug.Log("Give player 90000 coins");

            GameManager.instance.player.SavePlayer(0, 90000, 0);

        }
        else if (string.Equals(args.purchasedProduct.definition.id, coins180k, StringComparison.Ordinal))
        {
            Debug.Log("Give player 180k coins");

            GameManager.instance.player.SavePlayer(0, 180000, 0);

        }
        else if (string.Equals(args.purchasedProduct.definition.id, coins500k, StringComparison.Ordinal))
        {
            Debug.Log("Give player 500k coins");

            GameManager.instance.player.SavePlayer(0, 500000, 0);

        }
        else if (string.Equals(args.purchasedProduct.definition.id, coins1m, StringComparison.Ordinal))
        {
            Debug.Log("Give player 1m coins");

            GameManager.instance.player.SavePlayer(0, 1200000, 0);

        }
        else if (string.Equals(args.purchasedProduct.definition.id, hearts25, StringComparison.Ordinal))
        {
            Debug.Log("Give player 25 hearts");

            GameManager.instance.player.SavePlayer(0, 0, 25);

        }
        else if (string.Equals(args.purchasedProduct.definition.id, hearts50, StringComparison.Ordinal))
        {
            Debug.Log("Give player 50 hearts");

            GameManager.instance.player.SavePlayer(0, 0, 50);

        }
        else if (string.Equals(args.purchasedProduct.definition.id, hearts110, StringComparison.Ordinal))
        {
            Debug.Log("Give player 110 hearts");

            GameManager.instance.player.SavePlayer(0, 0, 110);

        }
        else if (string.Equals(args.purchasedProduct.definition.id, hearts300, StringComparison.Ordinal))
        {
            Debug.Log("Give player 300 hearts");

            GameManager.instance.player.SavePlayer(0, 0, 300);

        }
        else if (string.Equals(args.purchasedProduct.definition.id, hearts650, StringComparison.Ordinal))
        {
            Debug.Log("Give player 650 hearts");

            GameManager.instance.player.SavePlayer(0, 0, 650);

        }
        else
        {
            Debug.Log("Purchase Failed");
        }
        return PurchaseProcessingResult.Complete;
    }

    private void BuyProductID(string productID)
    {
        Debug.Log("Buy Product ID");

        if (IsInitialized())
        {
            Debug.Log("Purchasing is initialized");

            Product product = storeController.products.WithID(productID);


            if (product != null && product.availableToPurchase)
            {
                storeController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("FAIL: NOT INITIALIZED");
        }

    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("Initialization Failed. Check for reason, consider sharing reason with user.");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        storeExtensionProvider = extensions;
    }
}
