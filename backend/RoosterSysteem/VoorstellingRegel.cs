using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Authenticatie;

public class VoorstellingRegel
{
    public string VoorstellingTitel { get; set; }
    public Voorstelling voorstelling { get; set; }
    public Zaal Zaal { get; set; }
    public int Zaalnummer { get; set; }
}