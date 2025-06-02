using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuItem : MonoBehaviour
{
   [HideInInspector] public Image Image; 
   [HideInInspector] public RectTransform RectTransform; 
   private SettingsMenu _settingsMenu;
   private Button _button;
   private int _index;

   void Awake()
   {
      Image = GetComponent<Image>();
      RectTransform = GetComponent<RectTransform>();

      _settingsMenu = RectTransform.parent.GetComponent<SettingsMenu>();

      _index = RectTransform.GetSiblingIndex() - 1;

      _button = GetComponent<Button>();
      _button.onClick.AddListener(OnItemClick);
   }

   private void OnItemClick()
   {
      _settingsMenu.OnItemClick(_index);
   }

   void OnDestroy()
   {
      _button.onClick.RemoveListener(OnItemClick);
   }
}