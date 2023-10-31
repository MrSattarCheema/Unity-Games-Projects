using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private int speed = 4;
    //[SerializeField]
    //private GameObject tripleShotContainer;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        if(transform.position.y > 7)
        {
            Destroy(this.gameObject);
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            //or we can also do this by passing tripleshotcontainer gameobject from inspector
            //Destroy(tripleShotContainer);
        }
    }
}
