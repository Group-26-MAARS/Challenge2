using System;

[Serializable]
class Plant
{
    public int daysOver { get; set; }
    public double _id { get; set; }
    public DateTime dateOfLastService { get; set; }
    public QrCode qrCode { get; set; }
    public int __v { get; set; }
    public string Status;
   
    public void getStatus()
    {

    }
}
