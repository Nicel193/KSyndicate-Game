using System.Linq;
using CodeBase.Data.Static;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        private const string InitialPointTag = "InitialPoint";
        private const string LevelTransferInitialPointTag = "LevelTransferInitialPoint";
    
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelData = (LevelStaticData) target;

            if (GUILayout.Button("Collect"))
            {
                levelData.enemySpawners = FindObjectsOfType<SpawnMarker>()
                    .Select(x => new EnemySpawnerData(x.GetComponent<UniqueId>().Id, x.EnemyType, x.transform.position))
                    .ToList();

                levelData.LevelKey = SceneManager.GetActiveScene().name;
        
                levelData.playerSpawnPosition = GameObject.FindWithTag(InitialPointTag).transform.position;
        
                // levelData.LevelTransfer.Position = GameObject.FindWithTag(LevelTransferInitialPointTag).transform.position;
            }
      
            EditorUtility.SetDirty(target);
        }
    }
}