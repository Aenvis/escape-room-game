using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class Switch : MonoBehaviour
    {
        public GameObject[] background;
        private int index;
        private float update;
        private void Start()
        {
            update = 0.0f;
            index = 0;
        }

        private void Update()
        {
            update += Time.deltaTime;
            for (int i = 0; update > 5.0f;)
                {
                    index += 1;
                    update = 0.0f;
                    background[i].gameObject.SetActive(false);
                    background[index].gameObject.SetActive(true);

                    if (i >= 5)
                        index = 0;
                    if (i < 0)
                        index = 5;

                    UnityEngine.Debug.Log("Update Image");
                }
            

        }
    }
}
