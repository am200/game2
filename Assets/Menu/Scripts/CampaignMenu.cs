using UnityEngine;
using System.Collections;
using RTS;
using System.Collections.Generic;

public class CampaignMenu : AbstractButtonMenu
{

		public void Start ()
		{
				base.Start ("CampaignMenu", 250);
		}
	
		protected override void SetButtons ()
		{
				buttons = (new List<string> (MapManager.GetMapValues ().Keys)).ToArray ();
		}
	
		protected override void HandleButton (string text)
		{
				base.HandleButton (text);
				if (text.Equals ("Exit")) {
						ReturnToMainMenu ();
						return;
				}
				StartMap (text);
		}
	
		protected override void HideCurrentMenu ()
		{
				GetComponent<MainMenu> ().enabled = false;
		}
	
		private void StartMap (string buttonName)
		{
				string newLevel = SelectionList.GetCurrentEntry ();
				if (newLevel != "") {
						MapManager.CheckForMapNameAndLoad (buttonName, newLevel);
				}
		}

		private void ReturnToMainMenu ()
		{
				ResourceManager.LevelName = "";
				MapManager.LoadMainMenu ();
				Cursor.visible = true;
		}
}