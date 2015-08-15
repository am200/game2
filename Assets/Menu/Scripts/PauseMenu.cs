using UnityEngine;
using RTS;

public class PauseMenu : AbstractButtonMenu
{

		private Player player;
	
		protected void Start ()
		{
				base.Start ("PauseMenu", 250);
				player = transform.root.GetComponent<Player> ();
		}
	
		protected override void Update ()
		{
				if (Input.GetKeyDown (KeyCode.Escape)) {
						Resume ();
				}
		}
	
		protected override void SetButtons ()
		{
				buttons = new string[] {"Resume", "Save Game", "Load Game", "Exit Game"};
		}
	
		protected override void HandleButton (string text)
		{
				base.HandleButton (text);
				switch (text) {
				case "Resume":
						Resume ();
						break;
				case "Save Game":
						SaveGame ();
						break;
				case "Load Game":
						LoadGame ();
						break;
				case "Exit Game":
						ReturnToMainMenu ();
						break;
				default:
						break;
				}
		}
	
		protected override void HideCurrentMenu ()
		{
				GetComponent<PauseMenu> ().enabled = false;
		}
	
		private void Resume ()
		{
				Time.timeScale = 1.0f;
				GetComponent<PauseMenu> ().enabled = false;
				if (player) {
						player.GetComponent<UserInput> ().enabled = true;
				}
				Cursor.visible = false;
				ResourceManager.MenuOpen = false;
		}
	
		private void SaveGame ()
		{
				GetComponent<PauseMenu> ().enabled = false;
				SaveMenu saveMenu = GetComponent<SaveMenu> ();
				if (saveMenu) {
						saveMenu.enabled = true;
						saveMenu.Activate ();
				}
		}
	
		private void ReturnToMainMenu ()
		{
				ResourceManager.LevelName = "";
				Application.LoadLevel ("MainMenu");
				Cursor.visible = true;
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
	
}