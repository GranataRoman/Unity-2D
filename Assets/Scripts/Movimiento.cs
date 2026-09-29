using JetBrains.Annotations;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;



public enum TipoPersonaje
{
    Caballero,
    Elfo
}
public class Movimiento : MonoBehaviour
{
    public float vida = 100f;
    private bool estoyVivo = true;
    private int puntaje = 0;
    private float GolpeCaballero = 10f;
    public string nombre = "Caballero";
    private float movimientoHorizontal = 0f;
    private bool enSuelo = false;
    private bool salto = false;
    private float fuerzaSalto = 5f;
    private Vector2 mov = new Vector2(0, 0);
    private Animator animator;

    [SerializeField] private LayerMask tierraFirme;
    [SerializeField] private Transform detectorSuelo; // Punto en los pies del personaje
    [SerializeField] private float radioDeteccion = 0.2f;

    [SerializeField] private float velocidadNormal = 5f;
    // para correr [serializeField] private float velocidadSprint = 10f;
    [SerializeField] private string triggerGolpe = "GolpeCaballero";
    [SerializeField] private TipoPersonaje personaje ;


    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
 
    }


    void Update()
    {
        // comprueba si esta en el suelo
        enSuelo = Physics2D.OverlapCircle(detectorSuelo.position, radioDeteccion, tierraFirme);

        if (personaje == TipoPersonaje.Caballero)
            MovimientoCaballero();
        else
            MovimientoElfo();
    }

        void MovimientoCaballero() 
    {

        if (Input.GetKeyDown(KeyCode.W) && enSuelo)
            salto = true;

        if (Input.GetKey(KeyCode.D))
        {
            sprite.flipX = false;
            movimientoHorizontal = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            sprite.flipX = true;
            movimientoHorizontal = -1;
        }
        else
        {
            movimientoHorizontal = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
            DarGolpe();
        
    }


    void MovimientoElfo()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow) && enSuelo)
            salto = true;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            sprite.flipX = false;
            movimientoHorizontal = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            sprite.flipX = true;
            movimientoHorizontal = -1;
        }
        else
        {
            movimientoHorizontal = 0;
        }

        if (Input.GetKeyDown(KeyCode.RightControl))
            DarGolpe();

    }

    void FixedUpdate()
    {
        float movimientoVertical = rb.linearVelocity.y;

        if (salto)
        {
            movimientoVertical = fuerzaSalto;
            salto = false;
        }
        // Aplica el movimiento en la física (Eje X) respetando la caída por gravedad (Eje Y)
     
        rb.linearVelocity = new Vector2(movimientoHorizontal * velocidadNormal,movimientoVertical);


    }
    private void OnDrawGizmosSelected()
    {
        if (detectorSuelo != null)
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(detectorSuelo.position, radioDeteccion);
    }


    public void Estado()
    {
        

    }

    void DarGolpe()
    {
        if (animator != null)

        if (personaje == TipoPersonaje.Caballero)
           animator.SetTrigger("GolpeCaballero");
        else
           animator.SetTrigger("GolpeElfo");
    }











}

