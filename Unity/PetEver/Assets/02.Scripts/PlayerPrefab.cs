using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefab : MonoBehaviour
{

    public GameObject player;

    void Awake()
    {
        Instantiate(player, this.gameObject.transform.position, this.gameObject.transform.rotation);
    }


    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
