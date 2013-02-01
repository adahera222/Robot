using System;
using UnityEngine;

namespace Assets.Scripts
{
	public class Direction
	{
		public static Direction Left { get; private set; }
		public static Direction Right { get; private set; }
		public static Direction Up { get; private set; }
		public static Direction Down { get; private set; }

		public string Title { get; private set; }
		public int DX { get; private set; }
		public int DY { get; private set; }

		static Direction()
		{
			Left = new Direction(-1, 0, "Left");
			Right = new Direction(1, 0, "Right");
			Up = new Direction(0, -1, "Up");
			Down = new Direction(0, 1, "Down");
		}

		private Direction(int dx, int dy, string title)
		{
			DX = dx;
			DY = dy;
			Title = title;
		}

		public Vector3 Apply(Vector3 position)
		{
			return new Vector3(position.x + DX, position.y + DY, position.z);
		}

		public Direction Next()
		{
			if (this == Left)
				return Up;
			else if (this == Up)
				return Right;
			else if (this == Right)
				return Down;
			else if (this == Down)
				return Left;

			throw new NotSupportedException();
		}

		public override string ToString()
		{
			return Title;
		}

		public char ToChar()
		{
			return Title[0];
		}

		public static Direction Parse(char ch)
		{
			if (Up.Title[0] == ch)
				return Up;

			if (Down.Title[0] == ch)
				return Down;

			if (Left.Title[0] == ch)
				return Left;

			if (Right.Title[0] == ch)
				return Right;

			throw new Exception(string.Format("Can't parse '{0}'", ch));
		}

	}
}
