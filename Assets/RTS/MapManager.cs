using UnityEngine;
using System.Collections.Generic;

namespace RTS
{
		public static class MapManager
		{
				public static bool CheckForLevelName (string levelName)
				{
						return Application.loadedLevelName != levelName;
				}

				public static void LoadMap (string mapName)
				{
						Application.LoadLevel (mapName);
				}

				public static void setTimeScale (float timeScale)
				{
						Time.timeScale = timeScale;
				}

				public static void LoadMainMenu ()
				{
						LoadMap ("MainMenu");
				}

		}
}

