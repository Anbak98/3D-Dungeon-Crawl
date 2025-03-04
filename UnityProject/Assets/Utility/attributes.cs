using UnityEditor;
using UnityEngine;

/// <summary>
/// �ν����Ϳ��� �б� ���� �ʵ�� ����� ���� ��Ʈ����Ʈ
/// </summary>
public class ReadOnlyAttribute : PropertyAttribute { }

/// <summary>
/// ReadOnly ��Ʈ����Ʈ�� �ν����Ϳ��� �б� �������� ǥ���ϴ� Drawer
/// </summary>
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // ���� GUI Ȱ��ȭ ���� ����
        bool previousGUIState = GUI.enabled;

        // GUI�� ��Ȱ��ȭ�Ͽ� �б� �������� ����
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label);

        // ���� GUI Ȱ��ȭ ���� ����
        GUI.enabled = previousGUIState;
    }
}