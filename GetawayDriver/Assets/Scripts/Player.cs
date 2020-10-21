using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    private int coins;
    private int score;
    private int hearts;
    private float scoreModifier = 1;

    private float zSpeed;
    private float xSpeed;

    private int scoreBase = 20;
    private int coinsValue = 1;

    public float speedMultiplier = 1;

    public CoinMagnet coinMag;

    [Header("Upgrades & Single Use")]
    private float coinSpeed = 5;
    private float headstart = 0;
    private float scoreBoost = 0;
   

    private float scoreModDuration;
    private float coinMagDuration;
    private float nitroDuration;

    //the number of times the player has upgraded the duration of the x2 score upgrade
    public int scoreModUpgradeCount = 0;

    //the number of times the player has upgraded the duration of the coin magent upgrade
    public int coinMagentUpgradeCount = 0;

    //the number of times the player has upgraded the duration of the nitro score upgrade
    public int nitroUpgradeCount = 0;

    private float musicVol;
    private float sfxVol;


    private void Start()
    {
        zSpeed = GetComponent<PlayerController>().zSpeed;
        xSpeed = GetComponent<PlayerController>().xSpeed;

        //PlayerPrefs.DeleteAll();

        LoadPlayer();
    }

    private void UpdateScore(int newScore)
    {
        if (score < newScore)
        {
            score = newScore;
        }

        if (PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    private void UpdateHearts(int newHearts)
    {
        Debug.Log("Adding " + newHearts + "to the players heart count");

        hearts += newHearts;

        if (PlayerPrefs.HasKey("Hearts"))
        {
            PlayerPrefs.SetInt("Hearts", hearts);
        }
    }

    public float GetScoreModifier()
    {
        return scoreModifier;
    }

    public void SetScoreModifier(float newScoreMod)
    {
        scoreModifier = newScoreMod;

        if(PlayerPrefs.HasKey("Score Modifier"))
        {
            PlayerPrefs.SetFloat("Score Modifier", scoreModifier);
        }
    }

    public float GetMusicVol()
    {
        return musicVol;
    }

    public float GetSoundFXVol()
    {
        return sfxVol;
    }

    public void SetMusicVol(float newVol)
    {
        musicVol = newVol;

        if(PlayerPrefs.HasKey("MusicVol"))
        {
            PlayerPrefs.SetFloat("MusicVol", musicVol);
        }
    }

    public void SetSFXVol(float newVol)
    {
        sfxVol = newVol;

        if(PlayerPrefs.HasKey("sfxVol"))
        {
            PlayerPrefs.SetFloat("sfxVol", sfxVol);
        }
    }

    private void UpdateCoins(int newCoins)
    {
        coins += newCoins;

        // --------------------- Achievement Checks -----------------------------
        if (AchievementManager.instance.has100Coins == false && coins >= 100)
        {
            AchievementManager.instance.has100Coins = true;
        }

        if (AchievementManager.instance.has500Coins == false && coins >= 500)
        {
            AchievementManager.instance.has500Coins = true;
        }

        if (AchievementManager.instance.has1500Coins == false && coins >= 1500)
        {
            AchievementManager.instance.has1500Coins = true;
        }

        if (AchievementManager.instance.has6000Coins == false && coins >= 6000)
        {
            AchievementManager.instance.has6000Coins = true;
        }

        //----------------------- Save Stats ------------------------------------

        if (PlayerPrefs.HasKey("Coins"))
        {
            PlayerPrefs.SetInt("Coins", coins);
        }
    }

    public void AddHeadstart()
    {
        headstart++;

        if (PlayerPrefs.HasKey("Headstarts"))
        {
            PlayerPrefs.SetFloat("Headstarts", headstart);
        }
    }

    public void AddScoreBoost()
    {
        scoreBoost++;

        if (PlayerPrefs.HasKey("ScoreBoosts"))
        {
            PlayerPrefs.SetFloat("ScoreBoosts", scoreBoost);
        }
    }

    public void RemoveHeadstart()
    {
        headstart--;

        if (PlayerPrefs.HasKey("Headstarts"))
        {
            PlayerPrefs.SetFloat("Headstarts", headstart);
        }
    }

    public void RemoveScoreBoost()
    {
        scoreBoost--;

        if (PlayerPrefs.HasKey("ScoreBoosts"))
        {
            PlayerPrefs.SetFloat("ScoreBoosts", scoreBoost);
        }
    }

    public float GetScoreBoost()
    {
        return scoreBoost;
    }

    public float GetHeadstart()
    {
        return headstart;
    }

    public float GetZSpeed()
    {
        return zSpeed;
    }

    public void SetZSpeed(float newZSpeed)
    {
        zSpeed = newZSpeed;
    }

    public float GetXSpeed()
    {
        return xSpeed;
    }

    public int GetScoreBase()
    {
        return scoreBase;
    }
    public float GetScore()
    {
        return score;
    }

    public float GetCoins()
    {
        return coins;
    }

    public int GetHearts()
    {
        return hearts;
    }

    public int GetCoinValue()
    {
        return coinsValue;
    }

    public float GetScoreModDuration()
    {
        return scoreModDuration;
    }

    public float GetCoinMagDuration()
    {
        return coinMagDuration;
    }

    public float GetNitroDuration()
    {
        return nitroDuration;
    }

    public void SetScoreModDuration(float newDuration)
    {
        scoreModDuration = newDuration;

        if (PlayerPrefs.HasKey("ScoreModDuration"))
        {
            PlayerPrefs.SetFloat("ScoreModDuration", scoreModDuration);
        }

        if (PlayerPrefs.HasKey("ScoreUpgradeCount"))
        {
            PlayerPrefs.SetInt("ScoreUpgradeCount", scoreModUpgradeCount);
        }
    }

    public void SetCoinMagDuration(float newDuration)
    {
        coinMagDuration = newDuration;

        if (PlayerPrefs.HasKey("CoinMagDuration"))
        {
            PlayerPrefs.SetFloat("CoinMagDuration", coinMagDuration);
        }

        if (PlayerPrefs.HasKey("CoinMagUpgradeCount"))
        {
            PlayerPrefs.SetInt("CoinMagUpgradeCount", coinMagentUpgradeCount);
        }
    }

    public void SetNitroDuration(float newDuration)
    {
        nitroDuration = newDuration;

        if (PlayerPrefs.HasKey("NitroDuration"))
        {
            PlayerPrefs.SetFloat("NitroDuration", nitroDuration);
        }

        if (PlayerPrefs.HasKey("NirtoUpgradeCount"))
        {
            PlayerPrefs.SetInt("NitroUpgradeCount", nitroUpgradeCount);
        }
    }

    public void SetCoinValue(int newValue)
    {
        coinsValue = newValue;

        if (PlayerPrefs.HasKey("CoinValue"))
        {
            PlayerPrefs.SetInt("CoinValue", coinsValue);
        }
    }

    public void SavePlayer(int newScore, int coinsToAdd, int newHearts)
    {
        UpdateScore(newScore);
        UpdateCoins(coinsToAdd);
        UpdateHearts(newHearts);
    }

    public void LoadPlayer()
    {
        //If there are no player prefs for high score, create a new player prefs key for high score
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        //otherwise, get the data and store it in the score variable
        else
        {
            score = PlayerPrefs.GetInt("HighScore");
        }

        //if there are no player prefs for coins, create a new player prefs key for coins
        if (!PlayerPrefs.HasKey("Coins"))
        {
            PlayerPrefs.SetInt("Coins", coins);
        }
        //otherwise, get the data and store it in the coins variable
        else
        {
            coins = PlayerPrefs.GetInt("Coins");
        }

        //if there are no player prefs for hearts, create a new player prefs key for hearts
        if (!PlayerPrefs.HasKey("Hearts"))
        {
            PlayerPrefs.SetInt("Hearts", hearts);
        }
        //otherwise, get the data and store it in the hearts variable
        else
        {
            hearts = PlayerPrefs.GetInt("Hearts", hearts);
        }

        if (!PlayerPrefs.HasKey("CoinValue"))
        {
            PlayerPrefs.SetInt("CoinValue", coinsValue);
        }
        else
        {
            coinsValue = PlayerPrefs.GetInt("CoinValue");
        }

        if (!PlayerPrefs.HasKey("NitroDuration"))
        {
            PlayerPrefs.SetFloat("NitroDuration", nitroDuration);
        }
        else
        {
            nitroDuration = PlayerPrefs.GetFloat("NitroDuration");
        }

        if (!PlayerPrefs.HasKey("CoinMagDuration"))
        {
            PlayerPrefs.SetFloat("CoinMagDuration", coinMagDuration);
        }
        else
        {
            coinMagDuration = PlayerPrefs.GetFloat("CoinMagDuration");
        }

        if (!PlayerPrefs.HasKey("ScoreModDuration"))
        {
            PlayerPrefs.SetFloat("ScoreModDuration", scoreModDuration);
        }
        else
        {
            scoreModDuration = PlayerPrefs.GetFloat("ScoreModDuration");
        }

        if (!PlayerPrefs.HasKey("ScoreUpgradeCount"))
        {
            PlayerPrefs.SetInt("ScoreUpgradeCount", scoreModUpgradeCount);
        }
        else
        {
            scoreModUpgradeCount = PlayerPrefs.GetInt("ScoreUpgradeCount");

            if (GameManager.instance != null)
            {
                Debug.Log("Loading scoreModUpgradeCount");

                for (int index = 0; index < scoreModUpgradeCount; index++)
                {

                    GameManager.instance.x2Upgrade[index + 1].SetActive(true);

                }
            }
        }

        if (!PlayerPrefs.HasKey("CoinMagUpgradeCount"))
        {
            PlayerPrefs.SetInt("CoinMagUpgradeCount", coinMagentUpgradeCount);
        }
        else
        {
            coinMagentUpgradeCount = PlayerPrefs.GetInt("CoinMagUpgradeCount");

            if (GameManager.instance != null)
            {
                for (int index = 0; index < coinMagentUpgradeCount; index++)
                {

                    GameManager.instance.coinMagnent[index + 1].SetActive(true);

                }
            }
        }

        if (!PlayerPrefs.HasKey("NitroUpgradeCount"))
        {
            PlayerPrefs.SetInt("NitroUpgradeCount", nitroUpgradeCount);
        }
        else
        {
            nitroUpgradeCount = PlayerPrefs.GetInt("NitroUpgradeCount");

            if (GameManager.instance != null)
            {
                for (int index = 0; index < nitroUpgradeCount; index++)
                {
                    GameManager.instance.nitroBoost[index + 1].SetActive(true);
                }
            }
        }

        if (!PlayerPrefs.HasKey("Headstarts"))
        {
            PlayerPrefs.SetFloat("Headstarts", headstart);
        }
        else
        {
            headstart = PlayerPrefs.GetFloat("Headstarts");
        }

        if (!PlayerPrefs.HasKey("ScoreBoosts"))
        {
            PlayerPrefs.SetFloat("ScoreBoosts", scoreBoost);
        }
        else
        {
            scoreBoost = PlayerPrefs.GetFloat("ScoreBoosts");
        }

        if(!PlayerPrefs.HasKey("MusicVol"))
        {
            musicVol = 0;
            PlayerPrefs.SetFloat("MusicVol", musicVol);
        }
        else
        {
            musicVol = PlayerPrefs.GetFloat("MusicVol");
            GameManager.instance.mixer.SetFloat("Music", musicVol);
            
            if(musicVol > -80f)
            {
                GameManager.instance.musicButton.image.sprite = GameManager.instance.musicOnSprite;
            }
            else
            {
                GameManager.instance.musicButton.image.sprite = GameManager.instance.musicOffSprite;
            }
        }

        if(!PlayerPrefs.HasKey("sfxVol"))
        {
            sfxVol = 0;
            PlayerPrefs.SetFloat("sfxVol", sfxVol);
        }
        else
        {
            sfxVol = PlayerPrefs.GetFloat("sfxVol");
            GameManager.instance.mixer.SetFloat("Sound Effects", sfxVol);

            if (sfxVol > -80f)
            {
                GameManager.instance.sfxButton.image.sprite = GameManager.instance.sfxOnSprite;
            }
            else
            {
                GameManager.instance.sfxButton.image.sprite = GameManager.instance.sfxOffSprite;
            }
        }

        if(!PlayerPrefs.HasKey("Score Modifier"))
        {
            PlayerPrefs.SetFloat("Score Modifier", scoreModifier);
        }
        else
        {
            scoreModifier = PlayerPrefs.GetFloat("Score Modifier");
        }
    }

}

