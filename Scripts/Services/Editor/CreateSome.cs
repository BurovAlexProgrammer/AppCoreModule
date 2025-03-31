using UnityEngine;
using UnityEditor;

public static class CreateSomeMenu
{
    // Параметры:
    // "GameObject/Create/Some" – путь меню.
    // false – не является валидатором.
    // 10 – приоритет в меню (можно настроить порядок).
    [MenuItem("GameObject/Create/Some", false, 10)]
    private static void CreateSome(MenuCommand menuCommand)
    {
        // Создаем новый GameObject с именем "Some"
        GameObject go = new GameObject("Some");
        
        // Если пункт меню был вызван через контекстное меню (например, при правом клике на объекте),
        // то назначаем созданный объект дочерним для выбранного объекта
        GameObject parent = menuCommand.context as GameObject;
        if (parent != null)
        {
            GameObjectUtility.SetParentAndAlign(go, parent);
        }
        
        // Регистрируем операцию создания для возможности отмены (Undo)
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        
        // Выделяем созданный объект
        Selection.activeObject = go;
    }
}