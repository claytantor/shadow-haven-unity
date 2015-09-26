using System;
using NUnit.Framework;
using SimpleJSON;
using System.IO;
using Utils;

namespace Tests
{
	[TestFixture()]
	public class ShadowHavenTests
	{
		[Test()]
		public void TestCase ()
		{			
			JSONNode sceneJson = JSONUtils.ParseFile("../../../Assets/Resources/Data/scene1.json");				
			
			Player player = new Player(JSONUtils.ParseFile("../../../Assets/Resources/TestData/player1.json"));
			
			Scene1StateManager scene = new Scene1StateManager(sceneJson["states"].AsArray);
			scene.Player = player;
			
			Assert.AreEqual("SceneStart0",scene.SetState(player.LastState));
			
						
		}
	}
}

