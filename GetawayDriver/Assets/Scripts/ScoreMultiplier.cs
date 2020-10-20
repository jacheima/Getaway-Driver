using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMultiplier : Pickup
{
    bool startCountdown = false;
    float timer;

    float multiplier;

    protected override void Update()
    {
        if(startCountdown)
        {
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                GameManager.instance.AdjustMultiplier(multiplier);
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>())
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            multiplier = GameManager.instance.scoreMultiplier;

            GameManager.instance.AdjustMultiplier((multiplier * 2));

            timer = GameManager.instance.upgrades.upgradedDurations[GameManager.instance.player.scoreModUpgradeCount];

            Debug.Log("Timer = " + timer);

            Debug.Log(GameManager.instance.upgrades.upgradedDurations[GameManager.instance.player.scoreModUpgradeCount]);

            startCountdown = true;

            Debug.Log("The gameManager multiplier is: " + GameManager.instance.scoreMultiplier);
            Debug.Log("The previous muliplier was: " + multiplier);
        }
    }
}
