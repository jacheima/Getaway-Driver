using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;

    //Open the store
    public bool hasOpenedStore = false;
    private bool hasStoreAchievement = false;

    //Buy an upgrade
    public bool hasBoughtUpgrade = false;
    private bool hasUpgradeAchievement = false;

    //buy a single use item
    public bool hasBoughtSingleUse = false;
    private bool hasSingleUseAchievement = false;

    //turn off the sound
    public bool hasTurnedOffSound = false;
    private bool hasSoundAchievement = false;

    //collect 100 coins
    public bool has100Coins = false;
    private bool has100CoinAchievement = false;

    //collect 500 coins
    public bool has500Coins = false;
    private bool has500CoinAchievement = false;

    //collect 1500 coins
    public bool has1500Coins = false;
    private bool has1500CoinAchievement = false;

    //collect 6000 coins
    public bool has6000Coins = false;
    public bool has6000CoinAchievement = false;


    //score 15,000 points
    //score 30,000 points
    //score 65,000 points
    //score 125,000 points
    //score 250,000 points
    //score 500,000 points
    //score 1,000,000 points

    //unlock a character

    //earn 5 achievements

    private AudioSource achievementSound;

    [SerializeField] private Animator achievementAnimator;
    [SerializeField] private Text achievementText;

    float achievementTime = 2f;
    float timer;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        achievementSound = GetComponent<AudioSource>();
        PlayerPrefs.DeleteAll();
        LoadAchievements();
    }

    // Update is called once per frame
    void Update()
    {
        if(achievementAnimator.GetBool("gotAchievement") == true)
        {
            timer -= Time.deltaTime;

            if(timer < 0)
            {
                achievementAnimator.SetBool("gotAchievement", false);
            }
        }

        if(hasOpenedStore && !hasStoreAchievement)
        {
            timer = achievementTime;

            achievementText.text = "Going Shopping!";

            achievementAnimator.SetBool("gotAchievement", true);

            float newMod = GameManager.instance.player.GetScoreModifier() + 1;
            GameManager.instance.player.SetScoreModifier(newMod);

            SaveAchievement("Opened Store", 1);

            achievementSound.Play();

            hasStoreAchievement = true;
        }

        if (hasBoughtUpgrade && !hasUpgradeAchievement)
        {
            timer = achievementTime;

            achievementText.text = "Upgrades!";

            achievementAnimator.SetBool("gotAchievement", true);

            float newMod = GameManager.instance.player.GetScoreModifier() + 1;
            GameManager.instance.player.SetScoreModifier(newMod);

            SaveAchievement("Bought Upgrade", 1);

            achievementSound.Play();

            hasUpgradeAchievement = true;
        }

        if (hasBoughtSingleUse && !hasSingleUseAchievement)
        {
            timer = achievementTime;

            achievementText.text = "Single Use!";

            achievementAnimator.SetBool("gotAchievement", true);

            float newMod = GameManager.instance.player.GetScoreModifier() + 1;
            GameManager.instance.player.SetScoreModifier(newMod);

            SaveAchievement("Bought Single Use", 1);

            achievementSound.Play();

            hasSingleUseAchievement = true;
        }

        if (has100Coins && !has100CoinAchievement)
        {
            timer = achievementTime;

            achievementText.text = "100 Coins";

            achievementAnimator.SetBool("gotAchievement", true);

            float newMod = GameManager.instance.player.GetScoreModifier() + 1;
            GameManager.instance.player.SetScoreModifier(newMod);

            SaveAchievement("100 Coins", 1);

            achievementSound.Play();

            has100CoinAchievement = true;
        }

        if (has500Coins && !has500CoinAchievement)
        {
            timer = achievementTime;

            achievementText.text = "500 Coins";

            achievementAnimator.SetBool("gotAchievement", true);

            float newMod = GameManager.instance.player.GetScoreModifier() + 1;
            GameManager.instance.player.SetScoreModifier(newMod);

            SaveAchievement("500 Coins", 1);

            achievementSound.Play();

            has500CoinAchievement = true;
        }

        if (has1500Coins && !has1500CoinAchievement)
        {
            timer = achievementTime;

            achievementText.text = "1500 Coins";

            achievementAnimator.SetBool("gotAchievement", true);

            float newMod = GameManager.instance.player.GetScoreModifier() + 1;
            GameManager.instance.player.SetScoreModifier(newMod);

            SaveAchievement("1500 Coins", 1);

            achievementSound.Play();

            has1500CoinAchievement = true;
        }

        if (has6000Coins && !has6000CoinAchievement)
        {
            timer = achievementTime;

            achievementText.text = "6000 Coins";

            achievementAnimator.SetBool("gotAchievement", true);

            float newMod = GameManager.instance.player.GetScoreModifier() + 1;
            GameManager.instance.player.SetScoreModifier(newMod);

            SaveAchievement("6000 Coins", 1);

            achievementSound.Play();

            has6000CoinAchievement = true;
        }
    }

    void SaveAchievement(string prefName, int value)
    {
        PlayerPrefs.SetInt(prefName, value);
    }

    void LoadAchievements()
    {
        if(!PlayerPrefs.HasKey("Opened Store"))
        {
            PlayerPrefs.SetInt("Opened Store", 0);
        }
        else
        {
            if(PlayerPrefs.GetInt("Opened Store") == 0)
            {
                hasOpenedStore = false;
                hasStoreAchievement = false;
            }

            if(PlayerPrefs.GetInt("Opened Store") == 1)
            {
                hasOpenedStore = true;
                hasStoreAchievement = true;
            }
        }

        if (!PlayerPrefs.HasKey("Bought Upgrade"))
        {
            PlayerPrefs.SetInt("Bought Upgrade", 0);
        }
        else
        {
            if (PlayerPrefs.GetInt("Bought Upgrade") == 0)
            {
                hasBoughtUpgrade = false;
                hasUpgradeAchievement = false;
            }

            if (PlayerPrefs.GetInt("Bought Upgrade") == 1)
            {
                hasBoughtUpgrade = true;
                hasUpgradeAchievement = true;
            }
        }

        if (!PlayerPrefs.HasKey("Bought Single Use"))
        {
            PlayerPrefs.SetInt("Bought Single Use", 0);
        }
        else
        {
            if (PlayerPrefs.GetInt("Bought Single Use") == 0)
            {
                hasBoughtSingleUse = false;
                hasSingleUseAchievement = false;
            }

            if (PlayerPrefs.GetInt("Bought Single Use") == 1)
            {
                hasBoughtSingleUse = true;
                hasSingleUseAchievement = true;
            }
        }

        if (!PlayerPrefs.HasKey("100 Coins"))
        {
            PlayerPrefs.SetInt("100 Coins", 0);
        }
        else
        {
            if (PlayerPrefs.GetInt("100 Coins") == 0)
            {
                has100Coins = false;
                has100CoinAchievement = false;
            }

            if (PlayerPrefs.GetInt("100 Coins") == 1)
            {
                has100Coins = true;
                has100CoinAchievement = true;
            }
        }

        if (!PlayerPrefs.HasKey("500 Coins"))
        {
            PlayerPrefs.SetInt("500 Coins", 0);
        }
        else
        {
            if (PlayerPrefs.GetInt("500 Coins") == 0)
            {
                has500Coins = false;
                has500CoinAchievement = false;
            }

            if (PlayerPrefs.GetInt("500 Coins") == 1)
            {
                has500Coins = true;
                has500CoinAchievement = true;
            }
        }

        if (!PlayerPrefs.HasKey("1500 Coins"))
        {
            PlayerPrefs.SetInt("1500 Coins", 0);
        }
        else
        {
            if (PlayerPrefs.GetInt("1500 Coins") == 0)
            {
                has1500Coins = false;
                has1500CoinAchievement = false;
            }

            if (PlayerPrefs.GetInt("1500 Coins") == 1)
            {
                has1500Coins = true;
                has1500CoinAchievement = true;
            }
        }

        if (!PlayerPrefs.HasKey("6000 Coins"))
        {
            PlayerPrefs.SetInt("6000 Coins", 0);
        }
        else
        {
            if (PlayerPrefs.GetInt("6000 Coins") == 0)
            {
                has6000Coins = false;
                has6000CoinAchievement = false;
            }

            if (PlayerPrefs.GetInt("6000 Coins") == 1)
            {
                has6000Coins = true;
                has6000CoinAchievement = true;
            }
        }
    }
}
