using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using GoogleMobileAds.Api;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum GameState
    {
        CUSTOMIZATION, LEVEL, MENU, SHOP, SETTINGS
    }

    public GameState currentGameState;
    private GameState previousGameState;

    public int scoreBase;

    public Player player;
    public float scoreMultiplier;

    private float score;

    private int hearts;
    private int coins = 0;

    public Upgrades upgrades;

    [Header("Cameras")]
    public Camera levelCamera;
    public Camera chopShopCamera;

    [Header("Ads")]
    [SerializeField] private AdsManager adsManager;

    [Header("Starting Tiles")]
    public List<Obstacle> startingObstacles;
    public List<Coin> startingCoins;
    public List<Heart> startingHearts;

    [Header("Tiles")]
    public List<GameObject> spawnedTiles;
    public List<TileSpawner> startingTiles;

    [Header("Starting Positions")]
    [SerializeField] private Vector3 playerStartPosition;
    [SerializeField] private Vector3 cameraStartPosition;
    [SerializeField] private Vector3 cameraStartRotation;
    [SerializeField] private Vector3 lookAtStartPosition;

    [Header("GameOver")]
    [SerializeField] private float gameOverCount;
    [SerializeField] private Image timerFill;
    [SerializeField] private Text goHeartsText;
    private float gameoverTimer;
    private bool isDead = false;

    [Header("Play Stats")]
    [SerializeField] private Vector3 cameraPlayPosition;
    [SerializeField] private Vector3 lookAtPlayPosition;
    private bool startGame = false;
    private float cameraSpeed = 10;

    [Header("Unpause Countdown")]
    private float countdownStart = 4;
    private float unpauseTimer = 0;
    private bool startCountdown = false;
    private Animator countdDownAnimator;

    [Header("Main Menu UI Elements")]
    [SerializeField] private Text tapToPlayText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text heartsCountText;
    [SerializeField] private Text coinCountText;

    [Header("Level UI Elements")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text coinText;
    [SerializeField] private Text multiplierText;
    [SerializeField] private Text countdownText;

    [Header("Shop UI Elements")]
    [SerializeField] private GameObject coinsPage;
    [SerializeField] private GameObject heartsPage;
    [SerializeField] private GameObject BoostsPage;

    [Header("Shop - Coins Page UI Elements")]
    public GameObject boughtDoubleCoins;
    public GameObject doubleCoinsButton;
    public Button coinsPageNavButton;
    public Sprite selectedCoinNavButton;
    public Sprite unSelectedCoinsNavButton;

    [Header("Shop - Boosts Page UI Elements")]
    [SerializeField] private Text scoreUpgradeButtonText;
    [SerializeField] private Text coinMagUpgradeButtonText;
    [SerializeField] private Text nitroUpgradeButtonText;
    public Button boostsPageNavButton;
    public Sprite selectedBoostsNavButton;
    public Sprite unSelectedBoostsNavButton;

    [Header("Shop - Hearts Page UI Elements")]
    public Button heartsPageNavButton;
    public Sprite selectedHeartsNavButton;
    public Sprite unSelectedHeartsNavButton;

    [Header("Upgrade Indicators")]
    public List<GameObject> x2Upgrade;
    public List<GameObject> coinMagnent;
    public List<GameObject> nitroBoost;

    [Header("Settings")]
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;
    public Sprite sfxOnSprite;
    public Sprite sfxOffSprite;
    public Button musicButton;
    public Button sfxButton;
    [SerializeField] private Sprite OnTextSprite;
    [SerializeField] private Sprite OffTextSprite;
    [SerializeField] private Image musicOnOffImage;
    [SerializeField] private Image sfxOnOffImage;

    [Header("UI Screens")]
    [SerializeField] private GameObject levelUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject gameoverUI;
    [SerializeField] private GameObject statsBar;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject areYouSureBox;
    [SerializeField] private GameObject settingsUI;
    public GameObject successBox;
    public GameObject notEnoughMoney;
    public GameObject maxedOut;



    [Header("Audio")]
    private AudioSource music;
    public AudioMixer mixer;
    bool musicOn;
    bool sfxOn;
    float musicVol;
    float sfxVol;


    public GameObject lookAtTarget;


    private void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        music = player.gameObject.GetComponent<AudioSource>();
        unpauseTimer = countdownStart;
        countdDownAnimator = countdownText.gameObject.GetComponent<Animator>();

        scoreMultiplier = player.GetScoreModifier();

        hearts = 0;

        gameoverTimer = gameOverCount;

        adsManager.RequestInterstitial();

        chopShopCamera.enabled = false;
        levelCamera.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        ScorePlayer();

        if (startCountdown)
        {
            Unpause();
        }

        if (startGame)
        {
            StartLevel();
        }

        if (currentGameState == GameState.MENU)
        {
            music.Stop();

            DisableMovement();

            tapToPlayText.enabled = true;

            mainMenuUI.SetActive(true);
            statsBar.SetActive(true);
        }

        if (isDead)
        {
            gameoverTimer -= Time.deltaTime;

            timerFill.fillAmount -= .2f * Time.deltaTime;

            if (gameoverTimer < 0)
            {
                ResetLevel();
            }
        }


    }

    private void DisableMovement()
    {
        player.GetComponent<PlayerController>().zSpeed = 0f;
        player.GetComponent<PlayerController>().xSpeed = 0f;
    }

    private void EnableMovement()
    {
        player.GetComponent<PlayerController>().zSpeed = player.GetZSpeed();
        player.GetComponent<PlayerController>().xSpeed = player.GetXSpeed();
    }

    private void StartLevel()
    {
        if (levelCamera.transform.position != cameraPlayPosition)
        {
            levelCamera.transform.position = Vector3.MoveTowards(levelCamera.transform.position, cameraPlayPosition, cameraSpeed * Time.deltaTime);
        }
        else
        {
            startGame = false;

            EnableMovement();

            levelCamera.GetComponent<CameraController>().lookAtTarget = lookAtTarget.transform;

            music.Play();

            tapToPlayText.enabled = false;

            mainMenuUI.SetActive(false);
            statsBar.SetActive(false);
            levelUI.SetActive(true);
        }
    }

    private void Unpause()
    {
        unpauseTimer -= Time.deltaTime;

        if (unpauseTimer > 1)
        {
            countdownText.text = ((int)unpauseTimer).ToString();
        }
        else if (unpauseTimer < 1)
        {
            countdownText.text = "GO!";
            countdownText.gameObject.SetActive(false);

            EnableMovement();

            scoreBase = player.GetScoreBase();

            music.Play();

            unpauseTimer = countdownStart;
            startCountdown = false;
        }
    }

    public void AddCoin()
    {
        coins += player.GetCoinValue();
    }

    public void AddHeart()
    {
        hearts++;
    }

    public void AdjustMultiplier(float newMultiplyer)
    {
        scoreMultiplier = newMultiplyer;
    }

    public void ScorePlayer()
    {
        score += scoreBase * scoreMultiplier * Time.deltaTime;
    }

    public void SavePlayer()
    {
        player.SavePlayer((int)score, coins, hearts);
    }


    private void UpdateUI()
    {
        scoreText.text = ((int)score).ToString();
        coinText.text = coins.ToString();
        multiplierText.text = "x" + scoreMultiplier;

        Debug.Log("Score Multiplier: " + scoreMultiplier);

        highScoreText.text = player.GetScore().ToString();

        //player coins
        if (player.GetCoins() <= 999)
        {
            coinCountText.text = player.GetCoins().ToString();
        }
        else if (player.GetCoins() >= 1000 && player.GetCoins() <= 999999)
        {
            coinCountText.text = ((int)player.GetCoins() / 1000) + "K";
        }
        else if (player.GetCoins() >= 1000000 && player.GetCoins() <= 9999999)
        {
            coinCountText.text = ((float)player.GetCoins() / 1000000).ToString("F2") + "M";
        }
        else if (player.GetCoins() >= 1000000000 && player.GetCoins() <= 9999999999)
        {
            coinCountText.text = ((float)player.GetCoins() / 1000000000).ToString("F2") + "B";
        }

        //player hearts
        if (player.GetHearts() <= 999)
        {
            heartsCountText.text = player.GetHearts().ToString();
        }
        else if (player.GetHearts() >= 1000 && player.GetHearts() <= 999999)
        {
            heartsCountText.text = ((int)player.GetHearts() / 1000) + "K";
        }
        else if (player.GetHearts() >= 1000000 && player.GetHearts() <= 9999999)
        {
            heartsCountText.text = ((float)player.GetHearts() / 1000000).ToString("F2") + "M";
        }
        else if (player.GetHearts() >= 1000000000)
        {
            heartsCountText.text = ((float)player.GetHearts() / 1000000000).ToString("F2") + "B";
        }


        goHeartsText.text = player.GetHearts().ToString();

        //set button texts for the upgrade costs
        scoreUpgradeButtonText.text = upgrades.upgradeCosts[player.scoreModUpgradeCount + 1].ToString();
        coinMagUpgradeButtonText.text = upgrades.upgradeCosts[player.coinMagentUpgradeCount + 1].ToString();
        nitroUpgradeButtonText.text = upgrades.upgradeCosts[player.nitroUpgradeCount + 1].ToString();
    }

    public void StartGame()
    {
        currentGameState = GameState.LEVEL;
        startGame = true;
    }

    public void PauseGame()
    {
        DisableMovement();

        scoreBase = 0;

        pauseUI.SetActive(true);
        levelUI.SetActive(false);
        areYouSureBox.SetActive(false);

        music.Pause();
    }

    public void ResumeGame()
    {
        countdownText.text = countdownStart.ToString();

        pauseUI.SetActive(false);
        levelUI.SetActive(true);

        countdownText.gameObject.SetActive(true);
        countdDownAnimator.SetBool("startCountdown", true);

        startCountdown = true;
    }

    public void GoToChopShop()
    {
        currentGameState = GameState.CUSTOMIZATION;

        levelCamera.enabled = false;
        chopShopCamera.enabled = true;

        mainMenuUI.SetActive(false);

    }

    public void CloseChopShop()
    {
        levelCamera.enabled = true;
        chopShopCamera.enabled = false;

        GoToMainMenu();
    }

    public void GoToShop()
    {
        currentGameState = GameState.SHOP;

        mainMenuUI.SetActive(false);
        shopUI.SetActive(true);

        if(AchievementManager.instance.hasOpenedStore == false)
        {
            AchievementManager.instance.hasOpenedStore = true;
        }

        OpenCoins();
    }

    public void GoToMainMenu()
    {
        SavePlayer();
        ResetLevel();

        currentGameState = GameState.MENU;

        shopUI.SetActive(false);
        areYouSureBox.SetActive(false);
        coinsPage.SetActive(true);
        heartsPage.SetActive(false);
        BoostsPage.SetActive(false);

    }

    public void AreYouSure()
    {
        areYouSureBox.SetActive(true);
        pauseUI.SetActive(false);
    }

    public void OpenCoins()
    {
        coinsPage.SetActive(true);
        heartsPage.SetActive(false);
        BoostsPage.SetActive(false);

        coinsPageNavButton.image.sprite = selectedCoinNavButton;
        heartsPageNavButton.image.sprite = unSelectedHeartsNavButton;
        boostsPageNavButton.image.sprite = unSelectedBoostsNavButton;
    }

    public void OpenHearts()
    {
        coinsPage.SetActive(false);
        heartsPage.SetActive(true);
        BoostsPage.SetActive(false);

        coinsPageNavButton.image.sprite = unSelectedCoinsNavButton;
        heartsPageNavButton.image.sprite = selectedHeartsNavButton;
        boostsPageNavButton.image.sprite = unSelectedBoostsNavButton;
    }

    public void OpenBoosts()
    {
        coinsPage.SetActive(false);
        heartsPage.SetActive(false);
        BoostsPage.SetActive(true);


        coinsPageNavButton.image.sprite = unSelectedCoinsNavButton;
        heartsPageNavButton.image.sprite = unSelectedHeartsNavButton;
        boostsPageNavButton.image.sprite = selectedBoostsNavButton;
    }

    public void CloseSuccessBox()
    {
        successBox.SetActive(false);
    }

    public void OpenSuccessBox()
    {
        successBox.SetActive(true);
    }

    public void OpenMoneyWarning()
    {
        notEnoughMoney.SetActive(true);
    }

    public void CloseMoneyWarning()
    {
        notEnoughMoney.SetActive(false);
    }

    public void OpenMaxedOutWarnging()
    {
        maxedOut.SetActive(true);
    }

    public void CloseMaxedOutWarning()
    {
        maxedOut.SetActive(false);
    }

    public void KeepPlayingViaHeart()
    {
        //turn off the countdown timer 
        isDead = false;

        //subtract a heart
        hearts--;

        gameoverUI.SetActive(false);
        levelUI.SetActive(true);

        //unpause the game
        unpauseTimer = countdownStart;
        ResumeGame();
    }

    public void KeepPlayingViaAd()
    {
        //Show an ad then resume gameplay
    }

    public void UseScoreBoost()
    {
        if (player.GetScoreBoost() > 0)
        {
            int used = 0;

            if (used <= 3)
            {
                player.RemoveScoreBoost();
                scoreMultiplier += 5;
                used++;
            }
            else
            {
                Debug.Log("Player cannot use more scoreboosts");
            }
        }
    }

    public void UseHeadStart()
    {
        if (player.GetHeadstart() > 0)
        {
            player.RemoveHeadstart();

            //do headstart stuff
        }
    }

    public void OpenSettings()
    {
        settingsUI.SetActive(true);

        if (currentGameState == GameState.MENU)
        {
            previousGameState = currentGameState;
            currentGameState = GameState.SETTINGS;

            mainMenuUI.SetActive(false);
            statsBar.SetActive(false);
        }
        else if (currentGameState == GameState.SHOP)
        {
            previousGameState = currentGameState;
            currentGameState = GameState.SETTINGS;

            shopUI.SetActive(false);
            statsBar.SetActive(false);
        }
        else if (currentGameState == GameState.LEVEL)
        {
            previousGameState = currentGameState;
            currentGameState = GameState.SETTINGS;

            pauseUI.SetActive(false);
        }
    }

    public void CloseSettings()
    {
        settingsUI.SetActive(false);

        if (previousGameState == GameState.MENU)
        {
            previousGameState = currentGameState;
            currentGameState = GameState.MENU;

            mainMenuUI.SetActive(true);
            statsBar.SetActive(true);
        }
        else if (previousGameState == GameState.SHOP)
        {
            previousGameState = currentGameState;
            currentGameState = GameState.SHOP;

            shopUI.SetActive(true);
            statsBar.SetActive(true);
        }
        else if (previousGameState == GameState.LEVEL)
        {
            previousGameState = currentGameState;
            currentGameState = GameState.LEVEL;

            pauseUI.SetActive(true);
        }
    }

    public void ToggleMusic()
    {

        musicOn = !musicOn;

        if (musicOn)
        {
            mixer.SetFloat("Music", 20);
            player.SetMusicVol(20);

            musicButton.image.sprite = musicOnSprite;
            musicOnOffImage.sprite = OnTextSprite;
        }
        else
        {
            mixer.SetFloat("Music", -80);
            player.SetSFXVol(-80);

            musicButton.image.sprite = musicOffSprite;
            musicOnOffImage.sprite = OffTextSprite;
        }
    }

    public void ToggleSoundFX()
    {
        sfxOn = !sfxOn;

        if (sfxOn)
        {
            mixer.SetFloat("Music", 20);
            player.SetMusicVol(20);

            sfxButton.image.sprite = sfxOnSprite;
            sfxOnOffImage.sprite = OnTextSprite;
        }
        else
        {
            mixer.SetFloat("Music", -80);
            player.SetSFXVol(-80);

            sfxButton.image.sprite = sfxOffSprite;
            sfxOnOffImage.sprite = OffTextSprite;
        }
    }

    public void GameOver()
    {
        gameoverUI.SetActive(true);
        levelUI.SetActive(false);

        DisableMovement();

        scoreBase = 0;

        adsManager.interstitial.Show();

    }

    public void ContinueGameOver()
    {
        isDead = true;

        SavePlayer();
    }



    private void ResetLevel()
    {
        Debug.Log("Calling Reset Level");

        //set the player and camera back to the starting position
        player.gameObject.transform.position = playerStartPosition;
        levelCamera.transform.position = cameraStartPosition;

        //Set the rotation of the camera back to its starting rotation
        Quaternion cameraRotation = levelCamera.transform.rotation;
        cameraRotation.eulerAngles = cameraStartRotation;
        levelCamera.transform.rotation = cameraRotation;

        //reset all of the level counters
        coins = 0;
        score = 0;
        hearts = 0;
        scoreMultiplier = player.GetScoreModifier();
        scoreBase = player.GetScoreBase();

        timerFill.fillAmount = 1f;

        isDead = false;

        gameoverTimer = gameOverCount;

        for (int index = 0; index < startingObstacles.Count; index++)
        {
            startingObstacles[index].myCollider.enabled = true;

            for(int currentObstacle = 0; currentObstacle < startingObstacles[index].myRenderer.Count; currentObstacle++)
            {
                startingObstacles[index].myRenderer[currentObstacle].enabled = true;
            }
            
        }

        for(int index = 0; index < startingCoins.Count; index++)
        {
            startingCoins[index].myCollider.enabled = true;
            startingCoins[index].myRenderer.enabled = true;
        }

        for(int index = 0; index < startingHearts.Count; index++)
        {
            startingHearts[index].myRenderer.enabled = true;
            startingHearts[index].myCollider.enabled = true;
        }

        for(int index = 0; index < spawnedTiles.Count; index++)
        {
            Destroy(spawnedTiles[index]);
        }

        for(int index = 0; index < startingTiles.Count; index++)
        {
            startingTiles[index].isSpawned = false;
        }

        spawnedTiles.Clear();

        currentGameState = GameState.MENU;

        //turn the main menu UI back on
        gameoverUI.SetActive(false);
        mainMenuUI.SetActive(true);
        statsBar.SetActive(true);

        adsManager.RequestInterstitial();
    }


}

