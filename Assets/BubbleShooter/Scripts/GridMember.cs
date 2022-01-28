using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GridMember : MonoBehaviour
{
    //public GameObject parent;
    public Sprite[] sp;
	public GameObject efxPrefabBlock;
    public GameObject efxPrefabPop;
	public int Row;
	public int Column;
	public int Kind;
	public string state;


	int _value;
	public int Value
    {
		get { return _value; }
		set 
		{
			_value = value;
			valueTxt.text = _value.ToString();
		}
    }

	[SerializeField]
	Text valueTxt;

	public const float POP_SPEED = 0.9f;
	public const float EXPLODE_SPEED = 5f;
	public float KILL_Y = -20f;

    private bool efxFlag = false;

    public void Start()
    {
		KILL_Y = -20f - PlayerPrefs.GetInt("difficulty") * 2;
    }
    public void Update()
	{
		if (state == "Block")
		{
			/** Destory the bubble. */
			CircleCollider2D cc = GetComponent<CircleCollider2D>();
			if (cc != null)
				cc.enabled = false;

			if (!efxFlag)
			{
				efxFlag = true;
				GameObject go = Instantiate(efxPrefabBlock);
				go.transform.position = transform.position;
			}

			transform.localScale = transform.localScale * POP_SPEED;
			if (transform.localScale.sqrMagnitude < 0.05f)
				Destroy(gameObject);
		}
		else if (state == "Pop")
		{
			CircleCollider2D cc = GetComponent<CircleCollider2D>();
			if (cc != null)
				cc.enabled = false;

            if (!efxFlag)
            {
                efxFlag = true;
                GameObject go = Instantiate(efxPrefabPop);
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
