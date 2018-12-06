/*
//  Copyright (c) 2015 José Guerreiro. All rights reserved.
//
//  MIT license, see http://www.opensource.org/licenses/mit-license.php
//  
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//  
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//  
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
*/

using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

namespace cakeslice
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Renderer))]
    public class Outline : MonoBehaviour
    {
        public Renderer Renderer { get; private set; }

        public int color;
        public bool eraseRenderer;
        private bool windowActive;
        private GameObject canvas;
        private DetectClicks detectClicks;
        private GameObject activeWindow;
        private Camera cam;
        private RectTransform canvasRect;
        private ScreenTransitionImageEffect screenTrans;

        private void Awake()
        {
            Renderer = GetComponent<Renderer>();
            cam = Camera.main;
            canvas = GameObject.Find("Canvas");
            detectClicks = GameObject.Find("DetectClicks").GetComponent<DetectClicks>();
            canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
            screenTrans = cam.GetComponent<ScreenTransitionImageEffect>();
        }

        void OnEnable()
        {
			IEnumerable<OutlineEffect> effects = Camera.allCameras.AsEnumerable()
				.Select(c => c.GetComponent<OutlineEffect>())
				.Where(e => e != null);

			foreach (OutlineEffect effect in effects)
            {
                effect.AddOutline(this);
            }
        }

        void OnDisable()
        {
			IEnumerable<OutlineEffect> effects = Camera.allCameras.AsEnumerable()
				.Select(c => c.GetComponent<OutlineEffect>())
				.Where(e => e != null);

			foreach (OutlineEffect effect in effects)
            {
                effect.RemoveOutline(this);
            }
        }

        private void OnMouseDown()
        {
            GameObject islandPopup = GameObject.FindGameObjectWithTag("Popup");
            if (islandPopup == null)
            {
                Vector3 islPos = cam.WorldToViewportPoint(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10));

                Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, gameObject.transform.position);

                eraseRenderer = false;
                windowActive = true;
                activeWindow = Instantiate(Resources.Load("Prefabs/UI/Islands/IslandPopup"), islPos, Quaternion.identity) as GameObject;
                activeWindow.transform.SetParent(canvas.transform, false);
                activeWindow.name = "Island Popup" + gameObject.name;
                var actTrans = activeWindow.GetComponent<RectTransform>();
                var titleText = GameObject.FindGameObjectWithTag("IslandTitle").GetComponent<TMP_Text>();
                var descText = GameObject.FindGameObjectWithTag("IslandDesc").GetComponent<TMP_Text>();
                actTrans.position = screenPoint;

                switch (System.Convert.ToInt16(gameObject.name))
                {
                    case 0:
                        titleText.text = "Shop";
                        descText.text = "Spend those coins to upgrade your surfboard here!";
                        actTrans.position = new Vector3(screenPoint.x + 55, screenPoint.y - 145, -10);
                        break;
                    case 1:
                        titleText.text = "First Island";
                        descText.text = "This first island is the first place to race.$$(Tap again to travel)";
                        descText.text = descText.text.Replace('$', '\n');
                        actTrans.position = new Vector3(screenPoint.x + 176, screenPoint.y - 44, -10);
                        break;
                    case 2:
                        titleText.text = "This is the Second Island";
                        descText.text = "This is the second island, where you'll go to race second.";
                        actTrans.position = new Vector3(screenPoint.x - 184, screenPoint.y - 55, -10);
                        break;
                    default:
                        Debug.Log("all");
                        break;
                }
            }

            //go to next Island
            if (windowActive && Input.GetMouseButtonDown(0) && detectClicks.clickedObject)
            {
                if (detectClicks.nameOfHit == gameObject.name)
                {
                    Destroy(activeWindow);
                    StartCoroutine(screenTrans.FadeOut());
                    StartCoroutine(detectClicks.waitForFade(System.Convert.ToInt16(gameObject.name)));
                }
            }
        }
    }
}