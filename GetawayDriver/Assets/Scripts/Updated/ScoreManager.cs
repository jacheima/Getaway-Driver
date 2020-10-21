using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [Header("Player Score")]
    public float scoreBase;

    public float score;

    [Header("Score Modifier")]
    public float timeToFill;

    public Image modifierGuageFill;
    public Text scoreText;
    public TextMeshProUGUI scoreModText;

    public float scoreMod = 1;

    private float scoreModProgress = 0.086f;
    private float baseMod;
    public float modifier = 1;

    [SerializeField] private SpeedManager speedManager;

    private void Start()
    {

        Load();

        baseMod = 1 / timeToFill;
        modifier = 1;

        modifierGuageFill.fillAmount = .086f;
    }
    private void ScorePlayer()
    {
        score += scoreBase * scoreMod * Time.deltaTime;
    }

    public void AdjustMultiplier(float newMultiplyer)
    {
        scoreMod += newMultiplyer;
    }

    void AdjustModifier()
    {
        modifier -= scoreMod / 50;
    }

    private void Update()
    {

        if (GameManager.instance.currentGameState == GameManager.GameState.LEVEL)
        {
            if(GameManager.instance.player.GetZSpeed() > 0)
            {
                //track the players score
                ScorePlayer();
            }
            
            if (scoreMod < 10f)
            {
                //if scoreModProgress is less than one
                if (scoreModProgress < .9)
                {
                    //add to the progress
                    scoreModProgress += baseMod * modifier * Time.deltaTime;
                }
                //otherwise,
                else
                {
                    //return it to 0 and add to the scoreMod
                    scoreModProgress = 0.086f;
                    AdjustMultiplier(1);
                    AdjustModifier();
                    
                } 
            }

            if(scoreMod == 10)
            {
                scoreModProgress = 1;
            }
        }

        //update the score UI
        UpdateUI();
    }

    private void UpdateUI()
    {
        //adjust the image fill on the score progress bar
        modifierGuageFill.fillAmount = scoreModProgress;

        //adjust the text of the score mod
        scoreModText.text = "x" + scoreMod;

        //show the player score
        scoreText.text = ((int)score).ToString();
    }

    private void Load()
    {

    }
}
