using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(ChatManager))]
//public class ChatEditor : Editor
//{
//    ChatManager ChatManager;
//    string text;

//    private void OnEnable()
//    {
//        ChatManager = target as ChatManager;
//    }

//    //public override void OnInspectorGUI()
//    //{
//    //    base.OnInspectorGUI();

//    //    EditorGUILayout.BeginHorizontal();
//    //    text = EditorGUILayout.TextArea(text);

//    //    if(GUILayout.Button("내채팅", GUILayout.Width(60)) && text.Trim() != "")
//    //    {
//    //        ChatManager.Chat(true, text, "나", null);
//    //        text = "";
//    //        GUI.FocusControl(null);
//    //    }

//    //    if (GUILayout.Button("남채팅", GUILayout.Width(60)) && text.Trim() != "")
//    //    {
//    //        ChatManager.Chat(false, text, "타인", null);
//    //        text = "";
//    //        GUI.FocusControl(null);
//    //    }
//    //    ChatManager.scroll_rect.verticalNormalizedPosition = 0.0f;
//    //    EditorGUILayout.EndHorizontal();
//    //}
//}
