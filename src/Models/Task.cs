﻿namespace Models;

public class Tasks
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<Record> Records { get; set; }
}