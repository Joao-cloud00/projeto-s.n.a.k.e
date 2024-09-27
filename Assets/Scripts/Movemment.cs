using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class Movemment : MonoBehaviour
{
    public float speed = 5.0f;
    public Vector2 direction = Vector2.up; //---direçao inicial
    public Transform segmentosPrefab; //-------------------------prefab do corpo da cobra
    private List<Transform> segmentosLista; //----lista do corpo da cobra
    

    void Start()
    {
        segmentosLista = new List<Transform>();
        segmentosLista.Add(this.transform);

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
        Vector2 newPosition = (Vector2)transform.position + direction * speed * Time.fixedDeltaTime;

        transform.position = newPosition;

        for (int i = segmentosLista.Count - 1; i >= 0; i--)
        {
            segmentosLista[i].position = segmentosLista[i - 1].position;
        }
        
    }

    public void Grow()
    {
        Transform segment = Instantiate(segmentosPrefab);
        segment.position = segmentosLista[segmentosLista.Count - 1].position;
        segmentosLista.Add(segment);
    }

}
