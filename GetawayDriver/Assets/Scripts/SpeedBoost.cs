using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : Pickup
{
    [SerializeField] private float boost;
    [SerializeField] private float duration;

    private float timer;

    private bool countDown;

    private PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            player = other.GetComponent<PlayerController>();

            player.zSpeed *= boost;

            timer = duration;
            countDown = true;

            this.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    protected override void Update()
    {
        if(countDown == true)
        {
            timer -= Time.deltaTime;
        }

        if(timer < 0)
        {
            if(player != null)
            {
                player.zSpeed /= boost;
                countDown = false;
                timer = duration;
                Destroy(this.gameObject);
            }
        }

        base.Update();
    }
}
