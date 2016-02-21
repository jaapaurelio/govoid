
public static class Package1 {

	public const int numberOfLevels = 20;

	public static int[,] GetLevel(int level) {
		switch(level) {

		case 1:
			return new int[3, 3] { 
				{0, 0, 0},
				{1, 1, 0},
				{0, 0, 0}
			};

		case 2:
			return new int[3, 3] { 
				{0, 0, 0},
				{1, 1, 1},
				{0, 0, 0}
			};

		default:
			return null;
		}
	}

	/*
	public static readonly int[,] level1 = 

	public static readonly int[,] level2 = 
*/
	public static readonly int[,] level3 = new int[3, 3] { 
		{0, 0, 1},
		{1, 1, 1},
		{1, 0, 0}
	};

	public static readonly int[,] level4 = new int[3, 3] { 
		{0, 1, 1},
		{1, 1, 1},
		{1, 0, 0}
	};

	public static readonly int[,] level5 = new int[3, 3] { 
		{2, 1, 0},
		{1, 1, 0},
		{0, 0, 0}
	};

	public static readonly int[,] level6 = new int[3, 3] { 
		{0, 1, 1},
		{1, 2, 1},
		{0, 0, 0}
	};

	public static readonly int[,] level7 = new int[3, 3] { 
		{1, 2, 0},
		{1, 2, 1},
		{0, 0, 0}
	};

	public static readonly int[,] level8 = new int[3, 3] { 
		{1, 1, 0},
		{1, 2, 1},
		{0, 1, 0}
	};

	public static readonly int[,] level9 = new int[3, 3] { 
		{0, 1, 1},
		{1, 2, 2},
		{1, 1, 1}
	};

	public static readonly int[,] level10 = new int[3, 3] { 
		{1, 1, 0},
		{1, 3, 1},
		{1, 2, 1}
	};

	public static readonly int[,] level11 = new int[3, 3] { 
		{1, 1, 0},
		{2, 3, 1},
		{2, 2, 0}
	};

	public static readonly int[,] level12 = new int[4, 4] { 
		{1, 0, 0, 0},
		{1, 1, 0, 0},
		{0, 1, 1, 1},
		{0, 0, 1, 1}
	};

	public static readonly int[,] level13 = new int[4, 4] { 
		{0, 1, 1, 0},
		{0, 1, 1, 0},
		{0, 1, 2, 1},
		{0, 0, 1, 0}
	};

	public static readonly int[,] level14 = new int[4, 4] { 
		{0, 1, 0, 0},
		{0, 2, 1, 1},
		{1, 2, 1, 0},
		{1, 1, 1, 0}
	};

	public static readonly int[,] level15 = new int[4, 4] { 
		{0, 1, 0, 0},
		{1, 2, 1, 0},
		{1, 1, 2, 1},
		{1, 1, 1, 0}
	};

	public static readonly int[,] level16 = new int[4, 4] { 
		{0, 0, 0, 0},
		{0, 1, 2, 1},
		{1, 1, 3, 1},
		{1, 1, 1, 0}
	};

	public static readonly int[,] level17 = new int[4, 4] { 
		{0, 1, 1, 0},
		{0, 2, 3, 1},
		{1, 2, 2, 1},
		{1, 1, 1, 0}
	};

	public static readonly int[,] level18 = new int[4, 4] { 
		{0, 0, 1, 1},
		{1, 1, 2, 2},
		{1, 2, 3, 2},
		{0, 1, 2, 1}
	};

	public static readonly int[,] level19 = new int[4, 4] { 
		{1, 0, 1, 1},
		{2, 2, 3, 2},
		{1, 2, 2, 1},
		{0, 1, 1, 1}
	};

	public static readonly int[,] level20 = new int[4, 4] { 
		{0, 1, 2, 1},
		{0, 1, 4, 2},
		{0, 0, 4, 2},
		{0, 0, 1, 1}
	};
}

/*
public static readonly int[,] level = new int[3, 3] { 
	{0, 0, 0},
	{0, 0, 0},
	{0, 0, 0}
};

public static readonly int[,] level = new int[4, 4] { 
	{0, 0, 0, 0},
	{0, 0, 0, 0},
	{0, 0, 0, 0},
	{0, 0, 0, 0}
};
*/