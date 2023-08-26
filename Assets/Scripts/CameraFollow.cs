using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform cameraBounds;
    private Camera mainCamera;

    private float maxY;

    [SerializeField] public float FollowSpeed = 2f;
    [SerializeField] public float yOffset = 1f;
    [SerializeField] public float xOffset = 1f;
    [SerializeField] public Transform target;

    private float Xtot;
    private float Ytot;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Xtot = target.position.x + xOffset;
        Ytot = target.position.y + yOffset;
        Vector3 cameraPosition = mainCamera.transform.position;
        maxY = Mathf.Clamp(cameraPosition.y, cameraBounds.position.y, Mathf.Infinity);
        
        
        if (Ytot > maxY){
            Ytot = maxY;
            Vector3 newPos = new Vector3(Xtot, maxY, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
            print("maximo");
        } else
        {
            Vector3 newPos = new Vector3(Xtot, Ytot, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
        }
        
    }
    private void LateUpdate()
    {
        
        
        //mainCamera.transform.position = cameraPosition;

    }
}