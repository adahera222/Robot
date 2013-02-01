using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
	public class RobotController : MonoBehaviour 
	{
		private const string PROGRAM_KEY = "ProgramKey";
		public static RobotController Instance { get; private set; }

		private Vector3 _lastPosition;
		private Vector3 _startPosition;
		private float _lastMoveTime;

		public float MoveDelay;
		public List<Direction> Program { get; private set; }
		public bool IsRunning;
		public int NextMoveInd;

		public AudioClip BlockClip;
		public AudioClip MoveClip;
		public AudioClip WinClip;
		public GUIText WinText;

		public void Awake()
		{
			Instance = this;
			Program = new List<Direction>();
			Load();
		}

		public void Update()
		{
			if (IsRunning && _lastMoveTime + MoveDelay <= Time.time)
			{
				_lastMoveTime = Time.time;
				TryMove();
			}

			if (transform.position == new Vector3(3.5f, -3.5f, 0.3f))
			{
				Win();
			}
		}

		public void Run()
		{
			_startPosition = transform.position;
			IsRunning = true;
			WinText.gameObject.SetActive(false);
		}

		private void TryMove()
		{
			Direction direction = Program[NextMoveInd];
			NextMoveInd = (NextMoveInd + 1) % Program.Count;
			_lastPosition = transform.position;
			Vector3 position = direction.Apply(_lastPosition);
			iTween.MoveTo(gameObject, position, MoveDelay * 0.7f);
			audio.PlayOneShot(MoveClip);
		}

		public void OnTriggerEnter(Collider wall)
		{
			iTween.Stop(gameObject);
			iTween.MoveTo(gameObject, _lastPosition, MoveDelay * 0.25f);
			audio.PlayOneShot(BlockClip);
		}

		public void Stop()
		{
			IsRunning = false;
			iTween.Stop(gameObject);
			transform.position = _startPosition;
			NextMoveInd = 0;
			WinText.gameObject.SetActive(false);
		}

		private void Win()
		{
			Stop();
			audio.PlayOneShot(WinClip);
			WinText.gameObject.SetActive(true);
		}

		public void Load()
		{
			string strProgram = PlayerPrefs.GetString(PROGRAM_KEY, "RRRRUUUU");
			foreach (char ch in strProgram)
				Program.Add(Direction.Parse(ch));
		}

		public void Save()
		{
			string strProgram = Program.Aggregate(string.Empty, (current, direction) => current + direction.ToChar());
			PlayerPrefs.SetString(PROGRAM_KEY, strProgram);
		}
	}
}
