﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ShaderAfterShader : MonoBehaviour {

    //readonly string TAG = "ShaderAfterShader: ";

    #region Public Variables

    [SerializeField]
    private Transform player = null;
    [SerializeField]
    private RenderTexture rt = null;
    [SerializeField]
    private List<Material> materials = null;
    [SerializeField]
    private RenderTexture finalRt = null;
    [SerializeField]
    private RenderTexture bufferRt = null;
    [SerializeField]
    private Vector2 centerOffset = Vector2.zero;
    [SerializeField]
    private Vector2 centerOffsetWichRotates = Vector2.zero;
    [SerializeField]
    private float viewWidth = 1.0f;
    [SerializeField]
    private bool showInColor = false;
    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float intensity = 0;

    #endregion

    #region Overrided Methods

    private void Update() {
        Vector3 playerPos = Camera.main.WorldToScreenPoint(player.position);
        Vector3 mousePos = Input.mousePosition;
        Vector4 plPos = new Vector4(playerPos.x / Camera.main.pixelWidth, playerPos.y / Camera.main.pixelHeight, 0, 0);
        Vector4 lkPos = new Vector4(mousePos.x / Camera.main.pixelWidth, mousePos.y / Camera.main.pixelHeight, 0, 0);
        Vector4 diff4 = lkPos - plPos;
        Vector2 diff = new Vector2(diff4.x, diff4.y);
        float angle = (float)Mathf.Atan2(diff.y, diff.x);

        Vector4 centerOffset4 = new Vector4(centerOffset.x, centerOffset.y);
        Vector4 centerOffsetWichRotates4 = new Vector4(centerOffsetWichRotates.x, centerOffsetWichRotates.y);

        Vector4 resolution = new Vector4(Camera.main.pixelWidth, Camera.main.pixelHeight);

        materials[0].SetVector("_PlayerPos", plPos);
        materials[0].SetVector("_LookPos", lkPos);
        materials[0].SetFloat("_LookAngle", angle);
        materials[0].SetVector("_Resolution", resolution);
        materials[0].SetFloat("_ViewWidth", viewWidth);
        materials[0].SetVector("_Offset", centerOffset4);
        materials[0].SetVector("_OffsetRot", centerOffsetWichRotates4);
        materials[0].SetFloat("_ShowInColor", showInColor ? 1.0f : 0.0f);
        materials[0].SetFloat("_Intensity", intensity);

        materials[1].SetVector("_PlayerPos", plPos);
        materials[1].SetVector("_LookPos", lkPos);
        materials[1].SetFloat("_LookAngle", angle);
        materials[1].SetVector("_Resolution", resolution);
        materials[1].SetFloat("_ViewWidth", viewWidth);
        materials[1].SetVector("_Offset", centerOffset4);
        materials[1].SetVector("_OffsetRot", centerOffsetWichRotates4);
        materials[1].SetFloat("_ShowInColor", showInColor ? 1.0f : 0.0f);
        materials[1].SetFloat("_Intensity", intensity);

        Graphics.Blit(rt, bufferRt, materials[0]);
        Graphics.Blit(bufferRt, finalRt, materials[1]);
    }

    private void OnDestroy() {
        materials[0].SetVector("_PlayerPos", Vector2.zero);
        materials[0].SetVector("_LookPos", Vector2.zero);
        materials[0].SetFloat("_LookAngle", 0);
        materials[0].SetVector("_Resolution", Vector2.zero);
        materials[0].SetFloat("_ViewWidth", 0);
        materials[0].SetVector("_Offset", Vector2.zero);
        materials[0].SetVector("_OffsetRot", Vector2.zero);
        materials[0].SetFloat("_ShowInColor", 0);
        materials[0].SetFloat("_Intensity", intensity);

        materials[1].SetVector("_PlayerPos", Vector2.zero);
        materials[1].SetVector("_LookPos", Vector2.zero);
        materials[1].SetFloat("_LookAngle", 0);
        materials[1].SetVector("_Resolution", Vector2.zero);
        materials[1].SetFloat("_ViewWidth", 0);
        materials[1].SetVector("_Offset", Vector2.zero);
        materials[1].SetVector("_OffsetRot", Vector2.zero);
        materials[1].SetFloat("_ShowInColor", 0);
        materials[1].SetFloat("_Intensity", intensity);
    }

    #endregion
}
