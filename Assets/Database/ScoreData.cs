using System;
using Unity.VisualScripting.Dependencies.Sqlite;

namespace Database
{
    public class ScoreData
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Core { get; set; }
    }
}