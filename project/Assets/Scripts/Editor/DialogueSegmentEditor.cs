using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DialogueSegment))]
[CanEditMultipleObjects]
public class DialogueSegmentEditor : Editor
{
    bool showEndEvents;

    public override void OnInspectorGUI()
    {
        DialogueSegment segment = target as DialogueSegment;

        segment.textTag = EditorGUILayout.TextField("Tag", segment.textTag);

        SerializedProperty boxes = serializedObject.FindProperty("textBoxes");
        EditorGUILayout.PropertyField(boxes, true);

        segment.action = (EndActions)EditorGUILayout.EnumPopup("Action", segment.action);

        if (segment.action == EndActions.ShowCards)
        {
            // Card responses array
            SerializedProperty responses = serializedObject.FindProperty("responses");
            EditorGUILayout.PropertyField(responses, true);
        }
        else if (segment.action == EndActions.StartSegment)
        {
            segment.nextSegment = EditorGUILayout.TextField("Next segment", segment.nextSegment);
        }

        // Remove player card
        segment.removeCard = EditorGUILayout.Toggle("Remove Card", segment.removeCard);
        if (segment.removeCard)
        {
            segment.cardNameToTake = EditorGUILayout.TextField("Card To Remove", segment.cardNameToTake);
        }

        EditorGUILayout.Separator();

        // New player cards
        segment.giveCard = EditorGUILayout.Toggle("Give Card", segment.giveCard);
        if (segment.giveCard)
        {
            SerializedProperty card = serializedObject.FindProperty("cardToGive");
            EditorGUILayout.PropertyField(card, true);
        }

        EditorGUILayout.Separator();

        showEndEvents = EditorGUILayout.Foldout(showEndEvents, "Segment End Events");

        if (showEndEvents)
        {
            SerializedProperty endEvent = serializedObject.FindProperty("endEvent");
            EditorGUILayout.PropertyField(endEvent, true);
        }

        if (segment.action == EndActions.End)
        {
            segment.returnControl = EditorGUILayout.Toggle("Return Control", segment.returnControl);
            segment.allowInteraction = EditorGUILayout.Toggle("Allow Interaction", segment.allowInteraction);
        }

        // Apply properties
        // KEEP AT END
        serializedObject.ApplyModifiedProperties();
    }
}
