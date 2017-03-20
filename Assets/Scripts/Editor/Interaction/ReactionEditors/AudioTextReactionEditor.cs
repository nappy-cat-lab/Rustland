using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AudioTextReaction))]
public class AudioTextReactionEditor : ReactionEditor {

	protected override string GetFoldoutLabel()
	{
		return "Audio Text Reaction";
	}
}
