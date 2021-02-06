using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Util{
	static void Main(){
		Sol mySol =new Sol();
		mySol.Solve();
	}
}

class Sol{
	public void Solve(){
        int N = ri();

        List<string> ss = new List<string>();
        for(int i = 0; i < N; i++) ss.Add(rs());
        // int ACcnt = 0;
        // int WAcnt = 0;
        // int TLEcnt = 0;
        // int REcnt = 0;
        Dictionary<string, int> dic = new Dictionary<string, int>() {
            {"AC", 0},
            {"WA", 0},
            {"TLE", 0},
            {"RE", 0},
        };
        
        foreach(string s in ss) dic[s]++;

        string ans = "";

        foreach(string key in dic.Keys) ans += key + " x " + dic[key] + "\r\n";

        Console.WriteLine(ans);
        Console.ReadLine();
	}

	static String rs(){return Console.ReadLine();}
	static int ri(){return int.Parse(Console.ReadLine());}
	static long rl(){return long.Parse(Console.ReadLine());}
	static double rd(){return double.Parse(Console.ReadLine());}
	static String[] rsa(char sep=' '){return Console.ReadLine().Split(sep);}
	static int[] ria(char sep=' '){return Array.ConvertAll(Console.ReadLine().Split(sep),e=>int.Parse(e));}
	static long[] rla(char sep=' '){return Array.ConvertAll(Console.ReadLine().Split(sep),e=>long.Parse(e));}
	static double[] rda(char sep=' '){return Array.ConvertAll(Console.ReadLine().Split(sep),e=>double.Parse(e));}
}
