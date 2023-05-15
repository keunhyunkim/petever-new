using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySpaceScript : MonoBehaviour
{
    private GameObject dogModel;
    // Start is called before the first frame update
    void Start()
    {
        dogModel = GameObject.FindGameObjectWithTag("OwnerDog");
        if (dogModel != null)
        {
            ChangeLayersRecursively(dogModel.transform, "Default");
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

}
