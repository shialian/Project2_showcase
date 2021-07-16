using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField]
    private float speed;
    public Transform target;
    Rigidbody rb;
    Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 moveBy = transform.right * x + transform.forward * z;
        rb.MovePosition(transform.position + moveBy.normalized * speed * Time.deltaTime);
    }
    public void attach(){
        transform.SetParent(target,worldPositionStays:true);
        Debug.Log(new Vector3());
        position = transform.position;
        this.gameObject.transform.localPosition = new Vector3(0,5,0);
    }
    public void unattach(){
        transform.SetParent(null);
        gameObject.transform.position = position;
    }
}
