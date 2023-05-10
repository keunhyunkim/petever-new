using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefab : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject dogPrefab;
    GameObject manCharacter;
    GameObject dogCharacter;

    private string[] objectName;

    void Awake()
    {
        objectName = gameObject.name.Split('_');


        if (objectName[1]=="Man")
        {
            manCharacter = GameObject.FindGameObjectWithTag("Owner");
            if (manCharacter == null)
            {
                Instantiate(playerPrefab, this.gameObject.transform.position, this.gameObject.transform.rotation);
            }
        }


        else if(objectName[1]=="Dog")
        {
            dogCharacter = GameObject.FindGameObjectWithTag("OwnerDog");
            dogCharacter.transform.position = this.gameObject.transform.position;
            dogCharacter.transform.localScale = new Vector3(1f, 1f, 1f);
            if (dogCharacter == null)
            {
                Instantiate(dogPrefab, this.gameObject.transform.position, this.gameObject.transform.rotation);
            }
        }
    }




}
