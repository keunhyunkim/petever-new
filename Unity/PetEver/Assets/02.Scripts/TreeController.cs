using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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

        public GameObject treePrefab;
        private GameObject treeDetailUIPanel;
        private CanvasGroup treePopupCanvasGroup;

        private TouchInputController touchInputController;

        private MobileTouchCamera mobileTouchCamera;

        private MobilePickingController mobilePickingController;

        private Camera cam;

        private Transform selectedPickableTransform;
        private Dictionary<Renderer, List<Color>> originalItemColorCache = new Dictionary<Renderer, List<Color>>();

        private void Awake()
        {
            Application.targetFrameRate = 60;

            cam = GameObject.Find("Main Camera").GetComponent<Camera>();
            mobileTouchCamera = cam.GetComponent<MobileTouchCamera>();
            touchInputController = cam.GetComponent<TouchInputController>();
            mobilePickingController = cam.GetComponent<MobilePickingController>();



            #region detail callbacks
            touchInputController.OnInputClick += OnInputClick;
            touchInputController.OnDragStart += OnDragStart;
            touchInputController.OnDragStop += OnDragStop;
            touchInputController.OnDragUpdate += OnDragUpdate;
            touchInputController.OnFingerDown += OnFingerDown;
            #endregion

        }

        void Start()
        {

            treePopupCanvasGroup = GameObject.Find("TreePopupPannel").GetComponent<CanvasGroup>();
            manCharacter = GameObject.FindGameObjectWithTag("Owner");
            treeZone = GameObject.Find("TreeZone2");
            exitBtn = GameObject.Find("ExitBtn");
            exitBtn.GetComponent<Button>().onClick.AddListener(() =>
            {
                ExitTreePopup();
            });
            popupBack = GameObject.Find("TreePopupPannel");
            popupBack.GetComponent<Button>().onClick.AddListener(() =>
            {
                ExitTreePopup();
            });
            mainCanvas = GameObject.FindGameObjectWithTag("UICanvas");
            createTreeBtn = GameObject.Find("Plus");
            createTreeBtn.GetComponent<Button>().onClick.AddListener(() =>
            {
                createTree();
            });

        }
        void ShowTreeDetail()
        {
            treePopupCanvasGroup.alpha = 1;
            treePopupCanvasGroup.interactable = true;
            treePopupCanvasGroup.blocksRaycasts = true;
        }

        void ExitTreePopup()
        {
            treePopupCanvasGroup.alpha = 0;
            treePopupCanvasGroup.interactable = false;
            treePopupCanvasGroup.blocksRaycasts = false;
        }


        void createTree()
        {
            createTreeBtn.SetActive(false);
            treeZone.SetActive(false);
            Vector3 pos = treeZone.transform.position;
            GameObject newTree = Instantiate(treePrefab);
            newTree.transform.SetParent(GameObject.Find("TreeArea").transform);
            newTree.transform.position = pos;
        }


        public void OnPickItem(RaycastHit hitInfo)
        {
            Debug.Log("OnPickItem a collider: " + hitInfo.collider);
        }

        public void OnPickableTransformSelected(Transform pickableTransform)
        {
           
        }

        public void OnPickableTransformSelectedExtended(PickableSelectedData data)
        {
            if (data.IsLongTap)
            {
                 ShowTreeDetail();
            } else {
                Debug.Log("OnPickableTransformSelectedExtended() - SelectedTransform: " + data.SelectedTransform + ", IsLongTap: " + data.IsLongTap);
                if (data.SelectedTransform != selectedPickableTransform)
                {
                    StartCoroutine(AnimateScaleForSelection(data.SelectedTransform));
                }
                SetItemColor(data.SelectedTransform, Color.green);
                selectedPickableTransform = data.SelectedTransform;
            }
        }

        public void OnPickableTransformDeselected(Transform pickableTransform)
        {
            pickableTransform.localScale = Vector3.one;
            selectedPickableTransform = null;
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

        public void OnPickableTransformMoveEnded(Vector3 startPos, Transform pickableTransform)
        {
            SetItemColor(pickableTransform, Color.green);
            if (GetTransformPositionValid(pickableTransform) == false)
            {
                pickableTransform.position = startPos;
            }
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

        /// <summary>
        /// Method to check whether another MobileTouchPickable has the exact same position as the given transform.
        /// NOTE: This is a demo implementation that makes use of slow unity function calls.
        /// </summary>
        private bool GetTransformPositionValid(Transform pickableTransform)
        {

            //Expensive call. Should be optimized in live environments.
            List<MobileTouchPickable> allPickables = new List<MobileTouchPickable>(FindObjectsOfType<MobileTouchPickable>());

            allPickables.RemoveAll(item => item.PickableTransform == pickableTransform);
            foreach (var pickable in allPickables)
            {
                if (mobileTouchCamera.CameraAxes == CameraPlaneAxes.XY_2D_SIDESCROLL)
                {
                    if (Mathf.Approximately(pickableTransform.position.x, pickable.PickableTransform.position.x) && Mathf.Approximately(pickableTransform.position.y, pickable.PickableTransform.position.y))
                    {
                        return (false);
                    }
                }
                else
                {
                    if (Mathf.Approximately(pickableTransform.position.x, pickable.PickableTransform.position.x) && Mathf.Approximately(pickableTransform.position.z, pickable.PickableTransform.position.z))
                    {
                        return (false);
                    }
                }
            }
            return (true);
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