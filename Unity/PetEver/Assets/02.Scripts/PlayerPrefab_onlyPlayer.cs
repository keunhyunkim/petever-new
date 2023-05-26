using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefab_onlyPlayer: MonoBehaviour
{

    public GameObject playerPrefab;
    GameObject manCharacter;
    GameObject dogCharacter;

    void Awake()
    {
        manCharacter = GameObject.FindGameObjectWithTag("Owner");
        if (manCharacter == null)
        {
            Instantiate(playerPrefab, this.gameObject.transform.position, this.gameObject.transform.rotation);
        }
    }




}
