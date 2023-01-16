using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefab : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject dogPrefab;
    GameObject manCharacter;
    GameObject dogCharacter;

    void Awake()
    {
        manCharacter = GameObject.FindGameObjectWithTag("Owner");
        if (manCharacter == null)
        {
            Instantiate(playerPrefab, this.gameObject.transform.position, this.gameObject.transform.rotation);
        }
        dogCharacter = GameObject.FindGameObjectWithTag("OwnerDog");
        if (dogCharacter == null)
        {
            Instantiate(dogPrefab, this.gameObject.transform.position, this.gameObject.transform.rotation);
        }
    }



    // Update is called once per frame
    void Update()
    {

    }
}
