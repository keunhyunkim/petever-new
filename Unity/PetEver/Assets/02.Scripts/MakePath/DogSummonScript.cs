using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSummonScript : MonoBehaviour
{
    public GameObject dogNPC;
    void Awake()
    {
        dogNPC.tag = "DogNPC";
        dogNPC.layer = 0;

        Instantiate(dogNPC, this.gameObject.transform.position, this.gameObject.transform.rotation);
        
    }


}
