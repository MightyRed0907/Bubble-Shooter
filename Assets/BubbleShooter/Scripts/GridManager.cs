using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class GridManager : Singleton<GridManager>
{
    [Header("Grid")]
    public int Columns = 6;
    public int Rows = 10;
	public int BubbleSpawnRows = 10;

	public GameObject Bubble;
    public Transform Group;
	public int BubbleTypes = 5;
	
    public Vector3 InitialPos = new Vector3(-1.65f, 1, 0);
	
    [Range(0, 1)]
    public float Gap = 0.65f;

    [Header("Game")]
    public GameObject YouWin;
	//public GameObject youLose;
    public string GridData = "Assets/Data/level1.data";

    [Header("Fall down controller")]
    public Transform Compressor;

    GameObject[,] Grid;

	
	private int[] left, right, answer;

	private char[] oper;
 
	private int count = 9;
	[SerializeField]
	private Text question;


    private void Awake()
    {
		/** Set position of bubble group */
		Compressor.Translate(new Vector3(0, InitialPos.y - Compressor.position.y, 0));

		//if (Rows < BubbleSpawnRows + 10) Rows = BubbleSpawnRows + 10;
	}

    private void Start()
	{
		//Application.targetFrameRate = 10;
		left = new int[Rows];
		right = new int[Rows];
		answer = new int[Rows];
		oper = new char[Rows];
		CreateProblem();
		question.text = $"{left[count]} {oper[count]} {right[count]}";
		Grid = new GameObject[Columns, Rows];

		string level = LoadLevelInfo();
        ArragementBubble(level);
		//Group.transform.position = new Vector3(0, 2.08f - (PlayerPrefs.GetInt("difficulty") - 1) * Gap * 1.4f, 0);
	}

	private void CreateProblem()
    {
		string temp = PlayerPrefs.GetString("level");
		char[] op = new char[temp.Length];
		op = temp.ToCharArray();
		for (int i = 0; i < 10; i++)
        {
			//left[i] = Random.Range(2, 9);
			//right[i] = Random.Range(left[i] - 5 < 1 ? 1 : left[i] - 5, left[i]);
			oper[i] = op[Random.Range(0, op.Length)];
			switch(oper[i])
            {
				case '+':
					left[i] = Random.Range(1, 99);
					right[i] = Random.Range(1, 100 - left[i]);
					answer[i] = left[i] + right[i];
					break;
				case '-':
					right[i] = Random.Range(1, 99);
					left[i] = Random.Range(right[i] + 1, 100);
					answer[i] = left[i] - right[i];
					break;
				case 'x':
					left[i] = Random.Range(2, 51);
					right[i] = Random.Range(2, 100 / left[i]);
					answer[i] = left[i] * right[i];
					break;
				case '÷':
					answer[i] = Random.Range(2, 51);
					right[i] = Random.Range(2, 100 / answer[i]);
					left[i] = right[i] * answer[i];
					break;
			}
		}
		
		
	}

    private void Update()
    {
        Compressor.position = new Vector3(Compressor.position.x, InitialPos.y , Compressor.position.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(InitialPos.x+2f, InitialPos.y, 0), new Vector3(5, .1f, 1));
    }

    public Vector3 Snap(Vector3 position)
	{
		Vector3 objectOffset = position - InitialPos;
		Vector3 objectSnap = new Vector3(
			Mathf.Round(objectOffset.x / Gap),
			Mathf.Round(objectOffset.y / Gap),
			0f
		);

		if ((int)objectSnap.y % 2 != 0)
		{
			if (objectOffset.x > objectSnap.x * Gap)			
				objectSnap.x += 0.5f;
			
			else			
				objectSnap.x -= 0.5f;			
		}
		return InitialPos + objectSnap * Gap;
	}

    public GameObject Create(Vector2 position, int kind, bool isAnswer, int val)
	{
		
		Vector3 snappedPosition = Snap(position);
		int row = (int)Mathf.Round((snappedPosition.y - InitialPos.y) / Gap);
		int column = 0;
		if (row % 2 != 0)		
			column = (int)Mathf.Round((snappedPosition.x - InitialPos.x) / Gap - 0.5f);		
		else		
			column = (int)Mathf.Round((snappedPosition.x - InitialPos.x) / Gap);		


		GameObject bubbleClone = (GameObject)Instantiate(Bubble, snappedPosition, Quaternion.identity);
        bubbleClone.transform.parent = Group;
        try
		{
			Grid[column, -row] = bubbleClone;
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
			gridMember.Row = row;
			gridMember.Column = column;
			if (kind == 6)
			{
				gridMember.Kind = (int)Random.Range(1f, 6f);
			}
			else
			{
				gridMember.Kind = kind;
			}

			SpriteRenderer spriteRenderer = bubbleClone.GetComponent<SpriteRenderer>();
			if (spriteRenderer != null)			
                spriteRenderer.sprite = gridMember.sp[gridMember.Kind - 1];

			// Set value
			if (isAnswer)
			{
				gridMember.Value = val;
			}
			else
			{
				int temp;
				if (Random.Range(0, 2) == 1)
                {
					temp = val + Random.Range(1, 11);
					gridMember.Value = temp >= 100 ? 99 : temp;
				}
				else
				{
					temp = val + Random.Range(-10, 0);
					gridMember.Value = temp <= 0 ? 1 : temp;
				}
			}
		}
		bubbleClone.SetActive(true);

		//if (column == 6 && row == -6 && youLose != null)
		//	youLose.SetActive(true);

		try
		{
			Grid[column, -row] = bubbleClone;
		}
		catch (System.IndexOutOfRangeException)	{}

		return bubbleClone;
	}

	/*** Get the count of members in a row. */
	int GetMemberCountInRow(int row)
	{ 
		int i = 0;
		try { while (Grid[i, row] != null) i++; }
		catch { }
		return i;
    }

	/*** Get the upper members of new spawned member. */
	List<GameObject> GetUpperMember(int column, int row)
    {
		List<GameObject> upperMembers = new List<GameObject>();
		try
		{
			int i = 0;
			while (i < Columns)
			{
				if (Vector3.Distance(Grid[column, row].transform.position, Grid[i, row - 1].transform.position) < 1f)
					upperMembers.Add(Grid[i, row - 1]);
				i++;
			}
		}
		catch { }

		return upperMembers;
	}

	/*** Destroy all bubble in a row. */
	void DestroyRow(int row)
    {
		int i = 0;
		try 
		{
			while (Grid[i, row] != null)
			{
				Grid[i, row].GetComponent<GridMember>().state = "Pop";
				Grid[i, row] = null;
				i++;
			}
		}
		catch { }
	}

	public void CompareValue(int column, int row, int value)
    {
        //Debug.LogError(GetMemberCountInRow(row - 1));
        foreach (var member in GetUpperMember(column, row))
        {
            if (member.GetComponent<GridMember>().Value == answer[count])
            {
				DestroyRow(row - 1);
				count--;
				if (count > -1)
				{
					question.text = $"{left[count]} {oper[count]} {right[count]}";
				}
				else
				{
					//GameObject.Find("youwin2").transform.position = new Vector3(0, 0.58f, 0);
					FindInActiveObjectByName("youwin2").SetActive(true);
					Launcher.Instance.startShooting = false;
				}
				return;
            }
        }
    }

	GameObject FindInActiveObjectByName(string name)
	{
		Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
		for (int i = 0; i < objs.Length; i++)
		{
			if (objs[i].hideFlags == HideFlags.None)
			{
				if (objs[i].name == name)
				{
					return objs[i].gameObject;
				}
			}
		}
		return null;
	}

	public void Seek(int column, int row, int kind)
	{
		int[] pair = new int[2] { column, row };

		bool[,] visited = new bool[Columns, Rows];

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
			GameObject gtop = Grid[top[0], top[1]];
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
					GameObject g = Grid[neighbor[0], neighbor[1]];
					if (g != null)
					{
						GridMember gridMember = g.GetComponent<GridMember>();
						if (gridMember != null && gridMember.Kind == kind)
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
					Grid[gm.Column, -gm.Row] = null;
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

		bool[,] visited = new bool[Columns, Rows];

		Queue<int[]> queue = new Queue<int[]>();

		int[] deltax = { -1, 0, -1, 0, -1, 1 };
		int[] deltaxprime = { 1, 0, 1, 0, -1, 1 };
		int[] deltay = { -1, -1, 1, 1, 0, 0 };

		for (int i = 0; i < Columns; i++)
		{
			int[] pair = new int[2] { i, ceiling };
			if (Grid[i, ceiling] != null)
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
					GameObject g = Grid[neighbor[0], neighbor[1]];
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
			if (YouWin != null)
				YouWin.SetActive(true);
		}

		for (int r = 0; r < Rows; r++)
		{
			for (int c = 0; c < Columns; c++)
			{
				if (Grid[c, r] != null && !visited[c, r])
				{
					GameObject g = Grid[c, r];
					GridMember gm = g.GetComponent<GridMember>();
					if (gm != null)
					{
						Grid[gm.Column, -gm.Row] = null;
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

        string leveldata = "";

        for (int i = 0; i < BubbleSpawnRows; i++)
        {
            int columnnum = 5;
            if (i % 2 == 0) columnnum++;

            for (int j = 0; j < columnnum; j++)
            {
                leveldata += ((i % BubbleTypes) + 1).ToString();
            }

            if(i != BubbleSpawnRows - 1) leveldata += "\n";
        }

        return leveldata;
    }

    private void ArragementBubble(string level)
    {
        int levelpos = 0;
        for (int r = 0; r < Rows; r++)
        {
            if (r % 2 != 0) Columns -= 1;

			int answerIndex = Random.Range(0, Columns);

			for (int c = 0; c < Columns; c++)
            {
                Vector3 position = new Vector3((float)c * Gap, (float)(-r) * Gap, 0f) + InitialPos;
                if (r % 2 != 0)
                    position.x += 0.5f * Gap;

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

				if(c == answerIndex)
					Create(position, newKind, true, answer[r]);
				else
					Create(position, newKind, false, answer[r]);
                levelpos++;
            }
            if (r % 2 != 0) Columns += 1;
        }
    }

    #endregion
}
