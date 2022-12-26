class DonateurBestelling : iBestelling
{
    public int totaalbedrag { get; set; }
    public Stoel stoelen { get; set; }
    public string zaal { get; set; }
    public DateTime reserveerdatum { get; set; }
    public Voorstelling voorstelling { get; set; }
}