using UnityEditor;
public class InteractableEditor : Editor {
    /*public override void OnInspectorGUI() {
        Interactable interactable = (Interactable)target;
        if (target.GetType() == typeof(EventOnlyInteractable)) {
            interactable.promptMassage = EditorGUILayout.TextField("Prompt Massage", interactable.promptMassage);
            EditorGUILayout.HelpBox("EventOnlyInteract can ONLY use UnityEvents.", MessageType.Info);
            if (interactable.GetComponent<InteractionEvent>() == null) {
                interactable.useEvents = true;
                interactable.gameObject.AddComponent<InteractionEvent>();
            }
        } else {
            base.OnInspectorGUI();
            if (interactable.useEvents) {
                if (interactable.GetComponent<InteractionEvent>() == null)
                    interactable.gameObject.AddComponent<InteractionEvent>();
            } else {
                if (interactable.GetComponent<InteractionEvent>() == null)
                    DestroyImmediate(interactable.GetComponent<InteractionEvent>());
            }
        }

    */}