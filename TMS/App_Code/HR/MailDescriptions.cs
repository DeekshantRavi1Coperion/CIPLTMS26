using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MailDescriptions
/// </summary>
public class MailDescriptions
{

    private string _subject;
    private string _salutation;
    private string _body;
    private string _url;
    private string _closing;
    private string _signature;

    public MailDescriptions()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string Subject
    {
        get
        {
            return _subject;
        }

        set
        {
            _subject = value;
        }
    }

    public string Salutation
    {
        get
        {
            return _salutation;
        }

        set
        {
            _salutation = value;
        }
    }

    public string Body
    {
        get
        {
            return _body;
        }

        set
        {
            _body = value;
        }
    }

    public string Url
    {
        get
        {
            return _url;
        }

        set
        {
            _url = value;
        }
    }

    public string Closing
    {
        get
        {
            return _closing;
        }

        set
        {
            _closing = value;
        }
    }

    public string Signature
    {
        get
        {
            return _signature;
        }

        set
        {
            _signature = value;
        }
    }
}