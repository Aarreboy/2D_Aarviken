using UnityEngine;

public class Parallax : MonoBehaviour
{
    Transform player;

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }


    void Update()
    {
        float yDistance = transform.parent.position.y - player.position.y;
        float movement = Mathf.Pow(2, yDistance) -1;
        float prevY = transform.position.y;
        transform.position =   transform.parent.position - transform.parent.InverseTransformPoint(player.position) * movement;
        transform.position = new Vector3(transform.position.x, prevY, transform.position.z);

    }
}
