using UnityEngine;
using System.Collections.Generic;
using RTS;

public abstract class AbstractMenu : MonoBehaviour
{
	
	public GUISkin mySkin, selectionSkin;
	public AudioClip clickSound;
	public float clickVolume = 1.0f;
	protected AudioElement audioElement;
	private float menuHeight;
	
	protected virtual void Start (string menuAudioElementName, float menuHeight)
	{
		Activate ();
		if (clickVolume < 0.0f) {
			clickVolume = 0.0f;
		}
		if (clickVolume > 1.0f) {
			clickVolume = 1.0f;
		}
		List<AudioClip> sounds = new List<AudioClip> ();
		List<float> volumes = new List<float> ();
		sounds.Add (clickSound);
		volumes.Add (clickVolume);
		audioElement = new AudioElement (sounds, volumes, menuAudioElementName, null);
		this.menuHeight = menuHeight;

	}
	
	protected virtual void Update ()
	{
	}

	protected abstract void OnGUI ();
	
	protected void PlayClick ()
	{
		if (audioElement != null) {
			audioElement.Play (clickSound);
		}
	}
	
	public abstract void Activate ();

	protected abstract void DrawMenu ();
	
	protected virtual float GetMenuHeight ()
	{
		return menuHeight + GetMenuItemsHeight ();
	}
	
	protected virtual float GetMenuItemsHeight ()
	{
		return ResourceManager.ButtonHeight + ResourceManager.TextHeight + 3 * ResourceManager.Padding;
	}


}
