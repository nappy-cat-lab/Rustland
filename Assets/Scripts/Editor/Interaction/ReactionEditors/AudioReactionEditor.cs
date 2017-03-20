using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AudioReaction))]
public class AudioReactionEditor : ReactionEditor {
/*
	private SerializedProperty audioSourceProperty;
	private SerializedProperty audioClipProperty;
	private SerializedProperty delayProperty;

	private const string audioReactionPropAudioSourceName = "audioSource";
	private const string audioReactionPropAudioClipName = "audioClip";
	private const string audioReactionPropDelayName = "delay";

	private const float areaWidthOffset = 19f;
	private const float messageGUILines = 3f;

	protected override void Init()
	{
		audioSourceProperty = serializedObject.FindProperty(audioReactionPropAudioSourceName);
		audioClipProperty = serializedObject.FindProperty(audioReactionPropAudioClipName);
		delayProperty = serializedObject.FindProperty(audioReactionPropDelayName);
	}

	protected override void DrawReaction()
	{
		EditorGUILayout.BeginHorizontal();
		/*
		EditorGUILayout.LabelField("Message", GUILayout.Width(EditorGUIUtility.labelWidth - areaWidthOffset));
		messageProperty.stringValue =
			EditorGUILayout.TextArea(
				messageProperty.stringValue,
				GUILayout.Height(EditorGUIUtility.singleLineHeight * messageGUILines));

		EditorGUILayout.EndHorizontal();

		EditorGUILayout.PropertyField(audioSourceProperty);
		EditorGUILayout.PropertyField(audioClipProperty);
		EditorGUILayout.PropertyField(delayProperty);
	}
*/
	protected override string GetFoldoutLabel()
	{
		return "Audio Reaction";
	}
}
