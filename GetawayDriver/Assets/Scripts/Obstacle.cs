using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public List<Renderer> myRenderer;
    public Collider myCollider;

    [SerializeField] private GameObject myTile;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {

            GameManager.instance.GameOver();



            if (myTile.CompareTag("Starting Tile"))
            {
                for(int index = 0; index < myRenderer.Count; index++)
                {
                    myRenderer[index].enabled = false;
                }

                myCollider.enabled = false;
            }
            else
            {
                Destroy(this.gameObject);
            }



        }
    }
}
