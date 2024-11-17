using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class mirrorText : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        this.GetComponent<TMP_Text>().text = this.transform.parent.GetComponent<TMP_Text>().text;
    }
}
