using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetlossTest : MonoBehaviour
{
    private const int TESTNUM = 3;
    
    private int yesCnt = 0;
    private int unknownCnt = 0;
    private int noCnt = 0;

    private void OnTriggerEnter(Collider collision)
    {
        PetlossTestClass.testCnt++;
        if (this.name == "Yes") {
            yesCnt++;
        } else if (this.name == "Unknown") {
            unknownCnt++;
        } else if (this.name == "No") {
            noCnt++;
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
