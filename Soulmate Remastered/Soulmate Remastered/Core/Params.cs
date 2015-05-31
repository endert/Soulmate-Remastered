using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soulmate_Remastered.Core
{
    /// <summary>
    /// Parameter struct for implicit casting between all primitiv data types
    /// class because this way it can throw a Exception
    /// </summary>
    class Params
    {
        /// <summary>
        /// the String List with the Parameter
        /// </summary>
        List<String> parms;

        /// <summary>
        /// bool if the Params are empty or not
        /// </summary>
        public bool IsEmpty { get { return parms.Count == 0; } }

        /// <summary>
        /// number of elements
        /// </summary>
        public int Count { get { return parms.Count; } }

        /// <summary>
        /// initialize empty Params
        /// </summary>
        public Params()
        {
            parms = new List<string>();
        }

        public Params(Object o) : this()
        {
            add(o);
        }

        public Params(Object[] oa) : this()
        {
            add(oa);
        }

        /// <summary>
        /// add a parameter
        /// </summary>
        /// <param name="o">added obj as parameter</param>
        public void add(Object o)
        {
            parms.Add(o.ToString());
            normalize();
        }

        /// <summary>
        /// add a object list as parameters
        /// </summary>
        /// <param name="oa"></param>
        public void add(Object[] oa)
        {
            foreach (Object o in oa)
            {
                parms.Add(o.ToString());
            }
            normalize();
        }

        /// <summary>
        /// simplify the List, helps with casting a lot
        /// </summary>
        private void normalize()
        {
            String completeParams = "";

            for (int i = 0; i < parms.Count; ++i)
            {
                completeParams += parms[i] + " ";
            }

            completeParams = completeParams.Replace("[", "");
            completeParams = completeParams.Replace("]", "");
            completeParams = completeParams.Replace(",", "");

            String[] res = completeParams.Split(' ');

            parms = new List<string>();

            foreach (String s in res)
            {
                if (!s.Equals(""))
                    parms.Add(s);
            }
        }

        //Casts *********************************************************************

        /// <summary>
        /// casting the Params to one float, if there are more then one entry throw an Exception
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static implicit operator float(Params p)
        {
            if (p.parms.Count != 1)
                throw new FormatException();

            return Convert.ToSingle(p.parms[0]);
        }

        /// <summary>
        /// casting the Params to one double, if there are more then one entry throw FormatException
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static implicit operator double(Params p)
        {
            if (p.parms.Count != 1)
                throw new FormatException();

            return Convert.ToDouble(p.parms[0]);
        }

        /// <summary>
        /// casting the Params to one int, if there are more then one entry throw FormatException
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static implicit operator int(Params p)
        {
            if (p.parms.Count != 1)
                throw new FormatException();

            return Convert.ToInt32(p.parms[0]);
        }

        /// <summary>
        /// casting the Params to one Vector, if there are more then two entrys throw FormatEception
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static implicit operator Vector2(Params p)
        {
            if (p.parms.Count != 2)
                throw new FormatException();

            return new Vector2(Convert.ToSingle(p.parms[0]), Convert.ToSingle(p.parms[1]));
        }

        /// <summary>
        /// returns the ToString Method
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static implicit operator String(Params p)
        {
            return p.ToString();
        }

        /// <summary>
        /// casting the Params to a float Array
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static implicit operator float[](Params p)
        {
            List<float> res = new List<float>();

            foreach (Object o in p.parms)
                res.Add(Convert.ToSingle(o));

            return res.ToArray();
        }

        /// <summary>
        /// casting the Params to a double Array
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static implicit operator double[](Params p)
        {
            List<double> res = new List<double>();

            foreach (Object o in p.parms)
                res.Add(Convert.ToDouble(o));

            return res.ToArray();
        }

        /// <summary>
        /// casting the Params to a int Array
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static implicit operator int[](Params p)
        {
            List<int> res = new List<int>();

            foreach (Object o in p.parms)
                res.Add(Convert.ToInt32(o));

            return res.ToArray();
        }

        /// <summary>
        /// casting the Params to a Vector2 Array
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static implicit operator Vector2[](Params p)
        {
            List<Vector2> res = new List<Vector2>();

            if (p.parms.Count % 2 != 0)
                return null;

            for (int i = 0; i < p.parms.Count; i += 2)
                res.Add(new Vector2(Convert.ToSingle(p.parms[i]), Convert.ToSingle(p.parms[i + 1])));

            return res.ToArray();
        }

        /// <summary>
        /// casting the Params to a String Array
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static implicit operator String[](Params p)
        {
            String[] res = new String[p.parms.Count];

            for (int i = 0; i < p.parms.Count; ++i)
                res[i] = p.parms[i].ToString();

            return res;
        }

        /// <summary>
        /// casting one int to Params
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static implicit operator Params(int i)
        {
            Params res = new Params();
            res.add(i);
            return res;
        }

        /// <summary>
        /// casting one float to Params
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static implicit operator Params(float f)
        {
            Params res = new Params();
            res.add(f);
            return res;
        }

        /// <summary>
        /// casting one double to Params
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static implicit operator Params(double d)
        {
            Params res = new Params();
            res.add(d);
            return res;
        }

        /// <summary>
        /// casting one String to Params
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static implicit operator Params(String s)
        {
            Params res = new Params();
            res.add(s);
            return res;
        }

        /// <summary>
        /// casting one Vector2 to Params
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static implicit operator Params(Vector2 v)
        {
            Params res = new Params();
            res.add(v);
            return res;
        }

        /// <summary>
        /// casting an int array to Params
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static implicit operator Params(int[] i)
        {
            Params res = new Params();
            res.add(i);
            return res;
        }

        /// <summary>
        /// casting an float array to Params
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static implicit operator Params(float[] f)
        {
            Params res = new Params();
            res.add(f);
            return res;
        }

        /// <summary>
        /// casting an double array to Params
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static implicit operator Params(double[] d)
        {
            Params res = new Params();
            res.add(d);
            return res;
        }

        /// <summary>
        /// casting an String array to Params
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static implicit operator Params(String[] s)
        {
            Params res = new Params();
            res.add(s);
            return res;
        }

        /// <summary>
        /// casting an Vector2 array to Params
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static implicit operator Params(Vector2[] v)
        {
            Params res = new Params();
            res.add(v);
            return res;
        }
        
        //*******************************************************************

        /// <summary>
        /// returns the Parameter String
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            if (parms.Count == 0)
                return "";
            else if (parms.Count == 1)
                return parms[0];
            else
            {
                String res = "[";

                foreach (Object o in parms)
                {
                    res += o.ToString() + ", ";
                }

                res = res.Remove(res.Length - 2);
                res += "]";

                return res;
            }
        }
    }
}
