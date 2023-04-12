using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakFloor : MonoBehaviour
{
    [SerializeField] GameObject floor;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Player") 
        {
            StartCoroutine(Break());
        }
    }

    IEnumerator Break()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);

    }
}
