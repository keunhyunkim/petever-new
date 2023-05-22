using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerPrefab : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject dogPrefab;
    GameObject manCharacter;
    GameObject dogCharacter;
    private UnityEngine.AI.NavMeshAgent nav;

    private string[] objectName;

    void Start()
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

            nav = dogCharacter.GetComponent<NavMeshAgent>();
            nav.enabled = false;
            dogCharacter.transform.position = this.gameObject.transform.position;
            dogCharacter.transform.localScale = new Vector3(1f, 1f, 1f);
            nav.enabled = true;


            if (dogCharacter == null)
            {
                Instantiate(dogCharacter, this.gameObject.transform.position, this.gameObject.transform.rotation);
            }
        }
    }




}
