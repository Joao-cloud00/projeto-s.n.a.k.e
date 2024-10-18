using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using static UnityEngine.Rendering.HableCurve;

public class Movemment : MonoBehaviour
{
    public float speed = 5.0f;
    public Vector2 direction = Vector2.up; //---direçao inicial
    public Transform segmentosPrefab; //-------------------------prefab do corpo da cobra
    private List<Transform> segmentosLista; //----lista do corpo da cobra
    public int initialSize = 1;//------Tamanho inicial da cobra


    void Start()
    {
        segmentosLista = new List<Transform>();
        segmentosLista.Add(this.transform);

        for (int i = 0; i < initialSize - 1; i++)
        {
            Grow();
        }

    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
        {
            direction = Vector2.right;
        }
    }

    private void Move()
    {
        //Vector2 newPosition = (Vector2)transform.position + direction * speed * Time.fixedDeltaTime;

        //transform.position = newPosition;

        //for (int i = segmentosLista.Count - 1; i >= 0; i--)
        //{
        //    segmentosLista[i].position = segmentosLista[i - 1].position;
        //}

        // Armazena a posição atual da cabeça da cobra
        Vector3 previousPosition = transform.position;

        transform.position = transform.position + new Vector3(direction.x, direction.y, 0) * speed * Time.fixedDeltaTime;

        // Move os segmentos, de trás para frente
        for (int i = 1; i < segmentosLista.Count; i++)
        {
            Vector3 tempPosition = segmentosLista[i].position;
            segmentosLista[i].position = previousPosition;

            previousPosition = tempPosition;
        }

    }

    public void Grow()
    {
        Transform lastSegment = segmentosLista[segmentosLista.Count - 1];

        // Calcular a posição "atrás" do último segmento, com base na direção oposta à direção atual
        Vector3 newPosition = lastSegment.position - new Vector3(direction.x, direction.y, 0);

        Transform newSegment = Instantiate(segmentosPrefab, newPosition, Quaternion.identity);

        segmentosLista.Add(newSegment);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Comida")
        {
            Destroy(other.gameObject);
            Grow();
        }
        else if (other.tag == "Enemy")
        {
            // Se a cobra colidir com um inimigo, você pode finalizar o jogo ou remover uma vida
            Debug.Log("Cobra colidiu com o inimigo!");
            // GameOver();
        }
    }

}
