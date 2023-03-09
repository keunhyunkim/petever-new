using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSummonScript : MonoBehaviour
{
    public GameObject dogNPC;
    private string[] dogSpecies;
    private string prefabName;

    // Start is called before the first frame update
    void Awake()
    {

        dogSpecies = gameObject.name.Split('_');
        prefabName = "Prefabs/" + dogSpecies[1];
        dogNPC = Resources.Load<GameObject>(prefabName);

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
