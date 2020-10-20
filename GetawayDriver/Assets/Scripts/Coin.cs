using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Pickup
{
    private void OnTriggerEnter(Collider other)
    {
        //if the player collided with me
        if(other.gameObject.GetComponent<PlayerController>())
        {
            //play my sound effect
            audioSource.Play();

            //give the player a coin
            GameManager.instance.AddCoin();
            GetComponent<Renderer>().enabled = false;

            if (myTile.CompareTag("Starting Tile"))
            {
                GetComponent<Collider>().enabled = false;
            }
            else
            {
                //Then destroy me
                Destroy(this.gameObject, .2f);
            }
            

            
        }
    }
}
