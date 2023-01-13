using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetColliderScript : MonoBehaviour
{
    private float colliderRadius = 5f;
    public enum Sorting { Owner = 0, Flower, Butterfly, Dog }; //list of GameObjects' name that dog can collide

    public string collided_tag { get; private set; } // 
    public GameObject collided_object { get; private set; } // 


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetCollider(out string sorting, out GameObject gameobject);
        collided_tag = sorting;
        collided_object = gameobject;
    }

    public void GetCollider(out string sorting, out GameObject gameobject)
    {
        sorting = null;
        gameobject = null;

        Collider[] colliders = Physics.OverlapSphere(this.gameObject.transform.position, colliderRadius);

        foreach (Collider coll in colliders)
        {
            if (System.Enum.IsDefined(typeof(Sorting), coll.tag))
            {
                sorting = coll.tag;
                gameobject = coll.gameObject;
             }
        }
    }
}
