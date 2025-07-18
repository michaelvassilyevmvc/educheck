﻿namespace vassilyev.EduCheckV2App.WebAPI.Entities;

public class Topic
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public List<Question> Questions { get; set; } = new();
}
