using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using backend.Authenticatie;
public class Voorstelling
{
    public Voorstelling() { }
    public Voorstelling(string titel, string omschrijving, string image)
    {
        VoorstellingTitel = titel;
        Omschrijving = omschrijving;
        Image = image;
    }
    [Key]
    public int VoorstellingId { get; set; }
    public string VoorstellingTitel { get; set; }
    public string Image { get; set; }
    public int leeftijd { get; set; }
    public string Omschrijving { get; set; }

    [JsonIgnore]
    public List<Show> Shows { set; get; }
}