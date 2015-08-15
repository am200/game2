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

				public static void SetTimeScale (float timeScale)
				{
						Time.timeScale = timeScale;
				}

				public static void LoadMainMenu ()
				{
						LoadMap ("MainMenu");
				}
			
				public static Dictionary<string,string> GetMapValues ()
				{
						Dictionary<string,string> dict = new Dictionary<string,string> ();							
						dict.Add ("Conquest", "Map");
						dict.Add ("Survival", "Map1");
						dict.Add ("AccumulateMoney", "Map2");
						dict.Add ("EscortConvoy", "Map3");
						dict.Add ("BuildWonder", "Map4");
						return dict;
				}

				public static void CheckForMapNameAndLoad (string buttonName, string newLevel)
				{	
						string mapName;
						if (GetMapValues ().TryGetValue (buttonName, out mapName)) {
								if (CheckForLevelName (mapName)) {
										ResourceManager.LevelName = newLevel;
										LoadMap (mapName);
								}
								//makes sure that the loaded level runs at normal speed
								SetTimeScale (1.0f);
						}

				}
		}

}

