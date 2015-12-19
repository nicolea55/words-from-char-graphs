using System;
using System.Collections.Generic;
using Graphs;

namespace Graphy
{
	class MainClass
	{
		/// <summary>
		/// The ms dictionary of words.
		/// </summary>
		private static List<string> msDictionary = new List<string>();

		/// <summary>
		/// Determines if the word is found our dictionary.
		/// </summary>
		/// <returns><c>true</c> if is word the specified word; otherwise, <c>false</c>.</returns>
		/// <param name="word">Word.</param>
		private static bool IsWord(string word)
		{
			bool result = msDictionary != null ? msDictionary.Contains (word) : false;

			return result;
		}

		/// <summary>
		/// Loads the dictionary from the provided file or "DefaultDictionary.txt" if empty file name provided.
		/// </summary>
		/// <param name="fileName">File name.</param>
		private static void LoadDictionary(string fileName)
		{
			System.IO.TextReader tr = null;
			try
			{
				if(string.IsNullOrEmpty(fileName))
				{
					fileName = "DefaultDictionary.txt";
				}

				tr = (System.IO.TextReader)System.IO.File.OpenText (fileName);
				if(tr != null)
				{
					string aLine = string.Empty;
					do
					{
						aLine = tr.ReadLine ();
						if(!string.IsNullOrEmpty(aLine))
						{
							msDictionary.Add(aLine.ToLower());
						}
					}
					while (aLine != null);
				}
			}
			catch(System.IO.FileNotFoundException fileNotFoundException)
			{
				Console.WriteLine (fileNotFoundException.Message);
			}
			catch(System.IO.FileLoadException fileLoadException)
			{
				Console.WriteLine(fileLoadException.Message);
			}
			catch(Exception exception)
			{
				Console.WriteLine(exception.Message);
			}
			finally
			{
				if(tr != null)
				{
					tr.Close();
				}
			}
		}

		// TODO: make it read a JSON file or from DB
		public static Graph MakeGraph()
		{
			#region Make graph
			Graph g = new Graph ();

			// Create and add nodes
			Node p = new Node ('p');
			g.AddNode (p);
			Node o = new Node ('o');
			g.AddNode (o);
			Node p2 = new Node ('p');
			g.AddNode (p2);
			Node c = new Node ('c');
			g.AddNode (c);
			Node o2 = new Node ('o');
			g.AddNode (o2);
			Node r = new Node ('r');
			g.AddNode (r);
			Node n = new Node ('n');
			g.AddNode (n);
			Node a = new Node ('a');
			g.AddNode (a);

			// Add edges
			//p
			g.AddEdge(p, o);
			g.AddEdge(p, a);
			g.AddEdge(p, n);
			g.AddEdge(p, r);
			// o
			g.AddEdge( o, p2);
			g.AddEdge( o, a);
			g.AddEdge( o, n);
			g.AddEdge( o, p);
			// p2
			g.AddEdge( p2, c);
			g.AddEdge( p2, a);
			g.AddEdge( p2, o);
			// c
			g.AddEdge( c, o2);
			g.AddEdge( c, n);
			g.AddEdge( c, a);
			g.AddEdge( c, p2);
			// o2
			g.AddEdge( o2, r);
			g.AddEdge( o2, n);
			g.AddEdge( o2, a);
			g.AddEdge( o2, c);
			// r
			g.AddEdge( r, p);
			g.AddEdge( r, n);
			g.AddEdge( r, o2);
			// n
			g.AddEdge( n, p);
			g.AddEdge( n, o);
			g.AddEdge( n, a);
			g.AddEdge( n, c);
			g.AddEdge( n, o2);
			g.AddEdge( n, r);
			// a
			g.AddEdge( a, o);
			g.AddEdge( a, p2);
			g.AddEdge( a, c);
			g.AddEdge( a, o2);
			g.AddEdge( a, n);
			g.AddEdge( a, p);
			#endregion Make graph

			return g;
		}

		public static void Main (string[] args)
		{
			List<string> result = new List<string> ();

			// Load the dictionary of known words
			string dicFileName = args != null && args.Length >= 1 ? args [0] : string.Empty;
			LoadDictionary (dicFileName);

			// Make the graph
			Graph g = MakeGraph ();

			// Get the list of char combinations as potential words
			List<string> combinations = g.GetCombinations();

			// Check each combination we found (and print just for review)
			Console.WriteLine ("Letter combinations found (may duplicate):");
			foreach (string combination in combinations) {
				if (IsWord (combination.ToLower()) && !result.Contains(combination)) {
					result.Add (combination);
				}

				Console.Write (combination + "; ");
			}

			Console.WriteLine ();
			Console.WriteLine ();
			Console.WriteLine ("Here are the word we've found");
			Console.WriteLine ();

			// Print out the result
			result.Sort ();
			foreach (string word in result) {
				Console.WriteLine (word);
			}

			Console.WriteLine ();
			Console.WriteLine ();
			Console.WriteLine ("Done! (Press any key...)");
			Console.ReadKey ();
		}
	}
}
