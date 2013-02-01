using System;
using UnityEngine;

namespace Assets.Scripts
{
	public class GUIController : MonoBehaviour
	{
		public Texture Left;
		public Texture Right;
		public Texture Up;
		public Texture Down;
		public Texture StartTexture;
		public Texture StopTexture;
		public Texture Arrow;

		public void OnGUI()
		{
			GUILayout.BeginHorizontal();

			if (RobotController.Instance.IsRunning)
			{
				if (GUILayout.Button(StopTexture))
				{
					RobotController.Instance.Stop();
				}
			}
			else
			{
				if (GUILayout.Button(StartTexture))
				{
					RobotController.Instance.Run();
				}
			}

			for (int ind = 0; ind < RobotController.Instance.Program.Count; ind++)
			{
				Direction direction = RobotController.Instance.Program[ind];
				if (ind == RobotController.Instance.NextMoveInd)
				{
					GUILayout.BeginVertical();
					if (GUILayout.Button(GetTexture(direction)) && !RobotController.Instance.IsRunning)
					{
						RobotController.Instance.Program[ind] = direction.Next();
						RobotController.Instance.Save();
					}

					GUILayout.Box(Arrow);

					GUILayout.EndVertical();
				}
				else
				{
					if (GUILayout.Button(GetTexture(direction)) && !RobotController.Instance.IsRunning)
					{
						RobotController.Instance.Program[ind] = direction.Next();
						RobotController.Instance.Save();
					}
				}
			}

			GUILayout.BeginVertical();
			GUILayout.Label("Speed");
				
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Slow"))
				RobotController.Instance.MoveDelay = 1.5f;
			if (GUILayout.Button("Medium"))
				RobotController.Instance.MoveDelay = 1.0f;
			if (GUILayout.Button("Fast"))
				RobotController.Instance.MoveDelay = 0.5f;
			GUILayout.EndHorizontal();

			GUILayout.EndVertical();

			GUILayout.EndHorizontal();

			GUI.Label(new Rect(10, 130, 100, 100), "Program the Robot so that it got to the flag.");
		}

		private Texture GetTexture(Direction direction)
		{
			if (direction == Direction.Up)
				return Up;
			else if (direction == Direction.Down)
				return Down;
			else if (direction == Direction.Left)
				return Left;
			else if (direction == Direction.Right)
				return Right;

			throw new NotSupportedException();
		}

	}
}
