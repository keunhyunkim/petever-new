using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManSummonScript : MonoBehaviour
{
    public GameObject manNPC;
    private string[] manColor;
    private string prefabName;

    // Start is called before the first frame update
    void Awake()
    {

        manColor = gameObject.name.Split('_');
        prefabName = "Prefabs/" + manColor[1];
        manNPC = Resources.Load<GameObject>(prefabName);

        Instantiate(manNPC, this.gameObject.transform.position, this.gameObject.transform.rotation);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
