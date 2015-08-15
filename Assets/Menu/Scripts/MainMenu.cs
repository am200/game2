using UnityEngine;
using System.Collections;
using RTS;

public class MainMenu : AbstractButtonMenu
{

		public void Start ()
		{
				base.Start ("MainMenu", 250);
		}

		void OnLevelWasLoaded ()
		{
				Cursor.visible = true;
				if (PlayerManager.GetPlayerName () == "") {
						//no player yet selected so enable SetPlayerMenu
						GetComponent<MainMenu> ().enabled = false;
						GetComponent<SelectPlayerMenu> ().enabled = true;
				} else {
						//player selected so enable MainMenu
						GetComponent<MainMenu> ().enabled = true;
						GetComponent<SelectPlayerMenu> ().enabled = false;
				}
		}
	
		protected override void SetButtons ()
		{
				buttons = new string[] {
						"New Game",
						"Load Game",
						"Change Player",
						"Quit Game"
				};
		}
	
		protected override void HandleButton (string text)
		{
				base.HandleButton (text);
				switch (text) {
				case "New Game":
						NewGame ();
						break;
				case "Load Game":
						LoadGame ();
						break;
				case "Quit Game":
						ExitGame ();
						break;
				case "Change Player":
						ChangePlayer ();
						break;
				default:
						break;
				}
		}
	
		protected override void HideCurrentMenu ()
		{
				GetComponent<MainMenu> ().enabled = false;
		}
	
		private void NewGame ()
		{
				HideCurrentMenu ();
				CampaignMenu campaignMenu = GetComponent<CampaignMenu> ();
				if (campaignMenu) {
						campaignMenu.enabled = true;
						campaignMenu.Activate ();
				}
		}
	
		private void ChangePlayer ()
		{
				GetComponent<MainMenu> ().enabled = false;
				GetComponent<SelectPlayerMenu> ().enabled = true;
				SelectionList.LoadEntries (PlayerManager.GetPlayerNames ());
		}
	
		protected void LoadGame ()
		{
				HideCurrentMenu ();
				LoadMenu loadMenu = GetComponent<LoadMenu> ();
				if (loadMenu) {
						loadMenu.enabled = true;
						loadMenu.Activate ();
				}
		}
	
		protected void ExitGame ()
		{
				Application.Quit ();
		}
}