using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FdDecode
{
    public class ContentDecode : Fiddler.IAutoTamper
    {
        private HeaderSecurity security;
        public ContentDecode()
        {
            security = new HeaderSecurity();
        }
        void Fiddler.IAutoTamper.AutoTamperRequestAfter(Fiddler.Session oSession)
        {
          
        }

        void Fiddler.IAutoTamper.AutoTamperRequestBefore(Fiddler.Session oSession)
        {

        }

        void Fiddler.IAutoTamper.AutoTamperResponseAfter(Fiddler.Session oSession)
        {
            return;
            string uuId = oSession.oResponse.headers["CLIENT-UUID"];
            if (string.IsNullOrEmpty(uuId))
            {
                return;
            }
            string retString = oSession.GetResponseBodyAsString();
            string regString = "CDATA\\[(?<value>(.|\n)*?)\\]";
            Match match = Regex.Match(retString, regString);
            if (match.Success)
            {
                string data = match.Groups["value"].Value;
                if (string.IsNullOrEmpty(data))
                {
                    return;
                }

                if (data.Trim().StartsWith("{\""))
                {
                    //
                    return;
                }
                string decodeData = this.security.Decrypt(data, uuId);
                oSession.utilSetResponseBody(retString + "\r\nJSON:" + decodeData);

            }

            //this.security.Decrypt(retString, uuId);

            //oSession.utilSetResponseBody("123" + uuId);
        }

        void Fiddler.IAutoTamper.AutoTamperResponseBefore(Fiddler.Session oSession)
        {
            string uuId = oSession.oRequest.headers["CLIENT-UUID"];
            if (string.IsNullOrEmpty(uuId))
            {
                return;
            }
            string retString = oSession.GetRequestBodyAsString();
            string regString = "CDATA\\[(?<value>(.|\n)*?)\\]";
            Match match = Regex.Match(retString, regString);
            if (match.Success)
            {
                string data = match.Groups["value"].Value;
                if (string.IsNullOrEmpty(data))
                {
                    return;
                }
                if (data.Trim().StartsWith("{\""))
                {
                    //
                    return;
                }
                string decodeData = this.security.Decrypt(data, uuId);
                oSession.utilSetRequestBody(retString + "\r\nJSON:" + decodeData);

            }
        }

        void Fiddler.IAutoTamper.OnBeforeReturningError(Fiddler.Session oSession)
        {

        }

        void Fiddler.IFiddlerExtension.OnBeforeUnload()
        {

        }

        void Fiddler.IFiddlerExtension.OnLoad()
        {

        }
    }
}
