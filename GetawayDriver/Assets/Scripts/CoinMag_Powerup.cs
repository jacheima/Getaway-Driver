using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinMag_Powerup : MonoBehaviour
{
    bool startTimer;
    Player player;

    public float timer;
    public GameObject timerUI;
    public Image[] timerImages;

    private void Update()
    {
        if(startTimer == true)
        {
            timer -= Time.deltaTime;

            if(timer < 9f)
            {
                timerImages[0].enabled = false;
            }
            
            if(timer < 8f)
            {
                timerImages[0].enabled = false;
                timerImages[1].enabled = false;
            }
            
            if (timer < 7f)
            {
                timerImages[0].enabled = false;
                timerImages[1].enabled = false;
                timerImages[2].enabled = false;
            }
            
            if (timer < 6f)
            {
                timerImages[0].enabled = false;
                timerImages[1].enabled = false;
                timerImages[2].enabled = false;
                timerImages[3].enabled = false;
            }
            
            if (timer < 5f)
            {
                timerImages[0].enabled = false;
                timerImages[1].enabled = false;
                timerImages[2].enabled = false;
                timerImages[3].enabled = false;
                timerImages[4].enabled = false;
            }
            
            if (timer < 4f)
            {
                timerImages[0].enabled = false;
                timerImages[1].enabled = false;
                timerImages[2].enabled = false;
                timerImages[3].enabled = false;
                timerImages[4].enabled = false;
                timerImages[5].enabled = false;
            }
            
            if (timer < 3f)
            {
                timerImages[0].enabled = false;
                timerImages[1].enabled = false;
                timerImages[2].enabled = false;
                timerImages[3].enabled = false;
                timerImages[4].enabled = false;
                timerImages[5].enabled = false;
                timerImages[6].enabled = false;
            }
            
            if (timer < 2f)
            {
                timerImages[0].enabled = false;
                timerImages[1].enabled = false;
                timerImages[2].enabled = false;
                timerImages[3].enabled = false;
                timerImages[4].enabled = false;
                timerImages[5].enabled = false;
                timerImages[6].enabled = false;
                timerImages[7].enabled = false;
            }
            
            if (timer < 1f)
            {
                timerImages[0].enabled = false;
                timerImages[1].enabled = false;
                timerImages[2].enabled = false;
                timerImages[3].enabled = false;
                timerImages[4].enabled = false;
                timerImages[5].enabled = false;
                timerImages[6].enabled = false;
                timerImages[7].enabled = false;
                timerImages[8].enabled = false;
            }

            if (timer <= 0)
            {
                player.coinMag.enabled = false;

                timerUI.SetActive(false);

                for (int i = 0; i < timerImages.Length; i++)
                {
                    timerImages[i].enabled = true;
                }

                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>())
        {
            this.GetComponent<AudioSource>().Play();

            player = other.GetComponent<Player>();

            timerUI.SetActive(true);

            player.coinMag.enabled = true;
            this.GetComponent<Renderer>().enabled = false;
            this.GetComponent<Collider>().enabled = false;
            startTimer = true;
        }
    }
}
