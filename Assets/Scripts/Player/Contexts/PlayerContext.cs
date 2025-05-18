using UnityEngine;

public class PlayerContext : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Transform trPlayer;
    public Vector2 movePosi = Vector2.zero;
    
    public float jumpPower = 10;
    public float moveSpeed = 5;
}
