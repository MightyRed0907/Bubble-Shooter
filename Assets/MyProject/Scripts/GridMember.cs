using UnityEngine;
using System.Collections;

public class GridMember : MonoBehaviour
{
    //public GameObject parent;
    public Sprite[] sp;
    public GameObject efxPrefab;
	public int row;
	public int column;
	public int kind;
	public string state;

	public const float POP_SPEED = 0.9f;
	public const float EXPLODE_SPEED = 5f;
	public const float KILL_Y = -30f;

    private bool efxFlag = false;

	public void Update()
	{
		if (state == "Pop")
		{
			CircleCollider2D cc = GetComponent<CircleCollider2D>();
			if (cc != null)
				cc.enabled = false;

            if (!efxFlag)
            {
                efxFlag = true;
                GameObject go = Instantiate(efxPrefab);
                go.transform.position = transform.position;
            }

            transform.localScale = transform.localScale * POP_SPEED;
			if (transform.localScale.sqrMagnitude < 0.05f)	
				Destroy(gameObject);
		}
		else if (state == "Explode")
		{
			CircleCollider2D cc = GetComponent<CircleCollider2D>();
			if (cc != null)
				cc.enabled = false;

			Rigidbody2D rb = GetComponent<Rigidbody2D>();
			if (rb != null)
			{
				rb.gravityScale = 1f;
				rb.velocity = new Vector3(
					Random.Range(-EXPLODE_SPEED, EXPLODE_SPEED),
					Random.Range(-EXPLODE_SPEED, EXPLODE_SPEED),
					0f
				);
			}
			state = "Fall";
		}
		else if (state == "Fall")
		{
			if (transform.position.y < KILL_Y)			
				Destroy(gameObject);			
		}
	}

}
