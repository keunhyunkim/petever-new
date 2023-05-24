using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MySpaceScript : MonoBehaviour
{
    private GameObject dogModel;
    //public GameObject dogInteraction;
    public GameObject DogCamera;


    // Start is called before the first frame update
    void Start()
    {
        dogModel = GameObject.FindGameObjectWithTag("OwnerDog");

        if (dogModel != null)
        {
            ChangeLayersRecursively(dogModel.transform, "TransparentFX");
            dogModel.transform.position = new Vector3(374, 573, 18);
        }
    }

    
    private void ChangeLayersRecursively(Transform trans, string name)
    {
        trans.gameObject.layer = LayerMask.NameToLayer(name);
        foreach (Transform child in trans)
        {
            ChangeLayersRecursively(child, name);
        }
    }


    public void DogBtn()
    {
        gameObject.GetComponent<DogInteraction>().UniqueAction();
    }

}
