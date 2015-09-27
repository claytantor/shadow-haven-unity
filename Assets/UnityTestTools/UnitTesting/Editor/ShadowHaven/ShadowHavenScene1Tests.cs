using System;
using NUnit.Framework;
using SimpleJSON;
using System.IO;
using Utils;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
	[TestFixture()]
	public class ShadowHavenScene1Tests
	{
		private JSONNode sceneJson;

		public ShadowHavenScene1Tests(){
			sceneJson = JSONUtils.ParseFile("../../../Assets/Resources/Data/scene1.json");
		}

		[Test()]
		public void TestBasicPlayerState ()
		{

			Player player = new Player(JSONUtils.ParseFile("../../../Assets/Resources/TestData/player1.json"));
			Scene1StateManager scene = new Scene1StateManager(sceneJson);
			scene.Player = player;
			Assert.AreEqual("SceneStart0",scene.SetState("SceneStart"));

			player.AddStateCrumb("start0");
			Assert.AreEqual("SceneStart1",scene.SetState("SceneStart"));

			Assert.AreEqual("UnderBed0",scene.SetState("UnderBed"));

			player.AddStateCrumb("foobar0");
			player.AddStateCrumb("movebed0");
			Assert.AreEqual("UnderBed1",scene.SetState("UnderBed"));
			Assert.AreEqual("SceneStart1",scene.SetState("SceneStart"));
			
			



		}
		
		
		[Test()]
		public void TestFilter ()
		{
			string[] a = { "a","b","x"};
			string[] b = { "x"};
			
			IEnumerable<string> filtered =
				a.Except(b, new StringComparer());
				
			string[] fitems = CollectionUtils.AsArray(filtered);	
				
			Assert.AreEqual("a",fitems[0]);
			Assert.AreEqual("b",fitems[1]);
		
		}
	}
}
