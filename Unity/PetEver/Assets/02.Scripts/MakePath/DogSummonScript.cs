using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSummonScript : MonoBehaviour
{
    public GameObject dogNPC;
    private string[] dogSpecies;
    private string prefabName;
    private Vector3 dogScale;
    public static float dogRandomScale; // use dogNPC scale in DogSummonScript
                                        // use dogWalkingAnimationSpeed in DogAI 

    // Start is called before the first frame update
    void Awake()
    {

        dogSpecies = gameObject.name.Split('_');


        if (dogSpecies[1] == "Bichon")
            dogRandomScale = Random.Range(0.85f, 1.0f);
        else if(dogSpecies[1] == "PomeLong")
            dogRandomScale = Random.Range(0.75f, 1.0f);
        else if(dogSpecies[1] == "Pomeshort")
            dogRandomScale = Random.Range(0.75f, 1.0f);       
        else if(dogSpecies[1] == "Maltese")
            dogRandomScale = Random.Range(0.75f, 1.0f);
        else if(dogSpecies[1] == "Poodle")
            dogRandomScale = Random.Range(0.85f, 1.0f);


        dogScale = new Vector3(1,1,1)*dogRandomScale;

        dogNPC.transform.localScale = dogScale;
        dogNPC.tag = "DogNPC";
        dogNPC.layer = 0;

        Instantiate(dogNPC, this.gameObject.transform.position, this.gameObject.transform.rotation);
    }

    void Start()
    {

    }

}
