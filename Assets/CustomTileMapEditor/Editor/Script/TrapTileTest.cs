using UnityEngine;

public class TrapTileTest : MonoBehaviour
{
    [SerializeField] Vector2 startPos;
    [SerializeField] bool isTrap = false;
    [SerializeField] int distance = 0;
    private void Start()
    {
        startPos = this.transform.position;
    }
    private void Update()
    {
        if (isTrap)
        {
            this.transform.position += new Vector3(0, -0.01f, 0);
            distance++;
            if (distance > 3000)
            {
                this.transform.position = startPos;
                distance = 0;
                isTrap = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isTrap = true;
    }
}
