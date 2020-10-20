using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Pickup
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent <PlayerController>())
        {
            GameManager.instance.AddHeart();

            if(myTile.CompareTag("Starting Tile"))
            {
                GetComponent<Renderer>().enabled = false;
                GetComponent<Collider>().enabled = false;
            }
            else
            {
                Destroy(this.gameObject);
            }
            
        }
    }
}
