using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

namespace Database
{
    public class SqLiteGameDb : MonoBehaviour
    {
        void Awake() {
            DontDestroyOnLoad(gameObject);
        }

        public void AddToDb(int score)
        {
            var dbPath = Path.Combine(Application.persistentDataPath, "game.db");
            using var conn = new SQLiteConnection(dbPath);
            conn.CreateTable<ScoreData>();
            var newScore = new ScoreData
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                Core = score
            };

            conn.InsertOrReplace(newScore);
        }
    }
}