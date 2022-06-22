using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public AudioClip coinPickupSFX;
    [SerializeField] int coinScore = 100;
    void OnTriggerEnter2D(Collider2D other) 
   {
      if(other.tag == "Player")
      {
         FindObjectOfType<GameSession>().AddToScore(coinScore);
         AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
         Destroy(gameObject);
      }
   }

}
