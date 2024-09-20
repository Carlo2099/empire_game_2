using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Movimiento : MonoBehaviour
{
    public AudioClip SoundSalto;

    private Rigidbody2D rigidbody2;
    private float horizontal;
    private Animator animator;
    private bool enSuelo = true;
    public GameObject textoFlotante;

    public GameManager gameManager;

    public bool sePuedeMover = true;
    [SerializeField] private Vector2 velRebote;


    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal > 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (horizontal < 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        animator.SetBool("corriendo", horizontal != 0.0f);
        if (sePuedeMover)
        {
            if (Input.GetKeyDown(KeyCode.W) && enSuelo)
            {
                Jump();
            }
        }          
    }


    private void Jump()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(SoundSalto);
        rigidbody2.velocity = new Vector2(rigidbody2.velocity.x, 0);
        rigidbody2.AddForce(Vector2.up * 400);
        enSuelo = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Suelo") || collision.gameObject.layer == LayerMask.NameToLayer("plataforma"))
        {
            enSuelo = true; 
        }
    }


    private void FixedUpdate()
    {
        if (sePuedeMover)
        {
            moverizqDrc();
        }
        
    }
    public void Rebote(Vector2 puntoGolpe)
    {
        float direccionX = (horizontal > 0.0f) ? 1.0f : -1.0f;
        rigidbody2.velocity = new Vector2(velRebote.x * puntoGolpe.x * direccionX, velRebote.y);
    }

    private void moverizqDrc()
    {
        rigidbody2.velocity = new Vector2(horizontal * 4, rigidbody2.velocity.y);
    }

    public void MostrarTexto()
    {
        GameObject texto = Instantiate(textoFlotante, this.transform);
    }

    
}