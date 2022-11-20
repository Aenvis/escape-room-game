using JetBrains.Annotations;
using Project.Systems.Equipment;
using Project.Systems.Quest;
using Project.Systems.SoundSystem;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Project.Systems.Interactable
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] [CanBeNull] private QuestData quest;
        [SerializeField] protected float playerDistance;
        [SerializeField] protected AudioClip soundEffect;
        
        
        protected Transform m_playerTransform;
        private PlayerActionMaps m_playerInput;
        private Inventory m_inventory;
        private SoundManager m_soundManager;
        private AudioSource m_audioSource = null;
        private bool m_canInteract = true;
        private float m_questInfoTimer;
        private float m_questInfoTimerMax = 3f;
        
        [Inject]
        private void Inject(PlayerActionMaps playerActionMaps, Inventory inventory, SoundManager soundManager)
        {
            m_playerInput = playerActionMaps;
            m_inventory = inventory;
            m_soundManager = soundManager;
        }

        protected virtual void OnEnable()
        {
            m_playerInput.Interactions.Enable(); //FIXME: its being enabled many times in different places 
            m_playerInput.Interactions.Interact.performed += OnInteractKey;
        }
        
        protected virtual void Start()
        {
            m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            if(quest != null) quest.Completed = false;
            
            // usable items effects use universal audio source, because of them being destroyed upon collecting, which 
            // would be bugged if we used audio source attached to the item ; 
            // UsableItem use SoundManager.universalEffectSource
            if (gameObject.GetType() == typeof(UsableItem)) return;
            m_audioSource = CreateSpatialAudioSource();
        }

        private void Update()
        {
            if (m_questInfoTimer <= 0) return;

            m_questInfoTimer -= Time.deltaTime;
        }

        protected virtual void OnDisable()
        {   //we dont want to disable action map after one object is deleted - it cuts all the interactions
            m_playerInput.Interactions.Interact.performed -= OnInteractKey;
        }

        protected virtual void OnMouseOver()
        {
            m_canInteract = true;
        }

        protected virtual void OnMouseExit()
        {
            m_canInteract = false;
        }

        private void OnGUI()
        {
           if (m_questInfoTimer <= 0 || quest is null) return;

                float x = Screen.width / 2f - 300f;
                float y = Screen.height - 110f;
                GUIStyle style = new GUIStyle(GUI.skin.box)
                {
                    fontSize = 25,
                    normal =
                    {
                        textColor = Color.white
                    }
                };
                GUI.TextArea(new Rect(x, y, 600, 45), quest.GetText(), style);
        }

        private void OnInteractKey(InputAction.CallbackContext context)
        {
            if(!m_canInteract || Vector3.Distance(transform.position, m_playerTransform.position) > playerDistance) return;

            if(CheckQuest()) Interaction();
            if (quest is not null && !quest.Completed) return;

            var item = gameObject.GetComponent<UsableItem>();
            if (item is not null)
            {
                m_soundManager.PlaySoundEffect(gameObject, soundEffect);
                Debug.Log("USABLE ITEM");
                return;
            }
            m_audioSource.PlayOneShot(soundEffect);
        }

        private bool CheckQuest()
        {
            if (quest is null) return true;

            if (quest.Completed) return true;

            if (m_inventory.Contains(quest.GetRequiredItem()))
            {
                m_inventory.RemoveItem(quest.GetRequiredItem());
                quest.Completed = true;
                return true;
            }

            m_questInfoTimer = m_questInfoTimerMax;
            return false;
        }

        private AudioSource CreateSpatialAudioSource()
        {
            var audioSrcGO = Instantiate(new GameObject("Audio Source", typeof(AudioSource)), transform.position, quaternion.identity); 
            var audioSrc = audioSrcGO.GetComponent<AudioSource>();
            audioSrcGO.transform.SetParent(gameObject.transform);
            audioSrc.spatialize = true;
            audioSrc.spatialBlend = 1.0f;
            audioSrc.rolloffMode = AudioRolloffMode.Logarithmic;
            return audioSrc;
        }
        
        protected abstract void Interaction();
    }
}