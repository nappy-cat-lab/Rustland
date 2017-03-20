/// <summary>
/// SceneReaction is used to change between scenes
/// though there is a delay while the scene fades out.
/// this is done with the SceneController class and so
/// this is just a Reaction not a DelayedReaction.
/// </summary>
public class SceneReaction : Reaction
{
	// the name of the scene to be loaded.
	public string sceneName;

	// the name of the StartingPosition in the newly loaded scene.
	public string startingPointInLoadedScene;

	// reference to the save data asset that will store the StartingPosition.
	public SavaData playerSavaData;

	// reference to the SceneController to actually do the loading and unloading of scenes.
	private SceneController sceneController;

	// use this for initialization
	protected override void SpecificInit()
	{
		sceneController = FindObjectOfType<SceneController>();
	}

	// Update is called once per frame
	protected override void ImmediateReaction ()
	{
		// save the StartingPosition's name to the data asset.
		playerSavaData.Save (PlayerMovement.startingPositionKey, startingPointInLoadedScene);

		// start the scene loading process.
		sceneController.FadeAndLoadScene (this);
	}
}
