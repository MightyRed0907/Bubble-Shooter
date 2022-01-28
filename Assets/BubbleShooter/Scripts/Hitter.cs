using UnityEngine;
using System.Collections;

public class Hitter : MonoBehaviour
{
    public SpriteRenderer spHint;
    public Sprite[] sp;
	public int kind;
    public GameObject parent;

    private bool m_hitflag = false;
    private SpriteRenderer thissprenderer;
    //public Sprite specialBubble;

    private void Awake()
    {
        thissprenderer = GetComponent<SpriteRenderer>();
        spHint = GameObject.Find("Next-hint").GetComponent<SpriteRenderer>();
        kind = (int)Random.Range(1f, 6f);
    }

    void Start()
	{
        //spHint.sprite = sp[kind-1];
        spHint.sprite = sp[6];
        thissprenderer.enabled = false;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		if (spriteRenderer != null)
		{
			Color[] colorArray = new Color[] { Color.red, Color.cyan, Color.yellow, Color.green, Color.magenta };
            //spriteRenderer.sprite = sp[kind - 1];
            spriteRenderer.sprite = sp[6];


            //         if (kind == 6)
            //{
            //	spriteRenderer.sprite = specialBubble;
            //}
            //else
            //{
            //             spriteRenderer.color = colorArray[kind - 1];
            //         }
        }
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider != null)
		{
            if (!m_hitflag && collider.tag == "bubble")
            {
                m_hitflag = true;

                CircleCollider2D selfcollider = GetComponent<CircleCollider2D>();
                if (selfcollider != null)
                    selfcollider.enabled = false;

                GridManager gridManager = parent.GetComponent<GridManager>();
                if (gridManager != null)
                {
                    GameObject newBubble = gridManager.Create(transform.position, kind, false, 0);
                    if (newBubble != null)
                    {
                        GridMember gridMember = newBubble.GetComponent<GridMember>();
                        if (gridMember != null)
                            //gridManager.Seek(gridMember.Column, -gridMember.Row, gridMember.Kind);
                            gridManager.CompareValue(gridMember.Column, -gridMember.Row, gridMember.Value);

                        /** Destory it. */
                        gridMember.state = "Block";
                    }
                }

                Launcher launcher = parent.GetComponent<Launcher>();
                if (launcher != null)
                {
                    launcher.nextBubble = null;
                    launcher.Load();
                }

                Destroy(gameObject);
            }

		}
	}
}
