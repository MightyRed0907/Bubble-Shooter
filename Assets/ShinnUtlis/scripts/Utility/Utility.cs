using UnityEngine;

namespace Shinn
{
    public class Utility
    {
        /// <summary>
        /// Mapping 
        /// </summary>
        public static float Map(float v, float a, float b, float x, float y)
        {
            return (v == a) ? x : (v - a) * (y - x) / (b - a) + x;
        }

        /// <summary>
        /// 不重覆亂數 (int) 0 ~ length
        /// </summary>
        public static int[] NonrepetitiveRandom(int total)
        {
            int[] sequence = new int[total];
            int[] output = new int[total];

            for (int i = 0; i < total; i++)
            {
                sequence[i] = i;
            }

            int end = total - 1;
            for (int i = 0; i < total; i++)
            {
                int num = UnityEngine.Random.Range(0, end + 1);
                output[i] = sequence[num];
                sequence[num] = sequence[end];
                end--;
            }
            return output;
        }

        /// <summary>
        /// String to float, 無法轉換 out defaultValue
        /// </summary>
        public static float StringToFloat(string stringValue, float defaultValue = 0)
	    {
		    float result = defaultValue;
		    float.TryParse(stringValue, out result);
		    return result;
	    }

        /// <summary>
        /// Get Ip address
        /// </summary>
        public static string GetLocalIPAddress()
        {
            var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            foreach (var ip in host.AddressList)            
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)                
                    return ip.ToString(); 
            throw new System.Exception("No network adapters with an IPv4 address in the system!");
        }
	    
	/// <summary>
        /// CopyComponent function, from https://answers.unity.com/questions/458207/copy-a-component-at-runtime.html
        /// </summary>
	public Component CopyComponent(Component original, GameObject destination)
    	{
        	System.Type type = original.GetType();
        	Component copy = destination.AddComponent(type);
        	// Copied fields can be restricted with BindingFlags
        	System.Reflection.FieldInfo[] fields = type.GetFields();
        	foreach (System.Reflection.FieldInfo field in fields)
        	{
            	field.SetValue(copy, field.GetValue(original));
        	}
        	return copy;
    	}
	    
    }

}
