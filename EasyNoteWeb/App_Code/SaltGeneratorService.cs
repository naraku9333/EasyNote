/***********************************************************************************************
Class SaltGeneratorService

SaltGeneratorService creates a WebMethod in the GenerateSaltString method to create a random string
of bytes that can be used for hashing with a password.  

************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for SaltGeneratorService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class SaltGeneratorService : System.Web.Services.WebService
{

    public SaltGeneratorService()
    {
    }


    /******************************New for Assignment 5 ********************************
    * FUNCTION:  public string GenerateSaltString()
    *
    * ARGUMENTS: none
    *
    * RETURNS:   This function returns a string which represents a series of random bytes.  
    *
    * NOTES:     The GenerateSaltString method is a web method that generates a 16 byte long
    *            salt and converts this into a string to be returned.  
    **************************************************************************************/
    [WebMethod(Description="Returns a salt string generated from a RNGCryptoServiceProvider")]
    public static string GenerateSaltString()
    {
        RNGCryptoServiceProvider prng = new RNGCryptoServiceProvider();         //The Pseudo Random Number Generator for the salt.  
        byte[] saltArray = new byte[50];

        prng.GetBytes(saltArray);
        return Encoding.Default.GetString(saltArray);
    }

}
