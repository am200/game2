using UnityEngine;
using System.Collections.Generic;
using RTS;

public abstract class AbstractConfirmationMenu : AbstractMenu
{
	
	public GUISkin confirmationSkin;
	protected ConfirmDialog confirmDialog = new ConfirmDialog ();
	
	protected override void Update ()
	{
		//handle escape key	
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (confirmDialog.IsConfirming ()) {
				confirmDialog.EndConfirmation ();
			} else {
				Cancel();
			}
		}
		//handle enter key in confirmation dialog
		if (Input.GetKeyDown (KeyCode.Return) && confirmDialog.IsConfirming ()) {
			confirmDialog.EndConfirmation ();
			ExecuteConfirmed();
		}
		
		//handle escape key	
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Cancel ();
		}
		//handle enter key in confirmation dialog
		if (Input.GetKeyDown (KeyCode.Return)) {
			Execute ();
		}
	}

	protected override void OnGUI ()
	{
		if (SelectionList.MouseDoubleClick ()) {
			PlayClick ();
			Execute ();
		}
		GUI.skin = mainSkin;
		DrawMenu ();
		//handle enter being hit when typing in the text field
		if (Event.current.keyCode == KeyCode.Return) {
			Execute ();
		}
		
	}
	
	protected abstract void Execute ();

	protected abstract bool CheckIfConfirmed ();
	
	protected abstract void Cancel ();

	protected void ExecuteConfirmed ()
	{
		if (CheckIfConfirmed ()) {
			confirmDialog.StartConfirmation (clickSound, audioElement);
		} else {
			Execute ();
		}
	}
}
