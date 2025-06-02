using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class SettingsMenu : MonoBehaviour
{
   [Header("Menu Item Spacing")]
   [SerializeField] private Vector2 _spacing; 

   [Space]
   [Header("Main Button Animation")]
   [SerializeField] private float _rotationDuration = 0.3f; 
   [SerializeField] private Ease _rotationEase = Ease.OutBack; 

   [Space]
   [Header("Menu Animation")]
   [SerializeField] private float _expandDuration = 0.6f; 
   [SerializeField] private float _collapseDuration = 0.6f; 
   [SerializeField] private Ease _expandEase = Ease.OutBack; 
   [SerializeField] private Ease _collapseEase = Ease.InBack; 

   [Space]
   [Header("Fade Animation")]
   [SerializeField] private float _expandFadeDuration = 0.2f; 
   [SerializeField] private float _collapseFadeDuration = 0.2f; 
   
   [Space]
   [Header("Audio Button Icons")]
   [SerializeField] private Image _musicButtonImage; 
   [SerializeField] private Image _soundButtonImage; 
   [SerializeField] private Sprite _musicOnSprite; 
   [SerializeField] private Sprite _musicOffSprite; 
   [SerializeField] private Sprite _soundOnSprite; 
   [SerializeField] private Sprite _soundOffSprite; 

   private Button _mainButton; 
   private SettingsMenuItem[] _menuItems; 
   private bool _isExpanded = false;
   private Vector2 _mainButtonPosition; 
   private int _itemsCount; 

   void Start()
   {
      _itemsCount = transform.childCount - 1;
      _menuItems = new SettingsMenuItem[_itemsCount];
      for (int i = 0; i < _itemsCount; i++)
      {
         _menuItems[i] = transform.GetChild(i + 1).GetComponent<SettingsMenuItem>();
      }

      _mainButton = transform.GetChild(0).GetComponent<Button>();
      _mainButton.onClick.AddListener(ToggleMenu);
      
      _mainButton.transform.SetAsLastSibling();

      _mainButtonPosition = _mainButton.GetComponent<RectTransform>().anchoredPosition;

      ResetPositions();
      
      UpdateButtonSprites();
   }

   private void ResetPositions()
   {
      for (int i = 0; i < _itemsCount; i++)
      {
         _menuItems[i].RectTransform.anchoredPosition = _mainButtonPosition;
      }
   }

   private void ToggleMenu()
   {
      _isExpanded = !_isExpanded;

      if (_isExpanded)
      {
         for (int i = 0; i < _itemsCount; i++)
         {
            _menuItems[i].RectTransform.DOAnchorPos(_mainButtonPosition + _spacing * (i + 1), _expandDuration)
               .SetEase(_expandEase) 
               .SetUpdate(true); 
            
            _menuItems[i].Image.DOFade(1f, _expandFadeDuration)
               .From(0f)
               .SetUpdate(true); 
         }
      }
      else
      {
         for (int i = 0; i < _itemsCount; i++)
         {
            _menuItems[i].RectTransform.DOAnchorPos(_mainButtonPosition, _collapseDuration)
               .SetEase(_collapseEase) 
               .SetUpdate(true); 
            
            _menuItems[i].Image.DOFade(0f, _collapseFadeDuration)
               .SetUpdate(true); 
         }
      }

      _mainButton.transform
			.DORotate(Vector3.forward * 180f, _rotationDuration)
			.From(Vector3.zero)
			.SetEase(_rotationEase)
         .SetUpdate(true); 
   }
   
   private void UpdateButtonSprites()
   {
      if (AudioManager.Instance != null)
      {
         _musicButtonImage.sprite = AudioManager.Instance.IsMusicEnabled() ? 
            _musicOnSprite : _musicOffSprite;
         
         _soundButtonImage.sprite = AudioManager.Instance.IsEffectsEnabled() ? 
            _soundOnSprite : _soundOffSprite;
      }
   }

   public void OnItemClick(int index)
   {
      switch (index)
      {
         case 0: 
            if (AudioManager.Instance != null)
            {
               bool newState = !AudioManager.Instance.IsMusicEnabled();
               AudioManager.Instance.ToggleMusic(newState);
               
               _musicButtonImage.sprite = newState ? _musicOnSprite : _musicOffSprite;
            }
            break;
            
         case 1: 
            if (AudioManager.Instance != null)
            {
               bool newState = !AudioManager.Instance.IsEffectsEnabled();
               AudioManager.Instance.ToggleEffects(newState);
               
               _soundButtonImage.sprite = newState ? _soundOnSprite : _soundOffSprite;
            }
            break;
            
         case 2: 
            #if UNITY_EDITOR
               UnityEditor.EditorApplication.isPlaying = false;
            #else
               Application.Quit();
            #endif
            break;
      }
   }

   void OnDestroy()
   {
      _mainButton.onClick.RemoveListener(ToggleMenu);
   }
}