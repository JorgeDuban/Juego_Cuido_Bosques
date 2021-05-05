using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class esfera : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject esfe;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void OnTriggerExit(Collider other)
    {

        if (other.gameObject.name == "personaje")
        {
            Destroy(esfe.gameObject);


        }
    }

}