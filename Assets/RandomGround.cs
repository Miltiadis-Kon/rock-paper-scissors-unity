using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGround : MonoBehaviour
{
    public List<Material> groundList;
    private int rnd;
    // Start is called before the first frame update
    void Start()
    {
     rnd = Random.Range(0, groundList.Count);
        GetComponent<Renderer>().material = groundList[rnd];   
    }
}
