using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Purchaser : MonoBehaviour
{
    public Upgrades upgrades;

    public int headstartCost = 2000;
    public int scoreBoostCost = 3000;

    public void BuyScoreUpgrade()
    {
        if (GameManager.instance.player.scoreModUpgradeCount >= upgrades.upgradedDurations.Count)
        {
            GameManager.instance.OpenMaxedOutWarnging();
        }
        else
        {


            if (GameManager.instance.player.GetCoins() > upgrades.upgradeCosts[GameManager.instance.player.scoreModUpgradeCount + 1])
            {
                if(AchievementManager.instance.hasBoughtUpgrade == false)
                {
                    AchievementManager.instance.hasBoughtUpgrade = true;
                }
                //increase the number of times we have bought this upgrade
                GameManager.instance.player.scoreModUpgradeCount++;

                //subtract the cost of the upgrade from the players coings
                GameManager.instance.player.SavePlayer(0, -upgrades.upgradeCosts[GameManager.instance.player.scoreModUpgradeCount], 0);

                //set the new upgrade duration to the player
                GameManager.instance.player.SetScoreModDuration(upgrades.upgradedDurations[GameManager.instance.player.scoreModUpgradeCount]);

                GameManager.instance.x2Upgrade[GameManager.instance.player.scoreModUpgradeCount].SetActive(true);

                GameManager.instance.OpenSuccessBox();
            }
            else
            {
                GameManager.instance.OpenMoneyWarning();
            }
        }
    }

    public void BuyCoinMagUpgrade()
    {
        if (GameManager.instance.player.coinMagentUpgradeCount >= upgrades.upgradedDurations.Count)
        {
            GameManager.instance.OpenMaxedOutWarnging();
        }
        else
        {

            if (GameManager.instance.player.GetCoins() > upgrades.upgradeCosts[GameManager.instance.player.coinMagentUpgradeCount + 1])
            {
                if (AchievementManager.instance.hasBoughtUpgrade == false)
                {
                    AchievementManager.instance.hasBoughtUpgrade = true;
                }

                //increase the number of times we have bought this upgrade
                GameManager.instance.player.coinMagentUpgradeCount++;

                //subtract the cost of the upgrade from the players coings
                GameManager.instance.player.SavePlayer(0, -upgrades.upgradeCosts[GameManager.instance.player.coinMagentUpgradeCount], 0);

                Debug.Log("Subtracted " + upgrades.upgradeCosts[GameManager.instance.player.coinMagentUpgradeCount] + " from the player.");

                //set the new upgrade duration to the player
                GameManager.instance.player.SetCoinMagDuration(upgrades.upgradedDurations[GameManager.instance.player.coinMagentUpgradeCount]);

                GameManager.instance.coinMagnent[GameManager.instance.player.coinMagentUpgradeCount].SetActive(true);

                GameManager.instance.OpenSuccessBox();
            }
            else
            {
                GameManager.instance.OpenMoneyWarning();
            }
        }
    }

    public void BuyNitroUpgrade()
    {
        if (GameManager.instance.player.nitroUpgradeCount >= upgrades.upgradedDurations.Count)
        {
            GameManager.instance.OpenMaxedOutWarnging();
        }
        else
        {
            if (GameManager.instance.player.GetCoins() > upgrades.upgradeCosts[GameManager.instance.player.nitroUpgradeCount + 1])
            {
                if (AchievementManager.instance.hasBoughtUpgrade == false)
                {
                    AchievementManager.instance.hasBoughtUpgrade = true;
                }

                //increase the number of times we have bought this upgrade
                GameManager.instance.player.nitroUpgradeCount++;

                //subtract the cost of the upgrade from the players coings
                GameManager.instance.player.SavePlayer(0, -upgrades.upgradeCosts[GameManager.instance.player.nitroUpgradeCount], 0);

                //set the new upgrade duration to the player
                GameManager.instance.player.SetNitroDuration(upgrades.upgradedDurations[GameManager.instance.player.nitroUpgradeCount]);

                GameManager.instance.nitroBoost[GameManager.instance.player.nitroUpgradeCount].SetActive(true);

                GameManager.instance.OpenSuccessBox();
            }
            else
            {
                GameManager.instance.OpenMoneyWarning();
            }
        }
    }

    public void BuyHeadstart()
    {
        if (GameManager.instance.player.GetCoins() > headstartCost)
        {
            if(AchievementManager.instance.hasBoughtSingleUse == false)
            {
                AchievementManager.instance.hasBoughtSingleUse = true;
            }

            GameManager.instance.player.SavePlayer(0, -headstartCost, 0);
            GameManager.instance.player.AddHeadstart();
            GameManager.instance.OpenSuccessBox();
        }
        else
        {
            GameManager.instance.OpenMoneyWarning();
        }
    }

    public void BuyX5ScoreMultiplier()
    {
        if (GameManager.instance.player.GetCoins() > scoreBoostCost)
        {

            if (AchievementManager.instance.hasBoughtSingleUse == false)
            {
                AchievementManager.instance.hasBoughtSingleUse = true;
            }

            GameManager.instance.player.SavePlayer(0, -scoreBoostCost, 0);
            GameManager.instance.player.AddScoreBoost();
            GameManager.instance.OpenSuccessBox();
        }
        else
        {
            GameManager.instance.OpenMoneyWarning();
        }
    }

}
