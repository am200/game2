using UnityEngine;
using System.Collections;
using RTS;

public class CampaignMenu : AbstractButtonMenu
{

		public void Start(){
		base.Start("CampaignMenu",250);
	}
	
		protected override void SetButtons ()
		{
				buttons = new string[] {
					"Map1",
					"Map2",
					"Map3",
					"Map4",
					"Exit"
				};
		}
	
		protected override void HandleButton (string text)
		{
				base.HandleButton (text);

				if (text.Equals ("Exit")) {
						ReturnToMainMenu ();
						return;
				}

				startMap (text);
		}
	
		protected override void HideCurrentMenu ()
		{
				GetComponent<MainMenu> ().enabled = false;
		}
	
		private void startMap (string mapName)
		{
				string newLevel = SelectionList.GetCurrentEntry ();
				if (newLevel != "") {
						ResourceManager.LevelName = newLevel;
						if (Application.loadedLevelName != mapName) {
				Application.LoadLevel (mapName);
				//makes sure that the loaded level runs at normal speed
				Time.timeScale = 1.0f;
						}
						//makes sure that the loaded level runs at normal speed
						Time.timeScale = 1.0f;
				}
		}

		private void ReturnToMainMenu ()
		{
				ResourceManager.LevelName = "";
				Application.LoadLevel ("MainMenu");
				Cursor.visible = true;
		}
}