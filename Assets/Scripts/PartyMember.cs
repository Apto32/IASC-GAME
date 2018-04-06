using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMember : MonoBehaviour {

    public GameObject target;
    public List<Vector3> positions;
    public int distance_permitted;
    public float speed;
    private Vector3 lastLeaderPosition;
    private Animator anim;
    private bool playerMoving;
    private Vector2 lastMove;
    // Use this for initialization
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        positions.Add(target.transform.position);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerMoving = false;
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            playerMoving = true;
            lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        }
        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        {
            playerMoving = true;
            lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
        }
        if (lastLeaderPosition != positions[positions.Count - 1])
        {
            positions.Add(target.transform.position);
        }

        if (positions.Count >= distance_permitted)
        {
            if (gameObject.transform.position != positions[0])
            {

                transform.position = Vector3.MoveTowards(transform.position, positions[0], Time.deltaTime * speed);

            }
            else
            {
                positions.Remove(positions[0]);
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                transform.position = Vector3.MoveTowards(transform.position, positions[0], Time.deltaTime * speed);

            }
        }
        lastLeaderPosition = target.transform.position;
        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
    }
}
