using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

namespace BitBenderGames
{
    public class TreeController : MonoBehaviour
    {
        private GameObject manCharacter;
        private GameObject treeZone;
        private GameObject mainCanvas;
        private GameObject createTreeBtn;
        private GameObject exitBtn;
        private GameObject popupBack;

        private GameObject selectTreeBtn;
        private string[] treeName = { "tree0", "tree1", "tree2" };
        private string selectedTree = null;
        private Sprite selectedBtnSprite;
        private Sprite originalBtnSprite;

        private Dictionary<string, GameObject> treePrefabs;
        [SerializeField] private GameObject treePrefab0, treePrefab1, treePrefab2;
        private GameObject[] treeButtons;
        private GameObject treeDetailUIPanel;
        private CanvasGroup treePopupCanvasGroup;
        private CanvasGroup treeCreatePopupPanelCanvasGroup;


        private Camera cam;

        // private Transform selectedPickableTransform;
        private Dictionary<Renderer, List<Color>> originalItemColorCache = new Dictionary<Renderer, List<Color>>();

        private void Awake()
        {
            Application.targetFrameRate = 60;

            cam = GameObject.Find("Main Camera").GetComponent<Camera>();



            #region detail callbacks
            #endregion

        }

        void Start()
        {
            LoadTreeResoures();
            initTreeButtons();
            treePopupCanvasGroup = GameObject.Find("TreePopupPannel").GetComponent<CanvasGroup>();
            treeCreatePopupPanelCanvasGroup = GameObject.Find("TreeCreatePopupPannel").GetComponent<CanvasGroup>();
            manCharacter = GameObject.FindGameObjectWithTag("Owner");
            mainCanvas = GameObject.FindGameObjectWithTag("UICanvas");
            treeZone = GameObject.Find("TreeZone");
            exitBtn = GameObject.Find("ExitBtn");
            exitBtn.GetComponent<Button>().onClick.AddListener(() =>
            {
                ExitOpenedPopup();
            });
            popupBack = GameObject.Find("TreePopupPannel");
            popupBack.GetComponent<Button>().onClick.AddListener(() =>
            {
                ExitOpenedPopup();
            });
            createTreeBtn = GameObject.Find("Plus");
            createTreeBtn.GetComponent<Button>().onClick.AddListener(() =>
            {
                ShowPopUpDetail(treeCreatePopupPanelCanvasGroup);
            });
            selectTreeBtn = GameObject.Find("SelectTreeBtn");
            selectTreeBtn.GetComponent<Button>().onClick.AddListener(() =>
            {

                ExitOpenedPopup();
                if (selectedTree != null)
                {
                    createTree(selectedTree);
                    selectedTree = null;
                }
            });
        }

