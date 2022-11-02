using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class BackgroundIterator : MonoBehaviour
    {
        public GameObject[] background;
        private int m_currIndex = 0;
        private int m_prevIndex = 0;
        private float m_update = 0.0f;
       
        //CR: 
        // Bartek, your for loop worked as a if statement (the current one)
        // also, you've mistaken in-loop-declared i with class field index 
        // new version now follows the current and the previous scene and sets their gameObjects to proper values
        // i used Time.fixedDeltaTime instead of Time.deltaTime to preserve time step (Time.deltaTime depends on ur FPS)
        // we check if m_currIndex == 5 instead of == 6, because size of []background is 5, so last element is 5-1 = 4
        
        //Optional TODO: try to blend the images using alpha value (Background[i] -> Image -> Color)
        private void Update()
        {
            m_update += Time.fixedDeltaTime;
            if (m_update < 30.0f) return;

            m_currIndex++;
            m_prevIndex = m_currIndex - 1;
            if (m_currIndex == 5)
            {
                m_currIndex = 0;
                m_prevIndex = 4;
            }
            
            m_update = 0.0f;
            background[m_prevIndex].gameObject.SetActive(false);
            background[m_currIndex].gameObject.SetActive(true);
        }
    }
}
