using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class GridManager : MonoBehaviour
{
    [Header("Grid")]
    public int columns;
    public int rows;
    public GameObject bubble;
    public Transform group;
	public int BubbleTypes = 5;
    public Vector3 initialPos;    
    [Range(0, 1)]
    public float gap;

    [Header("Game")]
    public GameObject youWin;
	//public GameObject youLose;
    public string GridData = "Assets/Data/level1.data";

    [Header("Fall down controller")]
    public Transform Compressor;

    private GameObject[,] grid;

    private void Awake()
    {
		/** Set position of bubble group */
		Compressor.Translate(new Vector3(0, initialPos.y - Compressor.position.y, 0));
	}

    private void Start()
	{
		grid = new GameObject[columns, rows];

		string level = LoadLevelInfo();
        ArragementBubble(level);
	}

    private void Update()
    {
        Compressor.position = new Vector3(Compressor.position.x, initialPos.y, Compressor.position.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(initialPos.x+2f, initialPos.y, 0), new Vector3(5, .1f, 1));
    }

    public Vector3 Snap(Vector3 position)
	{
		Vector3 objectOffset = position - initialPos;
		Vector3 objectSnap = new Vector3(
			Mathf.Round(objectOffset.x / gap),
			Mathf.Round(objectOffset.y / gap),
			0f
		);

		if ((int)objectSnap.y % 2 != 0)
		{
			if (objectOffset.x > objectSnap.x * gap)			
				objectSnap.x += 0.5f;
			
			else			
				objectSnap.x -= 0.5f;			
		}
		return initialPos + objectSnap * gap;
	}

    public GameObject Create(Vector2 position, int kind)
	{
		Vector3 snappedPosition = Snap(position);
		int row = (int)Mathf.Round((snappedPosition.y - initialPos.y) / gap);
		int column = 0;
		if (row % 2 != 0)		
			column = (int)Mathf.Round((snappedPosition.x - initialPos.x) / gap - 0.5f);		
		else		
			column = (int)Mathf.Round((snappedPosition.x - initialPos.x) / gap);		


		GameObject bubbleClone = (GameObject)Instantiate(bubble, snappedPosition, Quaternion.identity);
        bubbleClone.transform.parent = group;
        try
		{
			grid[column, -row] = bubbleClone;
		}
		catch (System.IndexOutOfRangeException)
		{
			Destroy(bubbleClone);
			return null;
		}

		CircleCollider2D collider = bubbleClone.GetComponent<CircleCollider2D>();
		if (collider != null)
		{
			collider.isTrigger = true;
		}

		GridMember gridMember = bubbleClone.GetComponent<GridMember>();
		if (gridMember != null)
		{

			//gridMember.parent = gameObject;
			gridMember.row = row;
			gridMember.column = column;
			if (kind == 6)
			{
				gridMember.kind = (int)Random.Range(1f, 6f);
			}
			else
			{
				gridMember.kind = kind;
			}

			SpriteRenderer spriteRenderer = bubbleClone.GetComponent<SpriteRenderer>();
			if (spriteRenderer != null)			
                spriteRenderer.sprite = gridMember.sp[gridMember.kind - 1];
		}
		bubbleClone.SetActive(true);

		//if (column == 6 && row == -6 && youLose != null)
		//	youLose.SetActive(true);

		try
		{
			grid[column, -row] = bubbleClone;
		}
		catch (System.IndexOutOfRangeException)	{}

		return bubbleClone;
	}


	public void Seek(int column, int row, int kind)
	{
		int[] pair = new int[2] { column, row };

		bool[,] visited = new bool[columns, rows];

		visited[column, row] = true;

		int[] deltax = { -1, 0, -1, 0, -1, 1 };
		int[] deltaxprime = { 1, 0, 1, 0, -1, 1 };
		int[] deltay = { -1, -1, 1, 1, 0, 0 };


		Queue<int[]> queue = new Queue<int[]>();
		Queue<GameObject> objectQueue = new Queue<GameObject>();

		queue.Enqueue(pair);

		int count = 0;
		while (queue.Count != 0)
		{
			int[] top = queue.Dequeue();
			GameObject gtop = grid[top[0], top[1]];
			if (gtop != null)
			{
				objectQueue.Enqueue(gtop);
			}
			count += 1;
			for (int i = 0; i < 6; i++)
			{
				int[] neighbor = new int[2];
				if (top[1] % 2 == 0)				
					neighbor[0] = top[0] + deltax[i];				
				else				
					neighbor[0] = top[0] + deltaxprime[i];
				
				neighbor[1] = top[1] + deltay[i];
				try
				{
					GameObject g = grid[neighbor[0], neighbor[1]];
					if (g != null)
					{
						GridMember gridMember = g.GetComponent<GridMember>();
						if (gridMember != null && gridMember.kind == kind)
						{
							if (!visited[neighbor[0], neighbor[1]])
							{
								visited[neighbor[0], neighbor[1]] = true;
								queue.Enqueue(neighbor);
							}
						}
					}
				}
				catch (System.IndexOutOfRangeException)	{}
			}
		}
		if (count >= 3)
		{
			while (objectQueue.Count != 0)
			{
				GameObject g = objectQueue.Dequeue();

				GridMember gm = g.GetComponent<GridMember>();
				if (gm != null)
				{
					grid[gm.column, -gm.row] = null;
					gm.state = "Pop";
				}
			}

			AudioSource audioSource = GetComponent<AudioSource>();
			if (audioSource != null)
				audioSource.Play();
		}
		CheckCeiling(0);
	}


	public void CheckCeiling(int ceiling)
	{

		bool[,] visited = new bool[columns, rows];

		Queue<int[]> queue = new Queue<int[]>();

		int[] deltax = { -1, 0, -1, 0, -1, 1 };
		int[] deltaxprime = { 1, 0, 1, 0, -1, 1 };
		int[] deltay = { -1, -1, 1, 1, 0, 0 };

		for (int i = 0; i < columns; i++)
		{
			int[] pair = new int[2] { i, ceiling };
			if (grid[i, ceiling] != null)
			{
				visited[i, ceiling] = true;
				queue.Enqueue(pair);
			}
		}

		int count = 0;
		while (queue.Count != 0)
		{
			int[] top = queue.Dequeue();
			count += 1;
			for (int i = 0; i < 6; i++)
			{
				int[] neighbor = new int[2];
				if (top[1] % 2 == 0)
				{
					neighbor[0] = top[0] + deltax[i];
				}
				else
				{
					neighbor[0] = top[0] + deltaxprime[i];
				}
				neighbor[1] = top[1] + deltay[i];
				try
				{
					GameObject g = grid[neighbor[0], neighbor[1]];
					if (g != null)
					{
						if (!visited[neighbor[0], neighbor[1]])
						{
							visited[neighbor[0], neighbor[1]] = true;
							queue.Enqueue(neighbor);
						}
					}
				}
				catch (System.IndexOutOfRangeException)
				{
				}
			}
		}

		if (count == 0)
		{
			if (youWin != null)
				youWin.SetActive(true);
		}

		for (int r = 0; r < rows; r++)
		{
			for (int c = 0; c < columns; c++)
			{
				if (grid[c, r] != null && !visited[c, r])
				{
					GameObject g = grid[c, r];
					GridMember gm = g.GetComponent<GridMember>();
					if (gm != null)
					{
						grid[gm.column, -gm.row] = null;
						gm.state = "Explode";
					}
				}
			}
		}

	}

    #region pirvate function 
    private string LoadLevelInfo()
    {
		//using (StreamReader r = new StreamReader(GridData))
		//{
		//    string json = r.ReadToEnd();
		//    return json;
		//}

		string levelData = "";

        for (int i = 0; i < rows; i++)
        {
			int columnNum = 6;
			if (i % 2 == 0) columnNum++;

            for (int j = 0; j < columnNum; j++)
            {
				levelData += ((i % BubbleTypes) + 1).ToString();
            }

			levelData += "\n";
        }

		return levelData;
    }

    private void ArragementBubble(string level)
    {
        int levelpos = 0;
        for (int r = 0; r < rows; r++)
        {
            if (r % 2 != 0) columns -= 1;
            for (int c = 0; c < columns; c++)
            {
                Vector3 position = new Vector3((float)c * gap, (float)(-r) * gap, 0f) + initialPos;
                if (r % 2 != 0)
                    position.x += 0.5f * gap;

                int newKind = 0;

                if (level.Length <= levelpos)
                {
                    continue;
                }
                while (level[levelpos] == '\r' || level[levelpos] == '\n')
                {
                    levelpos++;
                }

                if (level[levelpos] == '0')
                {
                    levelpos++;
                    continue;
                }
                if (level[levelpos] == '1')
                    newKind = 1;

                if (level[levelpos] == '2')
                    newKind = 2;

                if (level[levelpos] == '3')
                    newKind = 3;

                if (level[levelpos] == '4')
                    newKind = 4;

                if (level[levelpos] == '5')
                    newKind = 5;

                Create(position, newKind);
                levelpos++;
            }
            if (r % 2 != 0) columns += 1;
        }
    }

    #endregion
}