        void initTreeButtons()
        {
            int treeBtnSize = 3;
            treeButtons = new GameObject[treeBtnSize];
            for (int i = 0; i < treeBtnSize; i++)
            {
                treeButtons[i] = GameObject.Find("TreeSelectItem" + i);
            }
            foreach (GameObject treeBtn in treeButtons)
            {
                treeBtn.GetComponent<Button>().onClick.AddListener(() =>
                {
                    string id = treeBtn.name;
                    string treeIndex = id.Substring(id.Length - 1);
                    setOutlineInTreeBtns(treeIndex);
                    selectedTree = "tree" + treeIndex;
                });
            }
        }
        void setOutlineInTreeBtns(string id)
        {
            int btnNumber = int.Parse(id);
            int treeBtnSize = 3;
            for (int i = 0; i < treeBtnSize; i++)
            {
                if (i == btnNumber)
                {
                    treeButtons[i].GetComponent<Image>().sprite = selectedBtnSprite;
                }
                else
                {
                    treeButtons[i].GetComponent<Image>().sprite = originalBtnSprite;
                }
            }
        }
        void LoadTreeResoures()
        {
            treePrefabs = new Dictionary<string, GameObject>();
            treePrefabs.Add(treeName[0], treePrefab0);
            treePrefabs.Add(treeName[1], treePrefab1);
            treePrefabs.Add(treeName[2], treePrefab2);
            selectedBtnSprite = Resources.Load<Sprite>("SelectedBtnBackground");
            originalBtnSprite = Resources.Load<Sprite>("OriginalBtnBackground");
        }
        void ShowPopUpDetail(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        void ExitOpenedPopup()
        {

            if (treeCreatePopupPanelCanvasGroup.interactable)
            {
                ExitPopupByCanvas(treeCreatePopupPanelCanvasGroup);
            }
            else if (treePopupCanvasGroup.interactable)
            {
                ExitPopupByCanvas(treePopupCanvasGroup);
            }
            else
            {
                Debug.Log("There isn't opend Popup");
            }

        }
        void ExitPopupByCanvas(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }


        void createTree(string treePrefabName)
        {
            createTreeBtn.SetActive(false);
            treeZone.SetActive(false);
            Vector3 pos = treeZone.transform.position;
            Vector3 scale = new Vector3(0.8f, 0.8f, 0.8f);

            if (treePrefabs[treePrefabName])
            {
                GameObject newTree = Instantiate(treePrefabs[treePrefabName]);

                newTree.transform.SetParent(GameObject.Find("TreeArea").transform);
                newTree.transform.position = pos;
                newTree.transform.localScale = scale;
            }
        }


        public void OnPickItem(RaycastHit hitInfo)
        {
            Debug.Log("OnPickItem a collider: " + hitInfo.collider);
        }

        public void OnPickableTransformSelected(Transform pickableTransform)
        {

        }

 
        public void OnPickableTransformDeselected(Transform pickableTransform)
        {
            pickableTransform.localScale = Vector3.one;
            // selectedPickableTransform = null;
            RevertToOriginalItemColor(pickableTransform);
        }

        public void OnPickableTransformMoveStarted(Transform pickableTransform)
        {
            SetItemColor(pickableTransform, new Color(0.5f, 1, 0.5f));
        }

        public void OnPickableTransformMoved(Transform pickableTransform)
        {
            Debug.Log("Moved transform: " + pickableTransform);
        }


        private void SetItemColor(Transform itemTransform, Color color)
        {
            foreach (var itemRenderer in itemTransform.GetComponentsInChildren<Renderer>())
            {
                if (originalItemColorCache.ContainsKey(itemRenderer) == false)
                {
                    originalItemColorCache[itemRenderer] = new List<Color>();
                }
                for (int i = 0; i < itemRenderer.materials.Length; ++i)
                {
                    Material mat = itemRenderer.materials[i];
                    originalItemColorCache[itemRenderer].Add(mat.color);
                    mat.color = color;
                }
            }
        }

        private void RevertToOriginalItemColor(Transform itemTransform)
        {
            foreach (var itemRenderer in itemTransform.GetComponentsInChildren<Renderer>())
            {
                if (originalItemColorCache.ContainsKey(itemRenderer) == true)
                {
                    for (int i = 0; i < itemRenderer.materials.Length; ++i)
                    {
                        itemRenderer.materials[i].color = originalItemColorCache[itemRenderer][i];
                    }
                }
            }
        }


        private IEnumerator AnimateScaleForSelection(Transform pickableTransform)
        {
            float timeAtStart = Time.time;
            const float animationDuration = 0.25f;
            while (Time.time < timeAtStart + animationDuration)
            {
                float progress = (Time.time - timeAtStart) / animationDuration;
                float scaleFactor = 1.0f + Mathf.Sin(progress * Mathf.PI) * 0.2f;
                pickableTransform.localScale = Vector3.one * scaleFactor;
                yield return null;
            }
            pickableTransform.localScale = Vector3.one;
        }
        #region detail messages
        private void OnInputClick(Vector3 clickScreenPosition, bool isDoubleClick, bool isLongTap)
        {
            //Debug.Log("OnInputClick(clickScreenPosition: " + clickScreenPosition + ", isDoubleClick: " + isDoubleClick + ", isLongTap: " + isLongTap + ")");
        }


        private void OnDragStart(Vector3 pos, bool isLongTap)
        {
            //Debug.Log("OnDragStart(pos: " + pos + ", isLongTap: " + isLongTap + ")");
        }

        private void OnFingerDown(Vector3 screenPosition)
        {
            //Debug.Log("OnFingerDown(screenPosition: " + screenPosition + ")");
        }

        private void OnDragUpdate(Vector3 dragPosStart, Vector3 dragPosCurrent, Vector3 correctionOffset)
        {
            //Debug.Log("OnDragUpdate(dragPosStart: " + dragPosStart + ", dragPosCurrent: " + dragPosCurrent + ")");
        }

        private void OnDragStop(Vector3 dragStopPos, Vector3 dragFinalMomentum)
        {
            //Debug.Log("OnDragStop(dragStopPos: " + dragStopPos + ", dragFinalMomentum: " + dragFinalMomentum + ")");
        }

        #endregion
    }
}