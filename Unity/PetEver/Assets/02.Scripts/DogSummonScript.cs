using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSummonScript : MonoBehaviour
{
    public GameObject dogNPC;

    // Start is called before the first frame update
    void Awake()
    {
        dogNPC = Resources.Load<GameObject>("Prefabs/Bichon");
        Instantiate(dogNPC, this.gameObject.transform.position, this.gameObject.transform.rotation);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
