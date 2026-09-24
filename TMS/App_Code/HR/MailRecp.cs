using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MaiRecp
/// </summary>
public class MailRecp
{
    private string _to;
    private string _toName;
    private string _cc;
    private string _bcc;


    public MailRecp()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string To
    {
        get
        {
            return _to;
        }

        set
        {
            _to = value;
        }
    }

    public string ToName
    {
        get
        {
            return _toName;
        }

        set
        {
            _toName = value;
        }
    }

    public string Cc
    {
        get
        {
            return _cc;
        }

        set
        {
            _cc = value;
        }
    }

    public string Bcc
    {
        get
        {
            return _bcc;
        }

        set
        {
            _bcc = value;
        }
    }
}