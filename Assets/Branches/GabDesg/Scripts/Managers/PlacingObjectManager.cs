﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingObjectManager : Flow {

    #region Singleton
    private static PlacingObjectManager instance;
    public static PlacingObjectManager Instance {
        get {
            return instance ?? (instance = new PlacingObjectManager());
        }
    }
    #endregion

    private ShopManager shopManager;
    private GridManager gridManager;
    private MapInfoPck mapInfoPck;

    public Vector3 tileSelected;
    private Vector3 oldTileSelected;

    public override void EndFlow() {
        base.EndFlow();
    }
    public override void PreInitialize() {
        base.PreInitialize();

        this.shopManager = ShopManager.Instance;
        this.gridManager = GridManager.Instance;
        this.mapInfoPck = MapInfoPck.Instance;
    }

    public override void Initialize() {
        base.Initialize();
        this.oldTileSelected = new Vector3(999, 999, 999);
    }

    public override void PhysicsRefresh() {
        base.PhysicsRefresh();
    }

    public override void Refresh() {
        base.Refresh();
    }

    public void MoveObj(GameObject obj) {
        //Check if ray cast hits game board
        Vector3? tileHitOnTable;
        if (this.gridManager.LookForHitOnTables(out tileHitOnTable)) {
            //Make sure obj is activated
            if (!obj.activeSelf)
                obj.SetActive(true);

            //Check if tile selected changed
            UpdateTileSelected((Vector3)tileHitOnTable);
            if (HasTileChanged()) {
                //Update SFX/VFX/Object position
                TileHasChanged();
            }
        }
        else {
            //Make sure obj isnt deactivated already
            if (obj.activeSelf)
                //If item in hands + not aiming at table, deactivate obj
                obj.SetActive(false);
        }
    }

    private void TileHasChanged() {
        //-----------------TODO--------------------------
        //Play sounds

        //Move obj selected
        this.shopManager.MoveSelectedObj(this.tileSelected);

        //Check if obj is placeable there
        bool isTileTaken = !IsObjectPlaceableThere();
        if (isTileTaken) {
            //-----------------TODO--------------------------
            //Make obj red

            //-----------------TODO--------------------------
            SetTileSidesColor(isTileTaken);
        }
    }

    public bool IsObjectPlaceableThere() {
        bool tileIsTaken = true;

        //Check in the MapInfoPck if tile is already taken
        ushort? tileSelectedId = this.gridManager.GetTileId(this.tileSelected);
        if (tileSelected != null) {
            Vector2 tileCoords = this.gridManager.GetTileCoordsFromTileId("MapRoom", (ushort)tileSelectedId);
            if (this.mapInfoPck.TileTowerInfos.ContainsKey(tileCoords))
                tileIsTaken = false;
            else if (this.mapInfoPck.TileTrapInfos.ContainsKey(tileCoords))
                tileIsTaken = false;
        }
        return tileIsTaken;
    }

    private void UpdateTileSelected(Vector3 hitPointInWorld) {
        this.tileSelected = this.gridManager.GetTileCenterFromCoords(this.gridManager.GetTileCoords(hitPointInWorld));
        this.tileSelected.y = this.gridManager.GetGrid("ShopRoom").StartPoint.y;
    }

    private bool HasTileChanged() {
        bool tileHasChanged = false;

        if (this.tileSelected != this.oldTileSelected) {
            tileHasChanged = true;
            this.oldTileSelected = this.tileSelected;
        }

        return tileHasChanged;
    }

    private void SetTileSidesColor(bool tileIsTaken) {
        //Get tile

        //Set tile sides color

    }
}

