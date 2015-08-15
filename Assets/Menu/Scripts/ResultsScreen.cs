using UnityEngine;
using System.Collections.Generic;
using RTS;

public class ResultsScreen : AbstractMenu
{
	
		private Player winner;
		private VictoryCondition metVictoryCondition;
		private readonly string GAME_OVER = "Game Over";
		private readonly string WIN_MESSAGE = "Congratulations %s! You have won by %s";

		protected override string GetMenuName ()
		{
				return "ResultsScreen";
		}
	
		void Start ()
		{
		
				base.Start (GetMenuName (), 250);
		}
	
		protected override void OnGUI ()
		{
				DrawMenu ();
		}

		protected override void DrawMenu ()
		{
				GUI.skin = mainSkin;
		
				GUI.BeginGroup (new Rect (0, 0, Screen.width, Screen.height));
		
				//display 
				float padding = ResourceManager.Padding;
				float itemHeight = ResourceManager.ButtonHeight;
				float buttonWidth = ResourceManager.ButtonWidth;
				float leftPos = padding;
				float topPos = padding;
				GUI.Box (new Rect (0, 0, Screen.width, Screen.height), "");
				string message = GAME_OVER;
				if (winner) {
						message = string.Format (WIN_MESSAGE, winner.username, metVictoryCondition.GetDescription ());
				}
				GUI.Label (new Rect (leftPos, topPos, Screen.width - 2 * padding, itemHeight), message);
				leftPos = Screen.width / 2 - padding / 2 - buttonWidth;
				topPos += itemHeight + padding;
				if (GUI.Button (new Rect (leftPos, topPos, buttonWidth, itemHeight), ButtonManager.NEW_GAME)) {
						PlayClick ();
						//makes sure that the loaded level runs at normal speed
			MapManager.SetTimeScale(1.0f);
						ResourceManager.MenuOpen = false;
						MapManager.LoadMap("Map");
				}
				leftPos += padding + buttonWidth;
				if (GUI.Button (new Rect (leftPos, topPos, buttonWidth, itemHeight), ButtonManager.MAIN_MENU)) {
						ResourceManager.LevelName = "";
						MapManager.LoadMainMenu ();
						Cursor.visible = true;
				}
		
				GUI.EndGroup ();
		}

		public override void Activate ()
		{
		}
	
		public void SetMetVictoryCondition (VictoryCondition victoryCondition)
		{
				if (!victoryCondition) {
						return;
				}
				metVictoryCondition = victoryCondition;
				winner = metVictoryCondition.GetWinner ();
		}
}
