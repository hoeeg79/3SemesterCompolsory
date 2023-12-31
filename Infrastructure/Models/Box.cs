﻿namespace Infrastructure.Models;

public class Box
{
    public int boxId { get; set; }
    public string name { get; set; }
    public string size { get; set; }
    public string description { get; set; }
    public float price { get; set; }
    public string boxImgUrl { get; set; }
    public string materials { get; set; }
}