using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMagnet : MonoBehaviour
{
    public float coinSpeed;

    private void Update()
    {
        Collider[] coins = Physics.OverlapSphere(this.transform.position, 12f);

        if (coins != null)
        {
            for(int i = 0; i < coins.Length; i++)
            {
                if(coins[i].CompareTag("Coin"))
                {
                    coins[i].transform.position = Vector3.MoveTowards(coins[i].transform.position, this.transform.position, coinSpeed * Time.deltaTime);
                }
            }
        }
    }
}
