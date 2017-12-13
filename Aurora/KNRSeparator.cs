using System;
using System.Collections;

namespace Utilities
{
    class KNRSeparator
    {
        static public ArrayList sentenceSeparate(string sentenceName)
        {
            ArrayList arr = new ArrayList();
            string tmp = "";
            try
            {
                for (int i = 0; i < sentenceName.Length; i++)
                {
                    if (isAlpha(sentenceName[i]))
                    {
                        if (isUpper(sentenceName[i]))
                        {
                            if (!tmp.Equals(""))
                            {
                                arr.Add(tmp);
                            }
                            tmp = "";
                        }
                        tmp += sentenceName[i];
                    }
                    else if (!tmp.Equals(""))
                    {
                        arr.Add(tmp);
                        tmp = "";
                    }
                }
                return arr;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }

        static private bool isAlpha(char ch)
        {
            if (ch >= 'a' && ch <= 'z' || ch >= 'A' && ch <= 'Z')
            {
                return true;
            }
            return false;
        }

        static private bool isUpper(char ch)
        {
            if (!(ch >= 'a' && ch <= 'z'))
            {
                return true;
            }
            return false;
        }
    }
}