using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ConditionReaction))]
public class ConditionReactionEditor : ReactionEditor
{
	private SerializedProperty conditionProperty;
	private SerializedProperty satisfiedProperty;

	private const string conditionReactionPropConditionName = "condition";
	private const string conditionReactionPropSatisfiedName = "satisfied";

	private const float areaWidthOffset = 19f;
	private const float messageGUILines = 3f;

	protected override void Init()
	{
		conditionProperty = serializedObject.FindProperty(conditionReactionPropConditionName);
		satisfiedProperty = serializedObject.FindProperty(conditionReactionPropSatisfiedName);
	}

	protected override void DrawReaction()
	{
		if (conditionProperty.objectReferenceValue == null)
			conditionProperty.objectReferenceValue = AllConditionsEditor.TryGetConditionAt (0);

		int index = AllConditionsEditor.TryGetConditionIndex ((Condition)conditionProperty.objectReferenceValue);

		index = EditorGUILayout.Popup (index, AllConditionsEditor.AllConditionDescriptions);
		conditionProperty.objectReferenceValue = AllConditionsEditor.TryGetConditionAt(index);

		EditorGUILayout.PropertyField(satisfiedProperty);
	}
	protected override string GetFoldoutLabel()
	{
		return "Condition Reaction";
	}
}
