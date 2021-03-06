﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FeedbackIconType
{
    sad, happy, sleep
}

public class UI_floatingIcon : MonoBehaviour {

    public float moveSpeed;
    public float timeToDelete;
    public RectTransform objectTransform;
    public Sprite spriteIcon;
    public Canvas canvas;
    public FeedbackIconType whichIcon; 

    // Use this for initialization
    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        transform.SetParent(canvas.transform, false);

        Invoke("Delete", timeToDelete);
        objectTransform = gameObject.GetComponent<RectTransform>();
        
    }

    // Update is called once per frame
    void Update()

    {
        objectTransform.position = new Vector3(transform.position.x, transform.position.y + (moveSpeed * Time.deltaTime), transform.position.z);
    }

    private void Delete()
    {
        Destroy(gameObject);
    }

    
}
