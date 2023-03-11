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
        prefabName = "Prefabs/" + dogSpecies[1];
        dogNPC = Resources.Load<GameObject>(prefabName);

        dogRandomScale = Random.Range(0.5f, 1.0f);
        dogScale = new Vector3(1,1,1)*dogRandomScale;

        dogNPC.transform.localScale = dogScale;

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
