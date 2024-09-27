using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Tiro_Cobra : MonoBehaviour
{
    public GameObject tiroPrefab;
    public float tiroSpeed = 10f;
    private Movemment snakeMovement;

    void Start()
    {
        snakeMovement = GetComponent<Movemment>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Atirar();
        }
    }

    void Atirar()
    {
        GameObject tiro = Instantiate(tiroPrefab, transform.position, Quaternion.identity);

        Vector2 direction = snakeMovement.direction;

        tiro.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(direction.x, direction.y) * tiroSpeed;
    }
}
