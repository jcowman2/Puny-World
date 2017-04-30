using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TextUtils : MonoBehaviour {

	public static string SeparateCamelCase(string str) {
        if (string.IsNullOrEmpty(str)) {
            return str;
        }

        StringBuilder strb = new StringBuilder();
        strb.Append(str[0]);

        for (int i = 1; i < str.Length; i++) {
            if (char.IsUpper(str[i]) && str[i - 1] != ' ') {
                strb.Append(' ');
            }
            strb.Append(str[i]);
        }

        return strb.ToString();
    }

    public static string PutCommasInNumber(string num) {
        if (string.IsNullOrEmpty(num) || num.Length <= 3) {
            return num;
        }

        int offset = num.Length % 3;
        StringBuilder strb = new StringBuilder();
        strb.Append(num.Substring(0,offset));

        for (int i = offset; i < num.Length; i += 3) {
            strb.Append(",").Append(num.Substring(i, 3));
        }

        return strb.ToString();
  
    }

    public static string PutCommasInNumber(int num) {
        return PutCommasInNumber(num.ToString());
    }

}
