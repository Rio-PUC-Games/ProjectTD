﻿using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class MovingSeedsManager : MonoBehaviour
    {

        public static MovingSeedsManager Instance;

        public Camera Cam;
        public Transform Target;
        public GameObject SeedPrefab;
		public float Speed = 10.0f;
		[Range(0,1)] public float MaxSize;

        private int _seedsToSpawn = 0;
        private MovingSeedUI _seed = null;

        public void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one MovingSeedsManager in scene!");
                return;
            }
            Instance = this;
        }

        // Update is called once per frame
        void Update()
        {
            if(_seedsToSpawn > 0 && _seed == null)
            {
                SpawnSeed();
            }
        }

        private void SpawnSeed()
        {
            _seed = ((GameObject)Instantiate(SeedPrefab, transform.position, Quaternion.identity)).GetComponent<MovingSeedUI>();

            _seed.transform.parent = transform;
            _seed.Manager = this;
            _seed.Cam = Cam;
            _seed.Target = Target;
            _seed.Speed = Speed;
			_seed.MaxSize = MaxSize;

            _seedsToSpawn--;
        }

        public void AddSeeds(int seeds)
        {
            _seedsToSpawn += seeds;
        }
    }
}
