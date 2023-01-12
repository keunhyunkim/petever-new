using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefab : MonoBehaviour
{

    public GameObject player;
    public GameObject manCharacter;

    void Awake()
    {
        manCharacter = GameObject.FindGameObjectWithTag("Owner");
        if (manCharacter == null)
        {
            Instantiate(player, this.gameObject.transform.position, this.gameObject.transform.rotation);
        }

    }



    // Update is called once per frame
    void Update()
    {

    }
}
