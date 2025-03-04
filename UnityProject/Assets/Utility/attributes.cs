using UnityEditor;
using UnityEngine;

/// <summary>
/// 인스펙터에서 읽기 전용 필드로 만들기 위한 애트리뷰트
/// </summary>
public class ReadOnlyAttribute : PropertyAttribute { }

/// <summary>
/// ReadOnly 애트리뷰트를 인스펙터에서 읽기 전용으로 표시하는 Drawer
/// </summary>
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // 기존 GUI 활성화 상태 저장
        bool previousGUIState = GUI.enabled;

        // GUI를 비활성화하여 읽기 전용으로 만듦
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label);

        // 원래 GUI 활성화 상태 복원
        GUI.enabled = previousGUIState;
    }
}