using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{

    public Transform grabDetect;
    public Transform Holder;
    public float rayDist;
    public bool grabing;
    public int grabJustOne;

    private GameObject grabbedObject;

    void Update()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDist);

        if (grabCheck.collider != null && grabCheck.collider.tag == "graves")
        {

            if (Input.GetKey(KeyCode.Mouse1))
            {
                grabCheck.collider.gameObject.transform.parent = Holder;
                grabCheck.collider.gameObject.transform.position = Holder.position;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                //grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
                grabing = true;
            }
            else
            {
                grabCheck.collider.gameObject.transform.parent = null;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                grabing = false;
            }


        }
        Debug.DrawRay(grabDetect.position, transform.right * rayDist);
    }
       
}
   

