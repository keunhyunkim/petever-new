using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManNPCController : MonoBehaviour
{

    private GameObject owner;
    public GetColliderScript getCollider;

    // Start is called before the first frame update
    void Start()
    {
        getCollider = gameObject.GetComponent<GetColliderScript>(); // get collider data from 'GetColliderScript' class 
        StartCoroutine(ManNPC());

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator ManNPC()
    {
        while(true)
        {
            if (getCollider.collided_tag == "Owner")
            {
                owner = getCollider.collided_object;
                LookOwner(owner);
            }





            yield return new WaitForSeconds(0.25f);
        }
    }




        void LookOwner(GameObject owner)
    {
        if (owner != null)
        {
            Vector3 dir = (owner.transform.position-gameObject.transform.position);
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.LookRotation(dir),Time.deltaTime*50f);
        }
    }


}
