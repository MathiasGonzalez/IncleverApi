using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using System.Text.RegularExpressions;


namespace zipTools
{
    public class zipTools
    {


        //public static void Main(string[] args)
        //{
        //    string base64Encoded = "H4sIAAAAAAAAA0v0CDJIdskv8zEqqIrKzcmOcq%2FI8ck1zUlxtjBNDU03Dgm2MPRz8S0rN%2FfIzE0stgUALnvEljAAAAA%3D";
        //    Console.WriteLine(
        //  Restore(base64Encoded)
        //   );
        //    Console.WriteLine("--------------");
        //    base64Encoded = "H4sIAAAAAAAAA0ut9MrwzMrPLDd1zfRx9jLxzCpIiogwsAUAyo56xhgAAAA=";
        //    string base64Decoded = "";
        //    Console.WriteLine(
        //       Restore(base64Encoded)
        //        );
        //    Console.WriteLine("--------------");
        //    Console.WriteLine(base64Decoded);

        //    Console.Read();
        //}

       public  static string Restore(string str)
        {
            str = Uri.UnescapeDataString(Uri.UnescapeDataString(str));
            str = Decompress(str);
            return DecodeUTF(str);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="base64Encoded"></param>
        /// <returns></returns>
        public static string Decode(string base64Encoded)
        {
            byte[] data = Convert.FromBase64String(base64Encoded);
            string base64Decoded = ASCIIEncoding.ASCII.GetString(data);
            return base64Decoded;
        }
        public static string DecodeUTF(string base64Encoded)
        {
            byte[] data = Convert.FromBase64String(base64Encoded);
            string base64Decoded = UTF8Encoding.UTF8.GetString(data);
            return base64Decoded;
        }
        public static string Decompress(string str)
        {
            MemoryStream outputStream = new MemoryStream();

            //str = Uri.UnescapeDataString(Uri.EscapeDataString(Decode("H8KLCAAAAAAAAAPDiygpKcKww5LDl8OPKk7Di0xJw4lJw5XDi0stw5HCt8Ksw7Qww7fDlTc1McOWBwDDq8KfDDoeAAAA")));


            string result = string.Empty;
            if (!IsBase64String(str))
            {
                throw new Exception("!IsBase64String");       
            }
            using (Stream stream = new MemoryStream(Convert.FromBase64String(str)))
            using (GZipStream gZip = new GZipStream(stream, CompressionMode.Decompress))
            {
                gZip.CopyTo(outputStream);
            }

            byte[] decompressedBuffer = outputStream.ToArray();
            //result = ASCIIEncoding.ASCII.GetString(decompressedBuffer);
            result = Encoding.UTF8.GetString(decompressedBuffer);
            return result;
        }
        public static bool IsBase64String(string s)
        {
            s = s.Trim();
            return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

        }
        /*
     // Get some base64 encoded binary data from the server. Imagine we got this:

var object={};
  var chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=';

  function InvalidCharacterError(message) {
    this.message = message;
  }
  InvalidCharacterError.prototype = new Error;
  InvalidCharacterError.prototype.name = 'InvalidCharacterError';

  // encoder
  // [https://gist.github.com/999166] by [https://github.com/nignag]
 
  object.btoa = function (input) {
    var str = String(input);
    for (
      // initialize result and counter
      var block, charCode, idx = 0, map = chars, output = '';
      // if the next str index does not exist:
      //   change the mapping table to "="
      //   check if d has no fractional digits
      str.charAt(idx | 0) || (map = '=', idx % 1);
      // "8 - idx % 1 * 8" generates the sequence 2, 4, 6, 8
      output += map.charAt(63 & block >> 8 - idx % 1 * 8)
    ) {
      charCode = str.charCodeAt(idx += 3/4);
      if (charCode > 0xFF) {
        throw new InvalidCharacterError("'btoa' failed: The string to be encoded contains characters outside of the Latin1 range.");
      }
      block = block << 8 | charCode;
    }
    return output;
  };

  // decoder
  // [https://gist.github.com/1020396] by [https://github.com/atk]
 
  object.atob = function (input) {
    var str = String(input).replace(/=+$/, '');
    if (str.length % 4 == 1) {
      throw new InvalidCharacterError("'atob' failed: The string to be decoded is not correctly encoded.");
    }
    for (
      // initialize result and counters
      var bc = 0, bs, buffer, idx = 0, output = '';
      // get next character
      buffer = str.charAt(idx++);
      // character found in table? initialize bit storage and add its ascii value;
      ~buffer && (bs = bc % 4 ? bs * 64 + buffer : buffer,
        // and if not first of each 4 characters,
        // convert the first 8 bits to one ascii character
        bc++ % 4) ? output += String.fromCharCode(255 & bs >> (-2 * bc & 6)) : 0
    ) {
      // try to find character in table (0-63, not found => -1)
      buffer = chars.indexOf(buffer);
    }
    return output;
  };



var xx="http://jsfiddle.net/9yH7M/543/ñ♫";

var ziped =pako.gzip(xx,{to:"base64"}); 
var stringZiped=pako.gzip("http://jsfiddle.net/9yH7M/543/ñññ",{to:"string"});
//console.log(ziped);
//console.log(stringZiped);
//console.log(object.btoa(stringZiped));
var enc=object.btoa(unescape(encodeURIComponent(stringZiped)));
//console.log(enc);
//console.log(object.atob(enc));
//console.log(escape(object.atob(enc)));
//console.log(decodeURIComponent(escape(object.atob(enc))));
console.log(unescape(encodeURIComponent(xx)));
console.log(object.btoa(unescape(encodeURIComponent(xx))));
console.log(pako.gzip(object.btoa(unescape(encodeURIComponent(xx))),{to:"string"}));

console.log(
encodeURIComponent(
object.btoa(pako.gzip(object.btoa(unescape(encodeURIComponent(xx))),{to:"string"}))
));

H4sIAAAAAAAAA0v0CDJIdskv8zEqqIrKzcmOcq%2FI8ck1zUlxtjBNDU03Dgm2MPRz8S0rN%2FfIzE0stgUALnvEljAAAAA%3D
        
        */
    }
}
