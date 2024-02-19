﻿using TabloidFullStack.Models;
using TabloidFullStack.Repositories;
using TabloidFullStack.Utils;

public class TagRepository : BaseRepository, ITagRepository
{
    public TagRepository(IConfiguration configuration) : base(configuration) { }
    public List<Tag> GetAll()
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                        SELECT Id, Name
                        FROM Tag
                        ORDER BY Name";
                var reader = cmd.ExecuteReader();
                var tags = new List<Tag>();
                while (reader.Read())
                {
                    tags.Add(new Tag()
                    {
                        Id = DbUtils.GetInt(reader, "Id"),
                        Name = DbUtils.GetString(reader, "Name")
                    });
                }
                reader.Close();
                return tags;
            }
        }
    }
}
