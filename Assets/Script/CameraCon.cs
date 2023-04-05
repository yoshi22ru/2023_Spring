using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCon : MonoBehaviour
{
    public Move player;
    [SerializeField] GameObject text_coin_text;
    public GameObject target;


    void FixedUpdate()
    {
        RaycastHit hit;

        if(Physics.SphereCast(gameObject.transform.position+Vector3.down*0.5f,1.0f,target.transform.forward, out hit ,5f, LayerMask.GetMask("Default")))
        {
            if(hit.collider.gameObject.tag=="Coin")
            {
                text_coin_text.SetActive(true);
                if(Input.GetMouseButton(0))
                {
                    player.Coin += 1;
                    Destroy(hit.collider.gameObject);
                    text_coin_text.SetActive(false);
                }
            }
            else
                text_coin_text.SetActive(false);
        }
        Debug.DrawRay(gameObject.transform.position, target.transform.forward * 5, Color.blue);

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(gameObject.transform.position+target.transform.forward ,1.0f);
        }
    }
 

    
}
