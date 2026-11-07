using System.Numerics;
using UnityEngine;

public class strickerscript : MonoBehaviour
{
        [SerializeField] public float speed = 1;
        private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }    
    void Start()
    {
        rb.linearVelocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
