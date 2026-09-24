using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class BOMLine
{
    public int Pid { get; set; }
    public int BomFid { get; set; }
    public string ProductCode { get; set; }
    public string ProductDescription { get; set; }
    public string AdditionalDescription { get; set; }
    public string Uom { get; set; }
    public double Quantity { get; set; }

    public BOMLine(int pid
                 , int bomFid
                 , string productCode
                 , string productDescription
                 , string additionalDescription
                 , string uom
                 , double quantity)
    {
        Pid = pid;
        BomFid = bomFid;
        ProductCode = productCode;
        ProductDescription = productDescription;
        AdditionalDescription = additionalDescription;
        Uom = uom;
        Quantity = quantity;        
    }
}