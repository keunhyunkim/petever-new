using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetlossTest : MonoBehaviour
{
    private const int TESTNUM = 3;
    
    private void OnTriggerEnter(Collider collision)
    {
        PetlossTestClass.testCnt++;
        if (this.name == "Yes") {
            PetlossTestClass.yesCnt++;
        } else if (this.name == "Unknown") {
            PetlossTestClass.unknownCnt++;
        } else if (this.name == "No") {
            PetlossTestClass.noCnt++;
        }

        if (PetlossTestClass.testCnt == TESTNUM) {
            // All Tests are done.
            GameObject petlossTestObj = GameObject.FindGameObjectWithTag("PetLossTest");
            petlossTestObj.SetActive(false);
            PetlossTestClass.testCnt = 0;
        } else {
            collision.transform.position = GameObject.Find("TestPosition").transform.position;
        }

        
    }
}
