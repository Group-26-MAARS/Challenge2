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
        // Locate the Plant with the ID
    }

    public Plant createNewPlant(Plant tempReturn)
    {
        // gernerate ID make call to get webWithID
        // add it to the Vuforia Database
        // Add it to the current List

        return tempReturn;
    }
}
