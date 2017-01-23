﻿using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class Node : MonoBehaviour {
        #region Variables
        public Color HoverColor;
        public Color NotEnoughMoneyColor;
        public Vector3 PositionOffset;
        [HideInInspector]
        public GameObject Turret;
        [HideInInspector]
        public TurretBlueprint TurretBlueprint;
        [HideInInspector]
        public bool IsUpgraded = false;

        private Renderer _rend;
        private Color _startColor;
        private BuildManager _buildManager;
        #endregion
        
        #region Properties
        public Vector3 GetBuildPosition { get { return transform.position + PositionOffset; } }
        #endregion

        public void Start()
        {
            _rend = GetComponent<Renderer>();
            _startColor = _rend.material.color;
            _buildManager = BuildManager.Instance;
        }

       
        public void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;//checar se o mouse não tá na IU

            if (Turret != null) {//se já existe um turret no node, seleciona o node 
                _buildManager.SelectNode(this);
                return;
            }

            if (!_buildManager.CanBuild)
                return;

            //_buildManager.BuildTorrentOn(this);
            BuildTurret(_buildManager.GetTurretToBuild);
        }

        public void OnMouseEnter()
        {

            if (EventSystem.current.IsPointerOverGameObject())
                return;//checar se o mouse não tá na IU

            if (!_buildManager.CanBuild)
                return;

            if (_buildManager.HasEnoughMoney)
            {
                _rend.material.color = HoverColor;
            }
            else
            {
                _rend.material.color = NotEnoughMoneyColor;
            }

        }

        public void OnMouseExit()
        {
            _rend.material.color = _startColor;
        }

        private void BuildTurret(TurretBlueprint blueprint)
        {
            if (PlayerStats.Money < blueprint.Cost)
            {
                Debug.Log("Not enough money");
                return;
            }
            PlayerStats.Money -= blueprint.Cost;

            GameObject turret = (GameObject)Instantiate(blueprint.Prefab, GetBuildPosition, Quaternion.identity);
            Turret = turret;

            TurretBlueprint = blueprint;

            if (_buildManager.BuildParticleEffectPrefab != null)
            {
                GameObject effect =
                    (GameObject)Instantiate(_buildManager.BuildParticleEffectPrefab, GetBuildPosition, Quaternion.identity);
                Destroy(effect, 5f);
            }

            Debug.Log("Turret built!");
        }

        public void UpgradeTurret()
        {
            if (PlayerStats.Money < TurretBlueprint.UpgradeCost)
            {
                Debug.Log("Not enough money to upgrade");
                return;
            }
            PlayerStats.Money -= TurretBlueprint.UpgradeCost;

            //Get rid of the old turret
            Destroy(Turret);

            //Build the upgraded version
            GameObject turret = (GameObject)Instantiate(TurretBlueprint.UpgradedPrefab, GetBuildPosition, Quaternion.identity);
            Turret = turret;

            if (_buildManager.BuildParticleEffectPrefab != null)
            {
                GameObject effect =
                    (GameObject)Instantiate(_buildManager.BuildParticleEffectPrefab, GetBuildPosition, Quaternion.identity);
                Destroy(effect, 5f);
            }

            IsUpgraded = true;

            Debug.Log("Turret upgraded!");
        }

        public void SellTurret()
        {
            PlayerStats.Money += TurretBlueprint.GetSellCost();
            //todo: spawn effect

            if (_buildManager.SellParticleEffectPrefab != null)
            {
                GameObject effect =
                    (GameObject)Instantiate(_buildManager.SellParticleEffectPrefab, GetBuildPosition, Quaternion.identity);
                Destroy(effect, 5f);
            }

            Destroy(Turret);
            TurretBlueprint = null;
        }
        
    }
}
